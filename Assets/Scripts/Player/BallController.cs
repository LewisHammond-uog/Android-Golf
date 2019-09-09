using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour {

    //Enum for ball state
    public enum BallState
    {
        Stopped,
        AdjustDirection,
        AdjustPower,
        ApplyForce,
        Moving,
        Disabled
    }

    [SerializeField]
    private GameObject pointerArrow;

    [Header("Ball Properties")]
    private float powerChangePerSecond = 1000f;
    private float maxPower = 1500f;
    public float MaxPower { get { return maxPower; } }
    private float minPower = 0.0f;
    public float pendingPower { get; private set; }
    private float powerChangeDirection = 1;
    private Vector3 pendingDirection; //Direction for the power value to move
    private Rigidbody ballRigidBody;

    public BallState currentState { private set; get; }

    //Store our last stopped postion for reset
    private Vector3 lastStoppedPos;
    private float fallOffYOffset = 30f; //How far can we be away in the y axis from the last stopped postion
                                        //before we reset

    public delegate void BallCameraEvent(bool active);
    public static event BallCameraEvent SetBallCameraActive;

    //Events for shots being taken
    public delegate void ShotEvent();
    public static event ShotEvent ShotTaken;

    private void Start()
    {
        //Set pending power and direction to 0
        pendingDirection = Vector2.zero;
        pendingPower = 0;

        //Get Rigid Body
        ballRigidBody = GetComponent<Rigidbody>() as Rigidbody;

        //Initalise state
        currentState = BallState.Disabled;
        lastStoppedPos = transform.position;

    }
    //Event Subscribes/Unsubsribes
    private void OnEnable()
    {
        HoleController.PlayingStop += DisableBall;
        HoleController.PlayingStart += EnableBall;
    }
    private void OnDisable()
    {
        HoleController.PlayingStop -= DisableBall;
        HoleController.PlayingStart -= EnableBall;
    }


    private void Update()
    {

        //Check what state we are in, run appopriate functions
        switch (currentState)
        {
            case BallState.Stopped:

                //Unlock the camera
                SetBallCameraActive(true);
                lastStoppedPos = transform.position;

                CheckIsStationary();


                //If the screen has been touched then move in to the direction adjustment phase
                if (TouchController.ScreenTouched())
                {
                    currentState = BallState.AdjustDirection;
                }

                break;

            case BallState.AdjustDirection:

                //Show Pointer arrow
                pointerArrow.SetActive(true);

                //Stop Rotation
                gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);

                AdjustDirection();

                break;

            case BallState.AdjustPower:
                pointerArrow.SetActive(false);
                SetBallCameraActive(false);
                AdjustPower();

                break;

            case BallState.ApplyForce:

                SetBallCameraActive(true);
                ApplyForce(pendingPower, pendingDirection);

                //Reset the power
                pendingPower = minPower;

                currentState = BallState.Moving;
                pointerArrow.SetActive(false);
                ShotTaken();
                break;

            case BallState.Moving:
                CheckIsStationary();

                break;                 
        }

        //Check if we are off the edge of the world
        if(transform.position.y < (lastStoppedPos.y - fallOffYOffset))
        {
            transform.position = lastStoppedPos;
            ballRigidBody.velocity = Vector3.zero;
        }

    }

    /// <summary>
    /// Applies a force in a given direction
    /// </summary>
    /// <param name="power">Power of force</param>
    /// <param name="direction">Direction to apply force in</param>
    private void ApplyForce(float power, Vector3 direction)
    {
        ballRigidBody.AddForce(direction.normalized * power, ForceMode.Force);
    }

    /// <summary>
    /// Adjusts the direction that the ball will go and the pointer arrow based on the direction the camera is facing
    /// </summary>
    private void AdjustDirection()
    {

        Vector3 currentCameraDirection = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);

        //Adjust the pointer arrow
        Vector3 targetDir = pointerArrow.transform.position + currentCameraDirection.normalized - transform.position + new Vector3(0,-90,0);
        pointerArrow.transform.rotation = Quaternion.LookRotation(targetDir);
        pendingDirection = currentCameraDirection;
    }

    /// <summary>
    /// Locks in the current direction and changes to adjust power state
    /// </summary>
    public void StartApplyingPower()
    {
        if (currentState != BallState.AdjustPower)
        {
            StartCoroutine(StartPowerApplyChange());
        }
    }

    /// <summary>
    /// Sets the state to apply force after this frame has ended
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartPowerApplyChange()
    {
        yield return new WaitForEndOfFrame();
        currentState = BallState.AdjustPower;
    }

    /// <summary>
    /// Adjusts the pending power based on whether the user is holding the screen or not
    /// </summary>
    private void AdjustPower()
    {

        if (TouchController.ScreenTouched()) {

            TouchPhase userTouchPhase = TouchController.GetTouchPhase();

            if (userTouchPhase == TouchPhase.Ended)
            {

                //Touch has been ended, change state
                currentState = BallState.ApplyForce;

            }
            else
            {
                //If the touch is in any other phase than ended then continue to move the power bar

                float powerChangeThisFrame = (powerChangePerSecond * Time.deltaTime);

                //If we are going to go past the min (0) or max power flip the direction that
                //we change or valiue in (+ve / -ve)
                if ((pendingPower + powerChangeThisFrame) > maxPower)
                {
                    pendingPower = maxPower;
                    powerChangeDirection = -1;
                }
                else if ((pendingPower - powerChangeThisFrame) < minPower)
                {
                    pendingPower = 0;
                    powerChangeDirection = 1;
                }


                //Apply Pending power
                pendingPower += (powerChangeThisFrame * powerChangeDirection);

            }
        }
    }

    

    /// <summary>
    /// Check if our velocity is not zero, set our state to moving, if we are
    /// moving and we have stopped then set our state to stopped
    /// </summary>
    private void CheckIsStationary()
    {

        if(Mathf.Abs(ballRigidBody.velocity.x) < 0.01f && Mathf.Abs(ballRigidBody.velocity.y) < 0.01f
            && Mathf.Abs(ballRigidBody.velocity.z) < 0.01f)
        {
            //Set state to moving overriding other states
            currentState = BallState.Stopped;


            //Make sure velcoty is zero and no rotation
            ballRigidBody.velocity = Vector3.zero;
            ballRigidBody.rotation = Quaternion.Euler(Vector3.zero);

        }
        else
        {
            currentState = BallState.Moving;
        }

    }

    /// <summary>
    /// Set the ball to be inactive
    /// </summary>
    /// <param name="active"></param>
    public void DisableBall()
    {
        currentState = BallState.Disabled;
    }    
    
    /// <summary>
    /// Set the ball to be active
    /// </summary>
    /// <param name="active"></param>
    public void EnableBall()
    {
        currentState = BallState.Stopped;
    }
}
