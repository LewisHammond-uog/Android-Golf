using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    private GolfCamera currentTargetCamera;
    private GolfCamera currentActiveCamera;
    private bool transitionActive = false;

    private float cameraTransitionSpeed = 0.75f;
    private float cameraDistanceSpeedMultiplyer = 0.5f; //How much the distance between this and the target camera influcnces camera speed
    private float cameraRotationSpeed = 10f;

    //Cameras
    public GolfCamera levelPreviewCamera, ballCamera, holeCamera;
	
	// Update is called once per frame
	void Update () {

        if (transitionActive)
        {
            if (currentActiveCamera != null && currentTargetCamera != null)
            {
                currentActiveCamera.transform.rotation = GolfCamera.SmoothLookAt(currentActiveCamera.transform, currentTargetCamera.Target.transform, 
                                                        cameraRotationSpeed);

                //Move towards the target camera
                float dist = (Vector3.Distance(currentActiveCamera.transform.position, currentTargetCamera.transform.position));
                currentActiveCamera.transform.position = Vector3.MoveTowards(currentActiveCamera.transform.position,
                                                        currentTargetCamera.transform.position, 
                                                        (cameraTransitionSpeed + (dist * cameraDistanceSpeedMultiplyer)) * Time.deltaTime);

                //Check if we are at the camera postion
                if (currentActiveCamera.transform.position == currentTargetCamera.transform.position)
                {
                    //Stop Transition
                    transitionActive = false;
                    currentTargetCamera = currentActiveCamera = null;
                }
            }
        }
		
	}

    /// <summary>
    /// Initiates the transition between two cameras
    /// </summary>
    /// <param name="activeCamera">Active Camera to manipulate</param>
    /// <param name="targetCamera">Camera to move towards</param>
    public void InitiateCameraTransision(GolfCamera activeCamera, GolfCamera targetCamera)
    {
        currentActiveCamera = activeCamera;
        currentTargetCamera = targetCamera;
        transitionActive = true;
    }

    /// <summary>
    /// Checks if we are transitioning between two cameras
    /// </summary>
    /// <returns>If we are transitioning</returns>
    public bool IsTransitioning(GolfCamera activeCamera, GolfCamera targetCamera)
    {
        if (transitionActive)
        {
            if (activeCamera == currentActiveCamera && targetCamera == currentTargetCamera)
            {
                return true;
            }
        }
        return false;
    }

}
