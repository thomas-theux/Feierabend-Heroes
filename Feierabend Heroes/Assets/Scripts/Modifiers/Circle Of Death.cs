using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleOfDeath : MonoBehaviour {

	private float levelDuration = 30.0f;

	private GameObject deathCircle;


	void Start ()
    {
        // Set level duration time
        LevelTimer.levelDurationCounter = levelDuration;

		// Find and attach the circle of death
		deathCircle = GameObject.Find("DeathCircle");
    }


	void Update()
	{
		if (GameManager.enableModifier) {
			deathCircle.transform.localScale -= new Vector3(0.06f, 0, 0.06f);
        }
	}

}
