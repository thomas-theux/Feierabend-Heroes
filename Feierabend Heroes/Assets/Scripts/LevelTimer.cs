using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour {

	private float levelStartCounter = 3.0f;
	private float levelDurationCounter = 3.0f;
	private float lastSecondsCounter = 3.0f;

	private bool levelStart;
	private bool levelDuration;
	private bool lastSeconds;
	private bool levelEnd;


	void Start()
	{
		levelStart = true;
	}


	void Update()
	{
		if (levelStart) {
			levelStartCounter -= Time.deltaTime;
			
			if (levelStartCounter <= 0) {
				print("start level");
				levelDuration = true;
				levelStart = false;
			}
		}

		if (levelDuration) {
			levelDurationCounter -= Time.deltaTime;
			
			if (levelDurationCounter <= 0) {
				print("playtime ended");
				lastSeconds = true;
				levelDuration = false;
			}
		}

		if (lastSeconds) {
			lastSecondsCounter -= Time.deltaTime;
			
			if (lastSecondsCounter <= 0) {
				print("level ended");
				levelEnd = true;
				lastSeconds = false;
			}
		}

		if (levelEnd) {
			// Stop character Movement
			// Stop attacking
		}
	}
}
