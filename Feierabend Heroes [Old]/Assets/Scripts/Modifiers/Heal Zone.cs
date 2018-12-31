using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealZone : MonoBehaviour {

	private float levelDuration = 30.0f;

	private GameObject safeZone;


	void Start()
	{
		// Set level duration time
        LevelTimer.levelDurationCounter = levelDuration;

		safeZone = (GameObject)Resources.Load("HealZone", typeof(GameObject));
		Instantiate(safeZone, new Vector3(0, 1, 0), Quaternion.identity);
	}

}
