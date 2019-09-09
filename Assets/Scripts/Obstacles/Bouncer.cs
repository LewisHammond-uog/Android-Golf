using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour {

    [SerializeField]
    private float hitbackForce = 10f;
    [SerializeField]
    private float hitbackRadius = 1f;

    void OnTriggerEnter(Collider other)
    {
        //Check ball collsion
        if(other.GetComponent<BallController>() != null)
        {
            other.GetComponent<Rigidbody>().AddExplosionForce(hitbackForce, transform.position, hitbackRadius);
        }
    }
}
