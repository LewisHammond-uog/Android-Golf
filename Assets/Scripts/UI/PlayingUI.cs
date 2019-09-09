using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayingUI : MonoBehaviour {

    [SerializeField]
    private BallController playerBall;

    [SerializeField]
    private Text toolTipText;
    [SerializeField]
    private Button lockDirectionButton;

    [Header("Quit UI")]
    [SerializeField]
    private GameObject quitUI;
    [SerializeField]
    private string menuSceneName;

    //Tool tips 
    const string toolTipDirectionChange = "Set ball direction, then tap LOCK DIRECTION";
    const string toolTipAdjustPower = "Hold down to adjust power then release to shoot";
    const string toolTipMoving = "Wait for the ball to stop";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        DoTooltipDisplay();
	}

    /// <summary>
    /// Displays a tool tip on the screen related to the current
    /// game state
    /// </summary>
    private void DoTooltipDisplay()
    {
        toolTipText.text = GetCurrentTooltip();

        //Choose if lock direction button should be shown
        if (playerBall.currentState == BallController.BallState.Stopped ||
            playerBall.currentState == BallController.BallState.AdjustDirection)
        {
            if (!lockDirectionButton.gameObject.activeSelf)
            {
                lockDirectionButton.gameObject.SetActive(true);
            }
        }
        else if (lockDirectionButton.gameObject.activeSelf)
        {
            lockDirectionButton.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Toggles the Quit UI being shown
    /// </summary>
    public void ToggleQuitMenu()
    {
        quitUI.SetActive(!quitUI.activeSelf);
    }

    /// <summary>
    /// Quits to main menu
    /// </summary>
    public void QuitToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    /// <summary>
    /// Gets the correct tool tip for the current gameplay state
    /// </summary>
    /// <returns>String of tool tip text</returns>
    private string GetCurrentTooltip()
    {
        //Select tool tip text based on current ball state
        switch (playerBall.currentState)
        {
            case BallController.BallState.Stopped:
            case BallController.BallState.AdjustDirection:
                return toolTipDirectionChange;
            case BallController.BallState.AdjustPower:
                return toolTipAdjustPower;
            case BallController.BallState.ApplyForce:
            case BallController.BallState.Moving:
                return toolTipMoving;
            default:
                return string.Empty;
        }
    }
}
