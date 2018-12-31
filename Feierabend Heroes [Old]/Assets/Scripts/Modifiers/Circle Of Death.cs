using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleOfDeath : MonoBehaviour {

	private float levelDuration = 30.0f;

	public GameObject deathCircle;
	private GameObject deathCircleSpawn;


	void Start ()
    {
        // Set level duration time
        LevelTimer.levelDurationCounter = levelDuration;

		// Attach the death circle and spawn point
		deathCircleSpawn = GameObject.Find("DeathCircleSpawn");

		// Instantiate the death circle at the spawn position
		deathCircle = (GameObject)Resources.Load("CircleOfDeath", typeof(GameObject));
		GameObject newDeathCircle = Instantiate(deathCircle, Vector3.zero, Quaternion.identity);
		// newDeathCircle.transform.parent = deathCircleSpawn.transform;
		newDeathCircle.transform.localScale = deathCircleSpawn.transform.localScale;
		newDeathCircle.transform.localPosition = deathCircleSpawn.transform.localPosition;
		Destroy(deathCircleSpawn);
    }

}
