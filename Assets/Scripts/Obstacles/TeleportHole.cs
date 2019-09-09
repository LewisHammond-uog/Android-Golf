using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportHole : MonoBehaviour {

    [SerializeField]
    private TeleportingHoles teleportController;

    private void OnTriggerEnter(Collider other)
    {
        //Teleport to the correct hole
        if(other.GetComponent<BallController>() != null)
        {
            //Get the postion of the linked hole in the teleporting holes controller
            //then teleport there
            Vector3 teleportPos = teleportController.holeLinkDictonary[gameObject].transform.position;
            other.transform.position = teleportPos;
        }
    }
}
