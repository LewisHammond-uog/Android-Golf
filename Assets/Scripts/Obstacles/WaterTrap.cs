using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrap : MonoBehaviour {

    [SerializeField]
    private GameObject respawnPoint;

    //If we enter the water then reset the ball 
    private void OnTriggerEnter(Collider other)
    {
        //Check that it is the ball that has collided
        if(other.gameObject.GetComponent<BallController>() != null)
        {
            other.gameObject.transform.position = respawnPoint.transform.position;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.GetComponent<Rigidbody>().rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
