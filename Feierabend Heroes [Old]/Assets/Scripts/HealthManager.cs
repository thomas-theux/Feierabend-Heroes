using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

	private GameObject levelManager;

	public float maxHealth;
	public float currentHealth;
	private float displayedHealth;

	private GameObject healthBar;

	public bool isRespawning = false;
	private float waitToRespawn;


	void Start ()
	{
		levelManager = GameObject.Find("LevelManager");
		healthBar = this.gameObject.transform.GetChild(3).GetChild(1).gameObject;

		maxHealth = GetComponent<CharacterStats>().characterHealth;
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
			// Disable camera of this player
			int diedCharacterID = GetComponent<CharacterMovement>().charID;
			GameObject.Find("Camera0" + diedCharacterID + "(Clone)").GetComponent<Camera>().enabled = false;

			// Remove this character from the "still alive"-array
			GameManager.activePlayerArr.Remove(GetComponent<CharacterMovement>().charID);
			GameManager.activePlayers--;

			isRespawning = true;
			this.gameObject.GetComponent<Renderer>().enabled = false;
			this.gameObject.transform.GetChild(1).GetComponent<Renderer>().enabled = false;
			this.gameObject.transform.GetChild(3).gameObject.SetActive(false);

			// If respawning is allowed then respawn character
			if (GameManager.allowRespawning) {
				StartCoroutine(respawnDelay());
			}
		}

		// Display current health
		displayedHealth = currentHealth / maxHealth;
		healthBar.transform.localScale = new Vector3(displayedHealth, transform.localScale.y, transform.localScale.z);
	}


	IEnumerator respawnDelay () {
		yield return new WaitForSeconds(2.0f);
		levelManager.GetComponent<SpawnCharacter>().respawnChar(this.gameObject);
		isRespawning = false;
	}

}
