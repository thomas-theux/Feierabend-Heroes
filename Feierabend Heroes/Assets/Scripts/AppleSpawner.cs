using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour {

	private int appleAmount;
	public GameObject appleGO;
	private GameObject levelGO;

	private float minPosX;
	private float minPosZ;
	private float maxPosX;
	private float maxPosZ;

	private float levelPadding = 5.0f;


	private void Awake() {
		// Get the level gameobject as scale reference
		levelGO = GameObject.Find("Ground");

		appleAmount = SettingsHolder.appleAmount;

		// Set limit positions for apples
		minPosX = 0 - levelGO.transform.localScale.x / 2 + levelPadding;
		minPosZ = 0 - levelGO.transform.localScale.z / 2 + levelPadding;
		maxPosX = levelGO.transform.localScale.x / 2 - levelPadding;
		maxPosZ = levelGO.transform.localScale.z / 2 - levelPadding;

		// Spawn apples randomly in level
		for (int i = 0; i < appleAmount; i++) {
			GameObject newApple = Instantiate(appleGO);

			float rndPosX = Random.Range(minPosX, maxPosX);
			float rndPosZ = Random.Range(minPosZ, maxPosZ);

			newApple.transform.position = new Vector3(rndPosX, newApple.transform.position.y, rndPosZ);
		}
	}

}
