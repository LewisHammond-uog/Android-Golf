using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuModel : MonoBehaviour {

    [SerializeField]
    private GameObject[] models;

    private GameObject currentModel;
    private float rotationSpeed = 10f;
    private float rotation;

	// Use this for initialization
	void Start () {
        int randomModelIndex = Random.Range(0, models.Length);
        currentModel = Instantiate(models[randomModelIndex]);
        currentModel.transform.parent = transform;
    }
	
	// Update is called once per frame
	void Update () {
        rotation += rotationSpeed * Time.deltaTime;
        currentModel.transform.rotation = Quaternion.Euler(0f, rotation, 0f);
	}
}
