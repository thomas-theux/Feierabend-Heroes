using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour {

	public Text countdownTimerText;

	private float levelStartCounter = 1.5f;
	public static float levelDurationCounter;
	private float lastSecondsCounter = 3.0f;

	private bool levelStart;
	private bool levelDuration;
	private bool lastSeconds;
	private bool levelEnd;
	private bool decreaseHealth;


	void Start()
	{
		levelStart = true;
		countdownTimerText = GameObject.Find("CountdownTimer").GetComponent<Text>();
	}


	void Update()
	{
		// Initial "3 seconds" start counter
		if (levelStart) {
			levelStartCounter -= Time.deltaTime;
			countdownTimerText.text = Mathf.Ceil(levelStartCounter * 2) + "";
			
			if (levelStartCounter <= 0) {
				levelDuration = true;

				// Allow character movement
				GameManager.allowMovement = true;
				GameManager.enableModifier = true;

				levelStart = false;
			}
		}

		// Level duration timer
		if (levelDuration && GameManager.activePlayers > 1) {
			levelDurationCounter -= Time.deltaTime;
			countdownTimerText.text = Mathf.Ceil(levelDurationCounter) + "";
			
			if (levelDurationCounter <= 3) {
				lastSeconds = true;

				levelDuration = false;
			}
		}

		// Last 3 seconds counter
		if (lastSeconds && GameManager.activePlayers > 1) {
			lastSecondsCounter -= Time.deltaTime;
			countdownTimerText.text = Mathf.Ceil(lastSecondsCounter) + "";
			
			if (lastSecondsCounter <= 0) {
				lastSeconds = false;
				decreaseHealth = true;
			}
		}

		// If there is still more than 1 player left after the 3 seconds countdown it will decrease their health
		if (decreaseHealth && GameManager.activePlayers > 1) {
			for (int i = 0; i < GameManager.activePlayerArr.Count; i++) {
				GameObject.Find("Character0" + GameManager.activePlayerArr[i] + "(Clone)").GetComponent<HealthManager>().currentHealth -= 0.5f;
			}
		} else if (decreaseHealth) {
			GetComponent<GameManager>().LevelEnd();
			lastSeconds = false;
		}
	}
}
