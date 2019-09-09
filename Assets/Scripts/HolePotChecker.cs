using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolePotChecker : MonoBehaviour {

    [SerializeField]
    private HoleController holeManager;

    const string playerTag = "Player";

    //Event for when the ball has been pot
    public static event BallController.ShotEvent BallPotted;  

    void OnTriggerEnter(Collider other)
    {
        //Check that it was the ball that collided
        if (other.GetComponent<BallController>() != null && other.GetComponent<BallData>() != null)
        { 

            //Get the balldata
            BallData data = other.GetComponent<BallData>();

            //Set the score for this hole
            ScoreKeeper.AddScore(holeManager.HoleNumber, data.Shots);

            //Trigger ball potted event
            if (BallPotted != null)
            {
                BallPotted();
            }
        }
    }

}
