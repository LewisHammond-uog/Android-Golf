using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleRotateCamera : GolfCamera {

    [SerializeField]
    float rotateSpeed;

    void Start()
    {
        transform.LookAt(target.transform);
    }

    // Update is called once per frame
    void Update () {

        //Rotate around hole at a given radius and speed
        transform.RotateAround(target.transform.position, new Vector3(0, 1, 0), Time.deltaTime * rotateSpeed);
        transform.LookAt(target.transform);
	}
}
