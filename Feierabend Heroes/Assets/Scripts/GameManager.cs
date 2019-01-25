using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject characterGO;
	public GameObject spawnParent;
	public List<GameObject> startSpawns;

	public static int playerCount = 4;
	public List<int> classesArr;


	private void Awake() {
		AssignClasses();

		SpawnCharacter();

		GetComponent<CameraManager>().InstantiateCams();
		// GetComponent<CameraFirstPerson>().InstantiateCams();
	}


	private void SpawnCharacter() {
		for (int i = 0; i < playerCount; i++) {
			startSpawns.Add(spawnParent.transform.GetChild(i).gameObject);

			GameObject newChar = Instantiate(characterGO);
			newChar.transform.position = startSpawns[i].transform.position;
			newChar.GetComponent<CharacterMovement>().playerID = i;
			newChar.name = "Character" + i;
			newChar.tag = "Character" + i;

			switch(classesArr[i]) {
				case 0:
					newChar.AddComponent<Class_Cloudmaster>();
					break;
				case 1:
					newChar.AddComponent<Class_Novist>();
					break;
			}
		}
	}


	private void AssignClasses() {
		for (int i = 0; i < playerCount; i++) {
			int rndClass = Random.Range(1, 2);
			classesArr.Add(rndClass);
		}
	}
	
}
