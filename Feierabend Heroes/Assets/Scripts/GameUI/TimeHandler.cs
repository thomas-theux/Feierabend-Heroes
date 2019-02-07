using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeHandler : MonoBehaviour {
	
	public static bool startLevel = false;
	public static bool startBattle = false;

	public Text levelStartTimerText;

	private float levelStartTimeDef = 3.0f;
	private float levelStartTime;
	private float battleStartTimeDef = 6.0f;
	private float battleStartTime;

	private bool levelStartTimerActive = false;
	private bool battleStartTimerActive = false;


	private void Awake() {
		levelStartTimerActive = true;
		levelStartTime = levelStartTimeDef;
		battleStartTime = battleStartTimeDef;
	}


	private void Update() {
		if (levelStartTimerActive) {
			LevelStartTimer();
		}

		if (battleStartTimerActive) {
			BattleStartTimer();
		}
	}


	private void LevelStartTimer() {
		if (levelStartTime > 0.1f) {
			levelStartTime -= Time.deltaTime;
			levelStartTimerText.text = Mathf.Ceil(levelStartTime) + "";
		} else {
			levelStartTimerText.text = "GO!";
			StartCoroutine(WaitOneSec());
			startLevel = true;
			battleStartTimerActive = true;
			levelStartTimerActive = false;
		}
	}


	private void BattleStartTimer() {
		if (battleStartTime > 0.1f) {
			battleStartTime -= Time.deltaTime;
		} else {
			startBattle = true;
			battleStartTimerActive = false;
		}
	}


	IEnumerator WaitOneSec() {
		yield return new WaitForSeconds(1.0f);
		levelStartTimerText.text = "";
	}

}
