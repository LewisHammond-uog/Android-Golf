using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCamera : GolfCamera {


    //Camera movement vars
    private Vector3 desiredPostion;
    [SerializeField]
    private float cameraYset = 1.0f;
    [SerializeField]
    private float radius = 5.0f;
    [SerializeField]
    private float radiusSpeed = 1f;
    [SerializeField]
    private float rotationSpeed = 80.0f;
    private float fingerMovementSensitivity = 2f;

    //Camera start postion (relative to ball pos)
    private readonly Vector3 camStartPos = new Vector3(0f, 0.372f, -0.827f);

    //Touch vars
    private Vector2 previousTouchPos;
    private Vector3 cameraMoveAmount;
    private bool cameraEnabled = false;

    void Awake()
    {
        //Set Pos and look at ball
        transform.position = target.transform.position + camStartPos;
        transform.LookAt(target.transform);
        fingerMovementSensitivity = SavedOptions.InputSensitivity;
    }

    void OnEnable()
    {
        //Subscribe to event for when camera rotation is unlocked by the ball contoller
        BallController.SetBallCameraActive += SetCameraEnabled;
        HolePotChecker.BallPotted += DeactivateCamera;
    }

    void OnDisable()
    {
        //Unsub from event when we are diabled
        BallController.SetBallCameraActive -= SetCameraEnabled;
        HolePotChecker.BallPotted -= DeactivateCamera;
    }

    void Update()
    {

        Vector2 cameraMoveAmount = Vector2.zero;

        //Check this camera is enabled
        if (cameraEnabled)
        {
            //Calculate the amount to move the camera then rotate by that amount
            cameraMoveAmount = CalculateCameraMovement();
            RotateCamera(cameraMoveAmount);
        }

        
    }

    /// <summary>
    /// Rotates the camera a given amount given by a movement in a 2D space
    /// </summary>
    /// <param name="rotateAmount">Amount to rotate camera on x and y axis</param>
    private void RotateCamera(Vector2 rotateAmount)
    {
        if (target != null)
        {
            //Check if we have a reasonable iputy to change camera pos
            if (Mathf.Abs(rotateAmount.x + rotateAmount.y) > 1)
            {
                transform.RotateAround(target.transform.position, new Vector3(0f, rotateAmount.x, 0f), rotationSpeed * fingerMovementSensitivity * Time.deltaTime);
            }

            //Caculate tartget pos for desired y pos if we aren't touching the screen
            Vector3 cameraBallDifference = Vector3.zero;

            //If the screen has been touched then allow the camera to move to the touched location
            //else settle at a given y postion
            if (TouchController.ScreenTouched())
            {
                cameraBallDifference = (transform.position - target.transform.position).normalized;
            }
            else
            {
                cameraBallDifference = (new Vector3(transform.position.x, cameraYset, transform.position.z) - new Vector3(target.transform.position.x, 0f, target.transform.position.z)).normalized;
            }

            //Work out the postion that the camera should be at
            desiredPostion = cameraBallDifference * radius + target.transform.position;

            //Calculate the camera speed based on our current distance from the camera so that we have a smooth camera,
            //then move the camera
            float cameraSpeed = Time.deltaTime * (radiusSpeed + (Vector3.Distance(transform.position, target.transform.position) * radiusSpeed));
            transform.position = Vector3.MoveTowards(transform.position, desiredPostion, cameraSpeed);

            transform.rotation = SmoothLookAt(transform, target.transform);
        }
    }

    /// <summary>
    /// Calculates the amount to move the camera based on user touch inputs
    /// </summary>
    /// <returns>Vector 2 of amount to rotate the camera in x/y axis</returns>
    private Vector2 CalculateCameraMovement()
    {

        //Reset finger movemnet
        Vector2 fingerMovement = Vector2.zero;

        //Check if we should even calculate the movement as the screen has ben touched
        if (TouchController.ScreenTouched())
        {
            //Get the current touch
            Touch currentTouch = Input.GetTouch(0);

            //If the touch has moved then calculate the difference between this and last frame
            if (currentTouch.phase == TouchPhase.Moved)
            {
                fingerMovement = (currentTouch.position - previousTouchPos);
                previousTouchPos = currentTouch.position;
            }
        }

        //Return how much our finger has moved
        return fingerMovement;

    }

    /// <summary>
    /// Sets if the camera is allowed to rotate
    /// </summary>
    /// <param name="allowed"></param>
    private void SetCameraEnabled(bool allowed)
    {
        cameraEnabled = allowed;
    }

    /// <summary>
    /// Sets the camera to move
    /// </summary>
    private void DeactivateCamera()
    {
        cameraEnabled = false;
    }
}
