using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Rewired;

public class GameManager : MonoBehaviour {

	public GameObject characterGO;
	public RuntimeAnimatorController[] classAnimations;

	public GameObject spawnParent;
	public List<GameObject> startSpawns;
	public static List<int> activePlayers = new List<int>();

	public AudioSource levelMusic;
	public AudioSource pauseGameSound;
	public AudioSource unpauseGameSound;

	public Material[] charColorsArr;

	private List<GameObject> characterArr = new List<GameObject>();

	public static int[] orbsCountStatsArr = {0, 0, 0, 0};
	public static int[] killsStatsArr = {0, 0, 0, 0};
	public static int[] deathsStatsArr = {0, 0, 0, 0};
	public static int[] orbsSpentStatsArr = {0, 0, 0, 0};
	public static List<int> rankingsArr = new List<int>();

	public GameObject pauseMenuGO;
	public static bool pauseMenuOpen = false;
	public static int whoPaused = -1;

	
	// ACTIVATE FOR TESTING BOMB RANGE
	// public float moveSpeed = 22.0f;
	// public float gravityScale = 2.2f;
	// public float bombAngle = -36.0f;


	private void Awake() {
		// AssignClasses();

		// Clear list on startup
		activePlayers.Clear();

		// Destroy dev cam
		Destroy(GameObject.Find("DevCam"));

		// Check if characters have already been spawned
		if (!SettingsHolder.initialSpawn) {
			SettingsHolder.playedFirstRound = false;
			
			SpawnCharacter();
			GetComponent<CameraManager>().InstantiateCams();
		} else {
			ResetCharacters();
		}

		GetComponent<CameraManager>().SpawnUITimer();

		AddPlayersToList();
		
		levelMusic.Play();
	}


	private void ShuffleSpawnList() {
		startSpawns.Clear();

		List<int> randomizedArray = new List<int>(){0, 1, 2, 3};

		// Fill start positions list with randomly picked positions
		for (int i = 0; i < spawnParent.transform.childCount; i++) {
			int randomIndex = Random.Range(0, randomizedArray.Count);
			startSpawns.Add(spawnParent.transform.GetChild(randomizedArray[randomIndex]).gameObject);
			randomizedArray.Remove(randomizedArray[randomIndex]);
		}
	}


	private void SpawnCharacter() {
		ShuffleSpawnList();

		for (int i = 0; i < SettingsHolder.playerCount; i++) {
			GameObject newChar = Instantiate(characterGO);
			characterArr.Add(newChar);
			newChar.transform.position = startSpawns[i].transform.position;

			newChar.GetComponent<CharacterMovement>().playerID = i;
			newChar.GetComponent<CharacterMovement>().gameManagerScript = this.gameObject.GetComponent<GameManager>();
			newChar.name = "Character" + i;
			newChar.tag = "Character" + i;
			newChar.transform.GetChild(0).tag = "Character" + i;

			newChar.GetComponent<CharacterSheet>().currentOrbs += SettingsHolder.startingOrbs;

			// Color characters
			// newChar.transform.GetChild(0).transform.GetChild(1).GetComponent<Renderer>().material = charColorsArr[i];
			// newChar.transform.GetChild(0).transform.GetChild(2).GetComponent<Renderer>().material = charColorsArr[i];

			switch(SettingsHolder.playerClasses[i]) {
				case 0:
					newChar.AddComponent<Class_Cloudmaster>();
					break;
				case 1:
					newChar.AddComponent<Class_Novist>();
					break;
			}

			newChar.transform.GetChild(0).GetComponent<Animator>().runtimeAnimatorController = classAnimations[SettingsHolder.playerClasses[i]];

			// Color the indicator underneath the hero
			newChar.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().color = Colors.keyIndicators[i];

		}
		
		// Tell the SettingsHolder that the characters have been spawned once and don't need to be spawned again
		SettingsHolder.initialSpawn = true;
	}


	private void AddPlayersToList() {
		// Add players to array to see which are active
		for (int i = 0; i < SettingsHolder.playerCount; i++) {
			activePlayers.Add(i);
		}
	}


	private void ResetCharacters() {
		ShuffleSpawnList();
		
		for (int j = 0; j < SettingsHolder.playerCount; j++) {
			GameObject findChar = GameObject.Find("Character" + j);

			findChar.transform.position = startSpawns[j].transform.position;
			findChar.GetComponent<LifeDeathHandler>().EnableCharRenderer();

			// Reset skill cooldowns
			findChar.GetComponent<CharacterSheet>().currentHealth = findChar.GetComponent<CharacterSheet>().maxHealth;
			switch (findChar.GetComponent<CharacterSheet>().charClass) {
				case 0:
					findChar.GetComponent<Class_Cloudmaster>().skillDelayTimer = 0;
					break;
				case 1:
					findChar.GetComponent<Class_Novist>().skillDelayTimer = 0;
					break;
			}

			// Give new orbs when players play next match
			if (SettingsHolder.nextMatch) {
				findChar.GetComponent<CharacterSheet>().currentOrbs = SettingsHolder.startingOrbs;
				SettingsHolder.nextMatch = false;
			}

			// Give players new orbs every round
			findChar.GetComponent<CharacterSheet>().currentOrbs += SettingsHolder.orbsEveryRound;
		}
	}


	public void HandlePauseMenu() {
		if (!pauseMenuOpen) {
			pauseMenuOpen = true;
			pauseMenuGO.SetActive(true);
			Instantiate(pauseGameSound);
			Time.timeScale = 0.0f;
		} else {
			pauseMenuOpen = false;
			pauseMenuGO.SetActive(false);
			Instantiate(unpauseGameSound);
			Time.timeScale = 1.0f;
		}
	}


	public void QuitMatch() {
		// Kill DontDestroyOnLOad game objetcts
		for (int i = 0; i < SettingsHolder.playerCount; i++) {
			Destroy(GameObject.Find("Character" + i));
			Destroy(GameObject.Find("PlayerCamera" + i));
        }
        
        SettingsHolder.matchOver = false;

        SceneManager.LoadScene("2 Character Selection");
	}
	
}
