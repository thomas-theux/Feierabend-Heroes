using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour {

	public Text countdownTimerText;
	public Text levelDurationText;

	private float levelStartCounter = 1.5f;
	public static float levelDurationCounter;
	private float lastSecondsCounter = 3.0f;

	private bool levelStart;
	private bool levelDuration;
	private bool lastSeconds;
	private bool levelEnd;


	void Start()
	{
		levelStart = true;
		countdownTimerText = GameObject.Find("CountdownTimer").GetComponent<Text>();
		levelDurationText = GameObject.Find("LevelDuration").GetComponent<Text>();

		levelDurationText.enabled = false;
	}


	void Update()
	{
		// Initial "3 seconds" start counter
		if (levelStart) {
			print("Start countdown");
			levelStartCounter -= Time.deltaTime;
			countdownTimerText.text = Mathf.Ceil(levelStartCounter * 2) + "";
			
			if (levelStartCounter <= 0) {
				levelDuration = true;
				countdownTimerText.enabled = false;
				levelDurationText.enabled = true;
				levelStart = false;
			}
		}

		// Level duration timer
		if (levelDuration) {
			print("Start level");
			GameManager.allowMovement = true;
			levelDurationCounter -= Time.deltaTime;
			levelDurationText.text = Mathf.Ceil(levelDurationCounter) + "";
			
			if (levelDurationCounter <= 3) {
				lastSeconds = true;
				countdownTimerText.enabled = true;
				levelDurationText.enabled = false;
				levelDuration = false;
			}
		}

		// Last 3 seconds counter
		if (lastSeconds) {
			print("Last 3 seconds");
			lastSecondsCounter -= Time.deltaTime;
			countdownTimerText.text = Mathf.Ceil(lastSecondsCounter) + "";
			
			if (lastSecondsCounter <= 0) {
				levelEnd = true;
				countdownTimerText.enabled = false;
				lastSeconds = false;
			}
		}

		// Level end functions
		if (levelEnd) {
			print("Level end");
			GameManager.allowMovement = false;
			// Stop modifier script
		}
	}
}
