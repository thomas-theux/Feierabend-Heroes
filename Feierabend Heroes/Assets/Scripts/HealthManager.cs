using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

	private GameObject levelManager;

	public int maxHealth = 50;
	public int currentHealth;

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

			// If respawning is allowed then respawn character
			if (GameManager.allowRespawning) {
				StartCoroutine(respawnDelay());
			}
		}
	}


	IEnumerator respawnDelay () {
		yield return new WaitForSeconds(2.0f);
		levelManager.GetComponent<SpawnCharacter>().respawnChar(this.gameObject);
		respawning = false;
	}

}
