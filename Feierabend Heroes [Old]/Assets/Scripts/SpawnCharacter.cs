using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour {

	public GameObject characterModel;
	public GameObject cameraFollow;

	public GameObject spawnPointContainer;
	private List<GameObject> allSpawnPoints = new List<GameObject>();
	private List<GameObject> tempSpawnPoints = new List<GameObject>();


	void Start ()
	{
		// Grab all spawn points and put them in a list
		foreach (Transform child in spawnPointContainer.transform) {
			allSpawnPoints.Add(child.gameObject);
		}

		// Set temporary spawn points
		tempSpawnPoints = allSpawnPoints;

		// Spawning characters and cameras
		for (int i = 0; i < GameManager.playerCount; i++) {
			// Pick random spawn point
			int randomSpawnPoint = Random.Range(0, tempSpawnPoints.Count);

			// Instantiate a character, rename it and give it an ID
			characterModel.name = "Character0" + i;
			characterModel.GetComponent<CharacterMovement>().charID = i;
			Instantiate(characterModel, tempSpawnPoints[randomSpawnPoint].transform.position, Quaternion.identity);

			// Remove the already used random spawn point from the list
			tempSpawnPoints.RemoveAt(randomSpawnPoint);

			// Instantiate the according Camera, rename it and give it an ID
			cameraFollow.name = "Camera0" + i;
			cameraFollow.GetComponent<CameraFollow>().cameraID = i * 10;
			Instantiate(cameraFollow, new Vector3(0, 0, 0), Quaternion.identity);
		}	
	}


	public void respawnChar (GameObject respawnWho) {
		// Restore health to default
		respawnWho.GetComponent<HealthManager>().currentHealth = respawnWho.GetComponent<HealthManager>().maxHealth;

		// Place character on their spawn position
		// respawnWho.transform.position = spawnSpots[respawnWho.GetComponent<CharacterMovement>().charID];
		respawnWho.transform.position = allSpawnPoints[respawnWho.GetComponent<CharacterMovement>().charID].transform.position;

		// Activate character
		respawnWho.GetComponent<Renderer>().enabled = true;
		respawnWho.transform.GetChild(1).GetComponent<Renderer>().enabled = true;
	}

}