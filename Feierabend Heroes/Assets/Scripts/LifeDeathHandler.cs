using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeDeathHandler : MonoBehaviour {

	private CharacterSheet characterSheetScript;

	public GameObject dudeHeadGO;
	public GameObject dudeBodyGO;

	private GameObject levelGO;
	private float clearance = 5.0f;

	private float defDefense;
	private float defMoveSPD;
	private float defOneDMG;
	private float defTwoDMG;
	private float defOneSPD;
	private float defTwoSPD;

	public bool healthIsFull = true;
	public bool isOutside = false;

	private bool isWaiting = false;
	public bool charIsDead = false;


	private void Awake() {
		characterSheetScript = GetComponent<CharacterSheet>();
		levelGO = GameObject.Find("Ground");
	}


	void OnEnable() {
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}
         
	void OnDisable() {
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}


	private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
		// Reset all bools
		healthIsFull = true;
		isOutside = false;
		isWaiting = false;
		charIsDead = false;
	}


	private void LateUpdate() {
		// Continuously increase HP if SELF HEAL skill is activated
		if (characterSheetScript.selfHealActive && !healthIsFull) {
			SelfHeal();
		}

		// Damage character when outside of safe zone
		if (isOutside && characterSheetScript.currentHealth > 0) {
			OutsideSafeZone();
		}

		HPCapCheck();

		CheckForRageMode();

		if (characterSheetScript.currentHealth <= 0 && !isWaiting && !charIsDead) {
			StartCoroutine(KillCharacter());
		}
	}


	private void SelfHeal() {
		characterSheetScript.currentHealth += 10.0f * Time.deltaTime;
	}


	public void OutsideSafeZone() {
		characterSheetScript.currentHealth -= 24.0f * Time.deltaTime;
	}


	private void HPCapCheck() {
		// Cap the current HP if higher than max HP
		if (characterSheetScript.currentHealth > characterSheetScript.maxHealth && !healthIsFull) {
			characterSheetScript.currentHealth = characterSheetScript.maxHealth;
			healthIsFull = true;
		} else if (characterSheetScript.currentHealth < characterSheetScript.maxHealth && healthIsFull) {
			healthIsFull = false;
		}
	}


	private void CheckForRageMode() {
		// Activate rage mode if HP is below 10%
		if (characterSheetScript.rageSkillActivated) {
			if (characterSheetScript.currentHealth <= characterSheetScript.maxHealth / 5) {
				characterSheetScript.rageModeOn = true;
			} else {
				characterSheetScript.rageModeOn = false;
			}
		}
	}


	private IEnumerator KillCharacter() {
		isWaiting = true;
		charIsDead = true;
		// gameObject.SetActive(false);
		dudeHeadGO.GetComponent<Renderer>().enabled = false;
		dudeBodyGO.GetComponent<Renderer>().enabled = false;
		gameObject.GetComponent<CapsuleCollider>().enabled = false;

		yield return new WaitForSeconds(2.0f);

		CheckForRespawn();
	}


	private void CheckForRespawn() {
		// Check for RESPAWN skill
		if (characterSheetScript.respawnChance > 0) {
			int rndRespawn = Random.Range(1, 101);

			if (rndRespawn > characterSheetScript.respawnChance) {
				RemoveFromList();
			} else {
				float minX = (levelGO.transform.localScale.x / 2) - levelGO.transform.localScale.x + clearance;
				float maxX = (levelGO.transform.localScale.x / 2) - clearance;
				float minZ = (levelGO.transform.localScale.z / 2) - levelGO.transform.localScale.z + clearance;
				float maxZ = (levelGO.transform.localScale.z / 2) - clearance;

				float posX = Random.Range(minX, maxX);
				float posZ = Random.Range(minZ, maxZ);

				this.gameObject.transform.position = new Vector3(posX, 1, posZ);

				characterSheetScript.currentHealth = characterSheetScript.maxHealth;

				isWaiting = false;
				charIsDead = false;

				// gameObject.SetActive(true);
				EnableCharRenderer();

				// Give 1 ORB when ORB skill is perfected and character is being respawned
				if (characterSheetScript.respawnOrb) {
					characterSheetScript.currentOrbs += 1;
				}
			}
		} else {
			RemoveFromList();
		}
	}


	private void RemoveFromList() {
		// Remove the just killed character from the player array
		GameManager.activePlayers.Remove(characterSheetScript.charID);
		if (GameManager.activePlayers.Count == 1) {
			TimeHandler.lastSeconds = true;
		}
	}


	public void EnableCharRenderer() {
		dudeHeadGO.GetComponent<Renderer>().enabled = true;
		dudeBodyGO.GetComponent<Renderer>().enabled = true;
		gameObject.GetComponent<CapsuleCollider>().enabled = true;
	}

}
