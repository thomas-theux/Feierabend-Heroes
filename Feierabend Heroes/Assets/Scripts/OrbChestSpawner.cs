using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbChestSpawner : MonoBehaviour {

	public GameObject orbChestGO;
	public GameObject levelGO;

	private float minPosX;
	private float minPosZ;
	private float maxPosX;
	private float maxPosZ;

	private float spawnHeight = 10.0f;

	private float levelPadding = 3.0f;

	private float waitingTime = 0.4f;
	private float timeMultiplier = 1.2f;
	public static int currentChestCount = 0;


	private void Awake() {
		StartCoroutine(WaitToSpawn());
	}


	private IEnumerator WaitToSpawn() {
		yield return new WaitForSeconds(waitingTime);
		SpawnChest();
	}


	public void SpawnChest() {
		// Set limit positions for orb chests
		minPosX = 0 - levelGO.transform.localScale.x / 2 + levelPadding;
		minPosZ = 0 - levelGO.transform.localScale.z / 2 + levelPadding;
		maxPosX = levelGO.transform.localScale.x / 2 - levelPadding;
		maxPosZ = levelGO.transform.localScale.z / 2 - levelPadding;

		GameObject newChest = Instantiate(orbChestGO);

		// DEV STUFF – Collect data on how many times an attack has been used
		SettingsHolder.orbsSpawned++;
		PlayerPrefs.SetInt("Orbs Spawned", SettingsHolder.orbsSpawned);

		float rndPosX = Random.Range(minPosX, maxPosX);
		float rndPosZ = Random.Range(minPosZ, maxPosZ);

		newChest.transform.position = new Vector3(rndPosX, spawnHeight, rndPosZ);

		currentChestCount++;

		if (currentChestCount < SettingsHolder.chestMax) {
			waitingTime *= timeMultiplier;
			StartCoroutine(WaitToSpawn());
		}
	}

}
