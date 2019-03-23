using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour {

	public GameObject appleGO;
	public GameObject levelGO;

	private float minPosX;
	private float minPosZ;
	private float maxPosX;
	private float maxPosZ;

	private float spawnHeight = 20.0f;

	private float levelPadding = 3.0f;

	private float waitingTime = 0.2f;
	private float timeMultiplier = 1.3f;
	public static int currentAppleCount = 0;


	private void Awake() {
		StartCoroutine(WaitToSpawn());
	}


	private IEnumerator WaitToSpawn() {
		yield return new WaitForSeconds(waitingTime);
		SpawnApple();
	}


	public void SpawnApple() {
		// Set limit positions for apples
		minPosX = 0 - levelGO.transform.localScale.x / 2 + levelPadding;
		minPosZ = 0 - levelGO.transform.localScale.z / 2 + levelPadding;
		maxPosX = levelGO.transform.localScale.x / 2 - levelPadding;
		maxPosZ = levelGO.transform.localScale.z / 2 - levelPadding;

		GameObject newApple = Instantiate(appleGO);

		float rndPosX = Random.Range(minPosX, maxPosX);
		float rndPosZ = Random.Range(minPosZ, maxPosZ);

		newApple.transform.position = new Vector3(rndPosX, spawnHeight, rndPosZ);

		currentAppleCount++;

		if (currentAppleCount < SettingsHolder.appleSpawnMax) {
			waitingTime *= timeMultiplier;
			StartCoroutine(WaitToSpawn());
		}
	}

}
