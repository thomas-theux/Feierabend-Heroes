using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCharacter : MonoBehaviour {

	public Vector3[] spawnSpots = {
		new Vector3(-3.0f, 3.0f, -19.0f),
		new Vector3(16.0f, 3.0f, -12.0f),
		new Vector3(12.0f, 3.0f, 6.0f),
		new Vector3(-9.0f, 3.0f, 6.0f)
	};

	public void respawnChar (GameObject respawnWho) {
		// Restore health to default
		respawnWho.GetComponent<HealthManager>().currentHealth = respawnWho.GetComponent<HealthManager>().maxHealth;

		// Place character on their spawn position
		respawnWho.transform.position = spawnSpots[respawnWho.GetComponent<CharacterMovement>().charID];

		// Activate character
		respawnWho.GetComponent<Renderer>().enabled = true;
		respawnWho.transform.GetChild(1).GetComponent<Renderer>().enabled = true;
	}

}
