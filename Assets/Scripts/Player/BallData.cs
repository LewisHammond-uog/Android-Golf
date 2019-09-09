using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallData : MonoBehaviour {

    private int shots = 0;
    public int Shots {
        get { return shots; }
    }

    void OnEnable()
    {
        //Subscribe to events
        BallController.ShotTaken += IncrementShotCounter;
    }

    void OnDisable()
    {
        //Unsubscribe from event
        BallController.ShotTaken -= IncrementShotCounter;
    }

    /// <summary>
    /// Increases the shot counter by 1
    /// </summary>
    private void IncrementShotCounter()
    {
        shots++;
    }
}
