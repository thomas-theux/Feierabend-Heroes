using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class GameManager : MonoBehaviour {

	public GameObject characterGO;
	public GameObject spawnParent;
	public List<GameObject> startSpawns;
	public static List<int> activePlayers = new List<int>();

	// private List<GameObject> characterArr = new List<GameObject>();


	private void Awake() {
		// AssignClasses();

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
			SettingsHolder.characterArr.Add(newChar);
			newChar.transform.position = startSpawns[i].transform.position;

			newChar.GetComponent<CharacterMovement>().playerID = i;
			newChar.name = "Character" + i;
			newChar.tag = "Character" + i;
			newChar.transform.GetChild(0).tag = "Character" + i;

			switch(SettingsHolder.playerClasses[i]) {
				case 0:
					newChar.AddComponent<Class_Cloudmaster>();
					break;
				case 1:
					newChar.AddComponent<Class_Novist>();
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
			SettingsHolder.charUIArr[j].GetComponent<CharacterSheet>().currentHealth = SettingsHolder.charUIArr[j].GetComponent<CharacterSheet>().maxHealth;
			startSpawns.Add(spawnParent.transform.GetChild(j).gameObject);

			SettingsHolder.characterArr[j].transform.position = startSpawns[j].transform.position;
			SettingsHolder.characterArr[j].GetComponent<LifeDeathHandler>().EnableCharRenderer();
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
