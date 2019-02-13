using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbChestSpawner : MonoBehaviour {

	public int orbChestAmount;
	public GameObject orbChestGO;
	private GameObject levelGO;

	private float minPosX;
	private float minPosZ;
	private float maxPosX;
	private float maxPosZ;

	private float levelPadding = 5.0f;


	private void Awake() {
		// Get the level gameobject as scale reference
		levelGO = GameObject.Find("Ground");

		orbChestAmount = SettingsHolder.orbChestAmount;

		// Set limit positions for orb chests
		minPosX = 0 - levelGO.transform.localScale.x / 2 + levelPadding;
		minPosZ = 0 - levelGO.transform.localScale.z / 2 + levelPadding;
		maxPosX = levelGO.transform.localScale.x / 2 - levelPadding;
		maxPosZ = levelGO.transform.localScale.z / 2 - levelPadding;

		// Spawn orb chests randomly in level
		for (int i = 0; i < orbChestAmount; i++) {
			GameObject newChest = Instantiate(orbChestGO);

			float rndPosX = Random.Range(minPosX, maxPosX);
			float rndPosZ = Random.Range(minPosZ, maxPosZ);

			newChest.transform.position = new Vector3(rndPosX, newChest.transform.position.y, rndPosZ);
		}
	}

}
