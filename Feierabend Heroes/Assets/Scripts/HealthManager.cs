using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

	private GameObject gameManager;

	public int maxHealth = 100;
	public int currentHealth;

	private bool respawning = false;
	private float waitToRespawn;


	void Start ()
	{
		gameManager = GameObject.Find("GameManager");
		currentHealth = maxHealth;
	}

	public void getHit (int damage)
	{
		currentHealth -= damage;
	}

	void Update ()
	{
		if (currentHealth <= 0 && !respawning) {
			respawning = true;
			// gameObject.SetActive(false);
			this.gameObject.GetComponent<Renderer>().enabled = false;
			this.gameObject.transform.GetChild(1).GetComponent<Renderer>().enabled = false;

			StartCoroutine(respawnDelay());
		}
	}

	IEnumerator respawnDelay () {
		yield return new WaitForSeconds(2.0f);
		gameManager.GetComponent<RespawnCharacter>().respawnChar(this.gameObject);
		respawning = false;
	}

}
