using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class GameManager : MonoBehaviour {

	public GameObject characterGO;
	public GameObject spawnParent;
	public List<GameObject> startSpawns;
	public static List<int> activePlayers = new List<int>();


	private void Awake() {
		// AssignClasses();
		SpawnCharacter();
		GetComponent<CameraManager>().InstantiateCams();

		// Add players to array to see which are active
		for (int i = 0; i < SettingsHolder.playerCount; i ++) {
			activePlayers.Add(i);
		}
	}


	private void SpawnCharacter() {
		for (int i = 0; i < SettingsHolder.playerCount; i++) {
			startSpawns.Add(spawnParent.transform.GetChild(i).gameObject);

			GameObject newChar = Instantiate(characterGO);
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
	}


	// DEV TESTING
	// private void AssignClasses() {
	// 	for (int i = 0; i < SettingsHolder.playerCount; i++) {
	// 		int rndClass = Random.Range(0, 2);
	// 		SettingsHolder.playerClasses[i] = rndClass;
	// 	}
	// }
	
}
