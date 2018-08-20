using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

	public static int playerCount = 1;
	public GameObject characterModel;

	public static float[] characterSpeed = {0f, 10f, 10f, 10f, 10f};
	public static float[] characterJumpHeight = {0f, 2f, 2f, 2f, 2f};


	void Start ()
	{
		for (int i = 1; i < playerCount + 1; i++) {
			characterModel.transform.name = "Character0" + i;
			characterModel.GetComponent<CharacterMovement>().charID = i;
			Instantiate(characterModel, new Vector3(i * 2, 1.5f, 0), Quaternion.identity);
		}	
	}

}
