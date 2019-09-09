using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoleController : MonoBehaviour
{

    //Enum for Current Playing State
    private enum LevelState
    {
        Preparing,
        Playing,
        Finished,
        Scorecard
    }
    private LevelState currentState = LevelState.Preparing;

    [Header("Hole Info")]
    [SerializeField]
    private int holeNumber;
    public int HoleNumber
    {
        get { return holeNumber; }
    }


    [Header("Cameras")]
    [SerializeField]
    private CameraManager holeCameraManager;

    [Header("UI")]
    [SerializeField]
    private GameObject previewUI;
    [SerializeField]
    private GameObject playingUI;
    [SerializeField]
    private GameObject finishedUI;

    [Header("Scene Transision")]
    [SerializeField]
    private string nextSceneName;
    [SerializeField]
    private Animator transitionAnim;


    //Time between camera transitions
    private const float scoreboardWaitTime = 2f;
    private bool previewCameraReturning = false;

    //Event for starting the game
    public delegate void GameEvent();
    public static event GameEvent PlayingStart;
    public static event GameEvent PlayingStop;

    private void Start()
    {
        //Initalise all UIs to be hidden
        previewUI.SetActive(false);
        playingUI.SetActive(false);
        finishedUI.SetActive(false);

        //Set the ball cam to active so that it sets its postion
        holeCameraManager.ballCamera.gameObject.SetActive(true);

        //Intialise Level Preview
        StartLevelPeview();
    }

    //Event Subscribes/Unsubscribes
    void OnEnable()
    {
        HolePotChecker.BallPotted += StartEndedState;
    }
    void OnDisable()
    {
        HolePotChecker.BallPotted -= StartEndedState;
    }

    private void Update()
    {
        //Case for current state
        switch (currentState)
        {
            case LevelState.Preparing:
                DoPreviewUpdate();
                break;
            case LevelState.Finished:
                DoEndedUpdate();
                break;
        }

    }

    /// <summary>
    /// Starts the level pewview state of the hole
    /// </summary>
    private void StartLevelPeview()
    {
        currentState = LevelState.Preparing;
        previewCameraReturning = false;

        //Make sure cameras are disabled
        holeCameraManager.holeCamera.gameObject.SetActive(false);
        holeCameraManager.ballCamera.gameObject.SetActive(false);
        holeCameraManager.levelPreviewCamera.gameObject.SetActive(true);

        //Intitalise UI
        previewUI.SetActive(true);
        playingUI.SetActive(false);
        finishedUI.SetActive(false);
    }

    private void DoPreviewUpdate()
    {
        //Check if the preview camera has finsihed and we are not already changing camera
        LevelPreviewCamera previewCamera = (LevelPreviewCamera)holeCameraManager.levelPreviewCamera;
        if (!previewCamera.FollowingRoute && !holeCameraManager.IsTransitioning(holeCameraManager.levelPreviewCamera, holeCameraManager.ballCamera))
        {
            //Change Camera to ball camera
            holeCameraManager.InitiateCameraTransision(holeCameraManager.levelPreviewCamera, holeCameraManager.ballCamera);
            previewCameraReturning = true;

            //Activate ball camera without movement or camera drawing so that it is sat in the correct position
            
        }

        if (previewCameraReturning)
        {
            if (!holeCameraManager.IsTransitioning(holeCameraManager.levelPreviewCamera, holeCameraManager.ballCamera))
            {
                holeCameraManager.levelPreviewCamera.gameObject.SetActive(false);
                holeCameraManager.ballCamera.gameObject.SetActive(true);

                //Initalise Playing State
                StartPlayingState();
            }
        }
    }

    /// <summary>
    /// Initalises the playing state of the hole
    /// </summary>
    private void StartPlayingState()
    {
        //Intitalise UI
        previewUI.SetActive(false);
        playingUI.SetActive(true);
        finishedUI.SetActive(false);

        currentState = LevelState.Playing;

        //Enable the ball
        PlayingStart();

    }
 

    /// <summary>
    /// Initalises the hole over state of the hole
    /// </summary>
    private void StartEndedState()
    {
        currentState = LevelState.Finished;

        PlayingStop();

        //Start to transition between this and the hole camera
        holeCameraManager.InitiateCameraTransision(holeCameraManager.ballCamera, holeCameraManager.holeCamera);
    }

    /// <summary>
    /// Does ended update logic
    /// </summary>
    private void DoEndedUpdate()
    {
        //Check if the transition has ended, if it has then switch camera
        if(!holeCameraManager.IsTransitioning(holeCameraManager.ballCamera, holeCameraManager.holeCamera) 
            && holeCameraManager.ballCamera.gameObject.activeSelf)
        {
            holeCameraManager.holeCamera.gameObject.SetActive(true);
            holeCameraManager.ballCamera.gameObject.SetActive(false);


            StartCoroutine("StartScoreboardWait");

        }
    }

    /// <summary>
    /// Loads a given level with an animation
    /// </summary>
    /// <param name="levelName">Level to load</param>
    private IEnumerator LoadLevelWithAnimation(string levelName)
    {
        transitionAnim.SetTrigger("endAnimation");
        finishedUI.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(levelName);
    }

    /// <summary>
    /// Continues to the next assigned level
    /// </summary>
    public void ContinueToNextLevel()
    {
        StartCoroutine(LoadLevelWithAnimation(nextSceneName));
    }

    /// <summary>
    /// Reloads the current level
    /// </summary>
    public void ReloadLevel()
    {
        StartCoroutine(LoadLevelWithAnimation(SceneManager.GetActiveScene().name));
    }

    /// <summary>
    /// Start the wait to show the scoreboard, switches to scoreboard state when done
    /// </summary>
    /// <param name="time">Time to wait</param>
    /// <returns></returns>
    public IEnumerator StartScoreboardWait()
    {
        playingUI.SetActive(false);
        yield return new WaitForSeconds(scoreboardWaitTime);
        //Switch State
        currentState = LevelState.Scorecard;
        finishedUI.SetActive(true);
        
    }

    /// <summary>
    /// Gets the total distance of this hole
    /// </summary>
    /// <returns></returns>
    public float GetHoleDistance()
    {
        LevelPreviewCamera levelPreviewCamera = (LevelPreviewCamera)holeCameraManager.levelPreviewCamera;
        return levelPreviewCamera.GetCameraPathDistance();
    }
}
