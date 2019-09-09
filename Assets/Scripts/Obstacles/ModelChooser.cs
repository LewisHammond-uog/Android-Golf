using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelChooser : MonoBehaviour {

    [SerializeField]
    private GameObject[] models;

	// Use this for initialization
	void Start () {
        
        //Set all models disabled
        foreach(GameObject model in models)
        {
            model.SetActive(false);
        }

        //Choose a random model to enable
        int chosenModelIndex = Random.Range(0, models.Length);
        models[chosenModelIndex].SetActive(true);
	}

}
