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
	private float spawnHeight = 3.0f;
	private float levelPadding = 3.0f;

	private float spawnDelayTime = 0;
	private float spawnMin;
	private float spawnMax;

	public int currentOrbCount = 0;
	private int orbID = 0;
	private bool initialOrbSpawns = false;


	private void Start() {
		spawnMin = 30.0f;
		spawnMax = 60.0f;

		spawnMin /= SettingsHolder.orbSpawnMax;
		spawnMax /= SettingsHolder.orbSpawnMax;

		InitialSpawns();
	}


	private void Update() {
		if (TimeHandler.startLevel && !initialOrbSpawns) {
			initialOrbSpawns = true;
		}
	}


	private void InitialSpawns() {
		for (int i = 0; i < SettingsHolder.orbSpawnMax; i++) {
			SpawnOrb();
		}
	}


	public void RespawnOrb() {
		StartCoroutine(SpawnDelay());
	}


	public IEnumerator SpawnDelay() {
		if (initialOrbSpawns) {
			spawnDelayTime = Random.Range(spawnMin, spawnMax);
		}

		yield return new WaitForSeconds(spawnDelayTime);

		SpawnOrb();
	}


	public void SpawnOrb() {
		// Set limit positions for orb chests
		minPosX = 0 - levelGO.transform.localScale.x / 2 + levelPadding;
		minPosZ = 0 - levelGO.transform.localScale.z / 2 + levelPadding;
		maxPosX = levelGO.transform.localScale.x / 2 - levelPadding;
		maxPosZ = levelGO.transform.localScale.z / 2 - levelPadding;

		float rndPosX = Random.Range(minPosX, maxPosX);
		float rndPosZ = Random.Range(minPosZ, maxPosZ);

		GameObject newOrb = Instantiate(orbChestGO);
		newOrb.name = "Orb " + orbID;
		newOrb.GetComponent<OrbChest>().orbChestSpawnerScript = GetComponent<OrbChestSpawner>();
		newOrb.transform.position = new Vector3(rndPosX, spawnHeight, rndPosZ);

		currentOrbCount++;
		orbID++;
	}

}