using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPreviewCamera : GolfCamera {

    [SerializeField]
    private GameObject holeFinish;

    [SerializeField]
    private GameObject[] levelPreviewNodes;
    private int currentNodeIndex;
    private float moveSpeed = 3f;
    private float lookAtTargetSpeed = 2f;
    private bool followingRoute = true;
    private bool lookingAtFinish = false;
    public bool FollowingRoute
    {
        get { return followingRoute ? followingRoute : !lookingAtFinish; }
    }

	// Use this for initialization
	void Start () {

        //Check we have level preview nodes
        if (levelPreviewNodes != null)
        {
            transform.position = levelPreviewNodes[0].transform.position;
            target = levelPreviewNodes[0];
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (followingRoute)
        {
            //Go towards and look at next node
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);


            //Check if we are at our target
            if (transform.position == target.transform.position)
            {
                //Check if we are at the end of the list
                if ((currentNodeIndex + 1) >= levelPreviewNodes.Length)
                {
                    followingRoute = false;
                    target = holeFinish;

                    return;
                }

                target = levelPreviewNodes[++currentNodeIndex];
            }
        }
        else
        {
            lookingAtFinish = IsLookingAt(transform, target.transform);
        }

        transform.rotation = SmoothLookAt(transform, target.transform, lookAtTargetSpeed);
    }

    /// <summary>
    /// Gets the total distance between all of the camera nodes and the finish hole
    /// </summary>
    /// <returns></returns>
    public float GetCameraPathDistance()
    {
        float distance = 0f;

        //Add up all the nodes
        for(int i = 0; i < levelPreviewNodes.Length - 1; i++)
        {
            distance += Vector3.Distance(levelPreviewNodes[i].transform.position, levelPreviewNodes[i + 1].transform.position);
        }

        //Add on the distance to the end hole
        distance += Vector3.Distance(levelPreviewNodes[levelPreviewNodes.Length - 1].transform.position, holeFinish.transform.position);

        return distance;
    }
}
