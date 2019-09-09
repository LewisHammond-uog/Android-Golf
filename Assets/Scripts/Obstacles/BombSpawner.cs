using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject[] spawnPoints;

    [SerializeField]
    private GameObject bombPrefab;

    //Time Between bombs spawning, in seconds
    [SerializeField]
    private float spawnSpacing = 10f;
    private float spawnCountdown;

	// Use this for initialization
	void Start () {
        spawnCountdown = spawnSpacing;
	}
	
	// Update is called once per frame
	void Update () {
		
        //Countdown and spawn bombs
        if(spawnCountdown <= 0f)
        {
            SpawnBombs();
            spawnCountdown = spawnSpacing;
        }

        spawnCountdown -= Time.deltaTime;

	}

    /// <summary>
    /// Spawns bombs at spawn points
    /// </summary>
    private void SpawnBombs()
    {
        foreach(GameObject spawnPoint in spawnPoints)
        {
            GameObject newBomb = Instantiate(bombPrefab);
            newBomb.transform.position = spawnPoint.transform.position;
            newBomb.transform.parent = transform;
        }
    }
}
