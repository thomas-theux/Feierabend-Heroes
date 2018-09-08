using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

	private GameObject levelManager;

	public int maxHealth = 100;
	public int currentHealth;

	public static bool respawningAllowed = true;

	private bool respawning = false;
	private float waitToRespawn;


	void Start ()
	{
		levelManager = GameObject.Find("LevelManager");
		currentHealth = maxHealth;
	}


	public void getHit (int damage)
	{
		currentHealth -= damage;
	}


	void Update ()
	{
		// If health of character is below 0 then disable them
		if (currentHealth <= 0 && !respawning) {
			respawning = true;
			this.gameObject.GetComponent<Renderer>().enabled = false;
			this.gameObject.transform.GetChild(1).GetComponent<Renderer>().enabled = false;

			StartCoroutine(respawnDelay());
		}
	}


	IEnumerator respawnDelay () {
		// If respawning is allowed then respawn character
		if (respawningAllowed) {
			yield return new WaitForSeconds(2.0f);
			levelManager.GetComponent<SpawnCharacter>().respawnChar(this.gameObject);
			respawning = false;
		}
	}

}
