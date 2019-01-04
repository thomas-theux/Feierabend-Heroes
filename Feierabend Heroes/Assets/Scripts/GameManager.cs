using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject characterGO;

	public static int playerCount = 4;


	private void Awake() {
		for (int i = 0; i < playerCount; i++) {
			GameObject newChar = Instantiate(characterGO);
			newChar.transform.position = new Vector3(i * 3.0f, 1.0f, 0);
			newChar.GetComponent<CharacterMovement>().playerID = i;
			newChar.name = "Character" + i;
			newChar.tag = "Character" + i;
		}
	}
	
}
