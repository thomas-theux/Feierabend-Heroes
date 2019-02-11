using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeHandler : MonoBehaviour {
	
	public static bool startLevel = false;
	public static bool startBattle = false;

	public Text levelStartTimerText;

	private float levelStartTimeDef = 3.0f;
	private float levelStartTime;
	private float battleStartTimeDef = 6.0f;
	private float battleStartTime;
	private float lastSecondsTimeDef = 3.0f;
	private float lastSecondsTime;

	private bool levelStartTimerActive = false;
	private bool battleStartTimerActive = false;

	private GameObject levelGO;
	public GameObject safeZoneGO;

	private float minPosX;
	private float minPosZ;
	private float maxPosX;
	private float maxPosZ;

	private float levelPadding = 20.0f;

	public static bool lastSeconds = false;


	private void Awake() {
		levelGO = GameObject.Find("Ground");

		// Reset all bools
		startLevel = false;
		startBattle = false;
		battleStartTimerActive = false;
		lastSeconds = false;

		levelStartTimerActive = true;
		levelStartTime = levelStartTimeDef;
		battleStartTime = battleStartTimeDef;
		lastSecondsTime = lastSecondsTimeDef;
	}


	private void Update() {
		if (levelStartTimerActive) {
			LevelStartTimer();
		}

		if (battleStartTimerActive) {
			BattleStartTimer();
		}

		if (lastSeconds) {
			LastSecondsTimer();
		}
	}


	private void LevelStartTimer() {
		if (levelStartTime > 0.1f) {
			levelStartTime -= Time.deltaTime;
			levelStartTimerText.text = Mathf.Ceil(levelStartTime) + "";
		} else {
			levelStartTimerText.text = "Explore!";
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

			minPosX = 0 - levelGO.transform.localScale.x / 2 + levelPadding;
			minPosZ = 0 - levelGO.transform.localScale.z / 2 + levelPadding;
			maxPosX = levelGO.transform.localScale.x / 2 - levelPadding;
			maxPosZ = levelGO.transform.localScale.z / 2 - levelPadding;

			float rndPosX = Random.Range(minPosX, maxPosX);
			float rndPosZ = Random.Range(minPosZ, maxPosZ);

			GameObject newSafeZone = Instantiate(safeZoneGO);
			newSafeZone.transform.position = new Vector3(rndPosX, newSafeZone.transform.position.y, rndPosZ);

			battleStartTimerActive = false;
		}
	}


	private IEnumerator WaitOneSec() {
		yield return new WaitForSeconds(1.0f);
		levelStartTimerText.text = "";
	}


	private void LastSecondsTimer() {
		if (lastSecondsTime > 0.1f) {
			lastSecondsTime -= Time.deltaTime;
			levelStartTimerText.text = Mathf.Ceil(lastSecondsTime) + "";
		} else {
			lastSeconds = false;
			levelStartTimerText.text = "Round Over!";
			startLevel = false;
			startBattle = false;

			StartCoroutine(WaitOneSec());
			SceneManager.LoadScene("3 Aeras");
		}
	}

}
