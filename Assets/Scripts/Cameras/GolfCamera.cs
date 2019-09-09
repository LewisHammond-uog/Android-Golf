using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent Class for Camera in Golf
/// </summary>
public class GolfCamera : MonoBehaviour {

    [SerializeField]
    protected GameObject target;
    public GameObject Target
    {
        get { return target; }
    }

    [SerializeField]
    protected Camera cam;
    public Camera Cam
    {
        get { return cam; }
    }

    /// <summary>
    /// Slerps between where the camera is currently looking and an object that the camera
    /// should be looking at creating a smooth alternative to the Unity LookAt Function
    /// </summary>
    /// <param name="cameraTransform">Transform of the camera being modified</param>
    /// <param name="targetTransform">Transform of the object to lookat</param>
    /// <returns>Camera Rotation</returns>
    public static Quaternion SmoothLookAt(Transform cameraTransform, Transform targetTransform, float rotateSpeed = 1f)
    {
        var targetRotation = Quaternion.LookRotation(targetTransform.position - cameraTransform.position);

        // Smoothly rotate towards the target point.
        return Quaternion.Slerp(cameraTransform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Checks if a given camera transform is looking at a target
    /// </summary>
    /// <param name="cameraTransform">Camera Transform</param>
    /// <param name="targetTransform">Target</param>
    /// <returns>If we are looking at object</returns>
    public static bool IsLookingAt(Transform cameraTransform, Transform targetTransform)
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(cameraTransform.position, cameraTransform.forward);

        foreach(RaycastHit hit in hits)
        {
            if(hit.transform == targetTransform)
            {
                return true;
            }
        }

        return false;
        
    }

}
