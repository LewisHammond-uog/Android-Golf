using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportingHoles : MonoBehaviour {

    [SerializeField]
    private GameObject[] inputHoles, outputHoles;

    //Dictonary for input and output holes
    public Dictionary<GameObject, GameObject> holeLinkDictonary = new Dictionary<GameObject, GameObject>();

    // Use this for initialization
    void Start () {
        holeLinkDictonary[inputHoles[0]] = outputHoles[1];
        holeLinkDictonary[inputHoles[1]] = outputHoles[0];
        holeLinkDictonary[inputHoles[2]] = outputHoles[2];
	}

}
