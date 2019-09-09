using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    [SerializeField]
    private GameObject explosionPrefab;

    private float fuseTime = 5f;
    private float fuseCountdown;
    private float explodeWaitTime = 2f;
    private bool exploded = false;

	// Use this for initialization
	void Start () {
        fuseCountdown = fuseTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(fuseCountdown <= 0f)
        {
            Explode();
        }

        fuseCountdown -= Time.deltaTime;
	}

    private void OnCollisionEnter(Collision collision)
    {
        //If we collide with the ball or another bomb explode
        if (collision.gameObject.GetComponent<BallController>() != null ||
            collision.gameObject.GetComponent<Bomb>() != null)
        {
            Explode();
        }
    }

    /// <summary>
    /// Explodes the bomb
    /// </summary>
    /// <returns></returns>
    private void Explode()
    {
        if (!exploded)
        {
            GameObject explosion = Instantiate(explosionPrefab);
            explosion.transform.position = transform.position;
            explosion.transform.parent = transform;
            //The Explosion prefab applies the explosion force
            StartCoroutine(WaitForDestroy());
            
        }
    }

    /// <summary>
    /// Waits for a given time to destroy the bomb and its explosion
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForDestroy()
    {
        exploded = true;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(explodeWaitTime);
        Destroy(gameObject);
    }
}
