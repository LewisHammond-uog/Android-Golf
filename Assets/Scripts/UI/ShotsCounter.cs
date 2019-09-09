using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotsCounter : MonoBehaviour {

    [SerializeField]
    private Text shotsCountText;

    private int shotsTakenByPlayer;

    private void Start()
    {
        //Reset Shot counter to 0
        shotsTakenByPlayer = 0;
    }

    //Sub/UnSubs for shot text
    void OnEnable()
    {
        BallController.ShotTaken += UpdateShotsUI;
    }

    void OnDisable()
    {
        BallController.ShotTaken -= UpdateShotsUI;
    }

    /// <summary>
    /// Updates the Shots UI to the current number of shots taken by the
    /// player
    /// </summary>
    void UpdateShotsUI()
    {
        shotsTakenByPlayer++;
        shotsCountText.text = shotsTakenByPlayer.ToString();
    }
}
