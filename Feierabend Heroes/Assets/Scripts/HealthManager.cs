using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

	private GameObject levelManager;

	public int maxHealth;
	public float currentHealth;
	private float displayedHealth;

	private GameObject healthBar;

	private bool isRespawning = false;
	private float waitToRespawn;


	void Start ()
	{
		levelManager = GameObject.Find("LevelManager");
		healthBar = this.gameObject.transform.GetChild(3).GetChild(1).gameObject;

		maxHealth = 100;
		currentHealth = maxHealth;
	}


	public void getHit (float damage)
	{
		currentHealth -= damage;
	}


	void Update ()
	{
		// If health of character is below 0 then disable them
		if (currentHealth <= 0 && !isRespawning) {
			GameManager.activePlayers--;
			isRespawning = true;
			this.gameObject.GetComponent<Renderer>().enabled = false;
			this.gameObject.transform.GetChild(1).GetComponent<Renderer>().enabled = false;

			// If respawning is allowed then respawn character
			if (GameManager.allowRespawning) {
				StartCoroutine(respawnDelay());
			}
		}

		// Display current health
		displayedHealth = currentHealth / 100;
		healthBar.transform.localScale = new Vector3(displayedHealth, transform.localScale.y, transform.localScale.z);
	}


	IEnumerator respawnDelay () {
		yield return new WaitForSeconds(2.0f);
		levelManager.GetComponent<SpawnCharacter>().respawnChar(this.gameObject);
		isRespawning = false;
	}

}
