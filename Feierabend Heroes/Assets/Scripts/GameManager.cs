using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject characterGO;

	public static int playerCount = 4;
	public List<int> classesArr;


	private void Awake() {
		AssignClasses();

		for (int i = 0; i < playerCount; i++) {
			GameObject newChar = Instantiate(characterGO);
			newChar.transform.position = new Vector3(i * 3.0f, 1.0f, 0);
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

		GetComponent<CameraManager>().InstantiateCams();
	}


	private void AssignClasses() {
		for (int i = 0; i < playerCount; i++) {
			int rndClass = Random.Range(1, 2);
			classesArr.Add(rndClass);
		}
	}
	
}
