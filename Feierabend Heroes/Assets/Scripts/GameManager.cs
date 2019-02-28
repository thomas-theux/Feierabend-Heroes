using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class GameManager : MonoBehaviour {

	public GameObject characterGO;
	public GameObject spawnParent;
	public List<GameObject> startSpawns;
	public static List<int> activePlayers = new List<int>();

	public Material[] charColorsArr;

	private List<GameObject> characterArr = new List<GameObject>();

	public static int[] orbsCountStatsArr = {0, 0, 0, 0};
	public static int[] killsStatsArr = {0, 0, 0, 0};
	public static int[] deathsStatsArr = {0, 0, 0, 0};
	public static int[] orbsSpentStatsArr = {0, 0, 0, 0};
	public static int[] rankingArr = {0, 0, 0, 0};


	private void Awake() {
		// AssignClasses();

		// Clear list on startup
		activePlayers.Clear();

		// Destroy dev cam
		Destroy(GameObject.Find("DevCam"));

		// Check if characters have already been spawned
		if (!SettingsHolder.initialSpawn) {
			SpawnCharacter();
			GetComponent<CameraManager>().InstantiateCams();
		} else {
			ResetCharacters();
		}

		AddPlayersToList();
	}


	private void SpawnCharacter() {
		for (int i = 0; i < SettingsHolder.playerCount; i++) {
			startSpawns.Add(spawnParent.transform.GetChild(i).gameObject);

			GameObject newChar = Instantiate(characterGO);
			characterArr.Add(newChar);
			newChar.transform.position = startSpawns[i].transform.position;

			newChar.GetComponent<CharacterMovement>().playerID = i;
			newChar.name = "Character" + i;
			newChar.tag = "Character" + i;
			newChar.transform.GetChild(0).tag = "Character" + i;

			newChar.GetComponent<CharacterSheet>().currentOrbs += SettingsHolder.startingOrbs;

			// Color characters
			newChar.transform.GetChild(0).transform.GetChild(1).GetComponent<Renderer>().material = charColorsArr[i];
			newChar.transform.GetChild(0).transform.GetChild(2).GetComponent<Renderer>().material = charColorsArr[i];

			switch(SettingsHolder.playerClasses[i]) {
				case 0:
					newChar.AddComponent<Class_Cloudmaster>();
					newChar.AddComponent<Class_Novist>();
					break;
				case 1:
					break;
			}
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
		for (int j = 0; j < SettingsHolder.playerCount; j++) {
			startSpawns.Add(spawnParent.transform.GetChild(j).gameObject);
			GameObject findChar = GameObject.Find("Character" + j);

			findChar.transform.position = startSpawns[j].transform.position;
			findChar.GetComponent<LifeDeathHandler>().EnableCharRenderer();

			findChar.GetComponent<CharacterSheet>().currentHealth = findChar.GetComponent<CharacterSheet>().maxHealth;

			// Give new orbs when players play next match
			if (SettingsHolder.nextMatch) {
				findChar.GetComponent<CharacterSheet>().currentOrbs = SettingsHolder.startingOrbs;
				SettingsHolder.nextMatch = false;
			}

			// Give players new orbs every round
			// findChar.GetComponent<CharacterSheet>().currentOrbs += SettingsHolder.orbsEveryRound;
		}
	}


	// DEV TESTING
	// private void AssignClasses() {
	// 	for (int i = 0; i < SettingsHolder.playerCount; i++) {
	// 		int rndClass = Random.Range(0, 2);
	// 		SettingsHolder.playerClasses[i] = rndClass;
	// 	}
	// }
	
}
