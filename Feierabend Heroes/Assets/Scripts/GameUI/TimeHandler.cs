using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class TimeHandler : MonoBehaviour {
	
	public static bool startLevel = false;
	public static bool startBattle = false;

	public AudioSource startExploringSound;
	public AudioSource startBattleSound;
	public AudioSource roundEndSound;
	public AudioSource timerTickingSound;

	public Text levelStartTimerText;
	public Text explorationTimerText;

	private float levelStartTimeDef = 3.0f;
	private float levelStartTime;
	private float battleStartTimeDef;
	private float battleStartTime;
	private float lastSecondsTimeDef = 3.0f;
	private float lastSecondsTime;

	private bool levelStartTimerActive = false;
	private bool battleStartTimerActive = false;

	private bool isTicking = false;
	private bool showExplorationTimer = false;

	private GameObject levelGO;
	public GameObject safeZoneGO;

	private float minPosX;
	private float minPosZ;
	private float maxPosX;
	private float maxPosZ;

	private float levelPadding = 20.0f;

	public static bool lastSeconds = false;

	private bool showResults = false;
	public static bool roundEnd = false;

	private List<int> orbCountArr = new List<int>();
	// private List<int> killsStatsArr = new List<int>();
	// private List<int> deathsStatsArr = new List<int>();

	// private List<int> medianValues = new List<int>();

	public AudioSource matchEndSound;
	public GameObject matchResultsGO;


	private void Awake() {
		levelGO = GameObject.Find("Ground");

		battleStartTimeDef = SettingsHolder.exploreTime;

		// Reset all bools
		startLevel = false;
		startBattle = false;
		battleStartTimerActive = false;
		lastSeconds = false;
		showResults = false;
		roundEnd = false;
		isTicking = false;
		showExplorationTimer = false;

		levelStartTime = levelStartTimeDef;
		battleStartTime = battleStartTimeDef;
		lastSecondsTime = lastSecondsTimeDef;

		StartCoroutine(StartTimer());
		SpawnSafeZone();
	}


	private IEnumerator StartTimer() {
		yield return new WaitForSeconds(0.8f);

		levelStartTimerText.text = "Round " + (SettingsHolder.currentRound + 1);

		yield return new WaitForSeconds(1.4f);

		levelStartTimerActive = true;
		isTicking = true;

		StartCoroutine(TimeTicking());
	}


	private void Update() {
		if (!SettingsHolder.matchOver) {
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

		if (SettingsHolder.matchOver && !showResults) {
			StartCoroutine(MatchOver());
		}
	}


	private void LevelStartTimer() {
		if (levelStartTime > 0.1f) {
			levelStartTime -= Time.deltaTime;
			levelStartTimerText.text = Mathf.Ceil(levelStartTime) + "";
		} else {
			startLevel = true;
			isTicking = false;

			Instantiate(startExploringSound);

			levelStartTimerText.text = "Explore!";
			StartCoroutine(WaitTwoSecs());
			
			battleStartTimerActive = true;
			levelStartTimerActive = false;
		}
	}


	private void BattleStartTimer() {
		if (battleStartTime > 0.1f) {
			if (showExplorationTimer) {
				explorationTimerText.text = Mathf.Ceil(battleStartTime) + "";
			}

			battleStartTime -= Time.deltaTime;

			if (battleStartTime < lastSecondsTimeDef) {
				explorationTimerText.text = "";
				showExplorationTimer = false;

				if (!isTicking) {
					isTicking = true;
					StartCoroutine(TimeTicking());
				}

				levelStartTimerText.text = Mathf.Ceil(battleStartTime) + "";
			}

		} else {
			startBattle = true;
			isTicking = false;

			Instantiate(startBattleSound);
			levelStartTimerText.text = "Battle!";

			StartCoroutine(WaitTwoSecs());

			battleStartTimerActive = false;
		}
	}


	private IEnumerator WaitTwoSecs() {
		yield return new WaitForSeconds(2.0f);
		levelStartTimerText.text = "";
		showExplorationTimer = true;

		if (roundEnd) {
			StartCoroutine(ShowLeaderboard());
		}
	}


	private IEnumerator ShowLeaderboard() {
		yield return new WaitForSeconds(1.0f);

		// CODE FOR SHOW LEADER BOARD GOES HERE

		SceneManager.LoadScene("3 Aeras");
	}


	private void LastSecondsTimer() {
		if (lastSecondsTime > 0.1f) {
			lastSecondsTime -= Time.deltaTime;

			if (!isTicking) {
				isTicking = true;
				StartCoroutine(TimeTicking());
			}

			levelStartTimerText.text = Mathf.Ceil(lastSecondsTime) + "";
		} else {
			lastSeconds = false;
			isTicking = false;

			Instantiate(roundEndSound);

			// Give round winner 2 orbs
			if (GameManager.activePlayers.Count > 0) {
				GameObject.Find("Character" + GameManager.activePlayers[0]).GetComponent<CharacterSheet>().currentOrbs += SettingsHolder.orbsForRoundWin;
				GameObject.Find("Character" + GameManager.activePlayers[0]).GetComponent<CharacterMovement>().anim.SetBool("charWins", true);
			}

			startLevel = false;
			startBattle = false;

			SettingsHolder.currentRound++;

			if (SettingsHolder.currentRound >= SettingsHolder.amountOfRounds) {
				if (!SettingsHolder.matchOver) {
					SettingsHolder.matchOver = true;
				}
			} else {
				levelStartTimerText.text = "Round Over!";
				roundEnd = true;
				StartCoroutine(WaitTwoSecs());
			}
		}
	}


	private IEnumerator MatchOver() {
		levelStartTimerText.text = "";
		yield return new WaitForSeconds(2.0f);

		startLevel = false;
		startBattle = false;
		battleStartTimerActive = false;
		lastSeconds = false;
		roundEnd = false;
		isTicking = false;

		showResults = true;

		Instantiate(matchEndSound);

		CalculateWinner();

		yield return new WaitForSeconds(0.3f);

		startLevel = false;
		startBattle = false;

		yield return new WaitForSeconds(1.0f);

		// Save current orbs count in stats
		for (int i = 0; i < SettingsHolder.playerCount; i++) {
			GameManager.orbsCountStatsArr[i] = GameObject.Find("Character" + i).GetComponent<CharacterSheet>().currentOrbs;
		}

		Instantiate(matchResultsGO);
	}


	private void SpawnSafeZone() {
		minPosX = 0 - levelGO.transform.localScale.x / 2 + levelPadding;
		minPosZ = 0 - levelGO.transform.localScale.z / 2 + levelPadding;
		maxPosX = levelGO.transform.localScale.x / 2 - levelPadding;
		maxPosZ = levelGO.transform.localScale.z / 2 - levelPadding;

		float rndPosX = Random.Range(minPosX, maxPosX);
		float rndPosZ = Random.Range(minPosZ, maxPosZ);

		GameObject newSafeZone = Instantiate(safeZoneGO);
		newSafeZone.transform.position = new Vector3(rndPosX, newSafeZone.transform.position.y, rndPosZ);
	}


	private IEnumerator TimeTicking() {
		if (!SettingsHolder.matchOver) {
			while (isTicking) {
				Instantiate(timerTickingSound);
				yield return new WaitForSeconds(1.0f);
			}
		}
	}


	private void CalculateWinner() {
		// Save all orb counts in array
		for (int i = 0; i < SettingsHolder.playerCount; i++) {
			orbCountArr.Add(GameObject.Find("Character" + i).GetComponent<CharacterSheet>().currentOrbs);
		}

		// Find highest and lowest orb count in array
        int highestValue = orbCountArr.Max();

		// Find index of highest and lowest orb count in array
		int highestOrbIndex = orbCountArr.IndexOf(highestValue);

		// Set first place in the rankings array to the player with the most orbs
		GameManager.rankingArr[highestOrbIndex] = 1;
	}

}
