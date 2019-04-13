﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;

public class TimeHandler : MonoBehaviour {
	
	public static bool startLevel = false;
	public static bool startBattle = false;

	public AudioSource startExploringSound;
	public AudioSource startBattleSound;
	public AudioSource roundEndSound;
	public AudioSource timerTickingSound;

	public TMP_Text levelStartTimerText;
	public TMP_Text explorationTimerText;

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

	public GameObject leaderBoardGO;
	public GameObject rankingsParentGO;
	public GameObject rankingEntryGO;

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

		levelStartTimerText.text = "Round " + (SettingsHolder.currentRound + 1) + "/" + SettingsHolder.amountOfRounds;

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

		//////////////////////////////////////////////////////////////////////////////////////////////////////


		// Calculate all player ratios

		// Save current orbs count in stats
		for (int k = 0; k < SettingsHolder.playerCount; k++) {
			GameManager.orbsCountStatsArr[k] = GameObject.Find("Character" + k).GetComponent<CharacterSheet>().currentOrbs;
		}

		float overallKills = 0;
		float overallDeaths = 0;
		float overallOrbs = 0;

		// Defining the overall stats
		for (int j = 0; j < SettingsHolder.playerCount; j++) {
			overallKills += GameManager.killsStatsArr[j];
			overallDeaths += GameManager.killsStatsArr[j];
			overallOrbs += GameManager.orbsCountStatsArr[j];
		}

		List<float> allPlayersRatioArr = new List<float>();
		List<int> duplicateValuesArr = new List<int>();

		for (int i = 0; i < SettingsHolder.playerCount; i++) {
			// Calculate kills, deaths, and orbs ratio
			float killsRatio = GameManager.killsStatsArr[i] / overallKills;
			float deathsRatio = GameManager.deathsStatsArr[i] / overallDeaths;
			float orbsRatio = GameManager.orbsCountStatsArr[i] / overallOrbs;

			// Formula for calculating the final value
			float deathsDelta = 1 - deathsRatio;
			float multipliedKills = (killsRatio + deathsDelta) * 2;
			float finalPlayerValue = multipliedKills + orbsRatio;

			// HIER ABFRAGEN OB ES DEN WERT IM ARRAY SCHON GIBT
			if (allPlayersRatioArr.IndexOf(finalPlayerValue) != -1) {
				// Duplikate vorhanden
				// print("Player " + (allPlayersRatioArr.IndexOf(finalPlayerValue) + 1) + " has the same value as Player " + (i + 1) + ".");

				// Add player id with duplicate value to array if it doesn't exist already in this array
				if (duplicateValuesArr.IndexOf(allPlayersRatioArr.IndexOf(finalPlayerValue)) == -1) {
					duplicateValuesArr.Add(allPlayersRatioArr.IndexOf(finalPlayerValue));
				}
				duplicateValuesArr.Add(i);

			} else {
				// Keine Duplikate, allet supa
			}

			// Adding the calculated value to the overall values array
			allPlayersRatioArr.Add(finalPlayerValue);
		}

		List<float> sortedKillsArr = new List<float>();
		for (int k = 0; k < SettingsHolder.playerCount; k++) {
			sortedKillsArr.Add(allPlayersRatioArr[k]);
		}

		sortedKillsArr.Sort();
        sortedKillsArr.Reverse();

		GameManager.rankingsArr.Clear();

		// Add every player to the ranking array sorted first to last place
		for (int l = 0; l < SettingsHolder.playerCount; l++) {
			int playerIndex = allPlayersRatioArr.IndexOf(sortedKillsArr[l]);
			GameManager.rankingsArr.Add(playerIndex);
		}


		// Show player ids that have duplicates
		for (int i = 0; i < duplicateValuesArr.Count; i++) {
			print(duplicateValuesArr[i]);
		}

		// Display the ranking
		leaderBoardGO.SetActive(true);

		for (int i = 0; i < SettingsHolder.playerCount; i++) {
			GameObject newRankingEntry = Instantiate(rankingEntryGO, rankingsParentGO.transform);
			newRankingEntry.transform.localPosition = new Vector2(25, -95 - (100 * i));

			int rankingIndex = GameManager.rankingsArr[i];

			if (duplicateValuesArr.IndexOf(i) == -1) {
				newRankingEntry.transform.GetChild(0).GetComponent<TMP_Text>().text = (i + 1) + ".";
			} else {
				newRankingEntry.transform.GetChild(0).GetComponent<TMP_Text>().text = "–";
			}

			newRankingEntry.transform.GetChild(1).GetComponent<TMP_Text>().text = SettingsHolder.heroNames[rankingIndex];
			newRankingEntry.transform.GetChild(1).GetComponent<TMP_Text>().color = Colors.keyPlayers[rankingIndex];
			newRankingEntry.transform.GetChild(2).GetComponent<TMP_Text>().text = GameManager.killsStatsArr[rankingIndex] + "";
			newRankingEntry.transform.GetChild(3).GetComponent<TMP_Text>().text = GameManager.deathsStatsArr[rankingIndex] + "";
			newRankingEntry.transform.GetChild(4).GetComponent<TMP_Text>().text = GameManager.orbsCountStatsArr[rankingIndex] + "";
		}


		//////////////////////////////////////////////////////////////////////////////////////////////////////

		// Show leader board for 5 seconds
		yield return new WaitForSeconds(5.0f);

		SceneManager.LoadScene(SettingsHolder.selectedMap);
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
		GameManager.rankingsArr[highestOrbIndex] = 1;
	}

}
