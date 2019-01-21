using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDeathHandler : MonoBehaviour {

	private CharacterSheet characterSheetScript;

	private GameObject levelGO;
	private float clearance = 5.0f;

	private float defDefense;
	private float defMoveSPD;
	private float defOneDMG;
	private float defTwoDMG;
	private float defOneSPD;
	private float defTwoSPD;

	public bool healthIsFull = true;


	private void Awake() {
		characterSheetScript = GetComponent<CharacterSheet>();
		levelGO = GameObject.Find("Ground");
	}


	private void LateUpdate() {
		// Continuously increase HP if SELF HEAL skill is activated
		if (characterSheetScript.selfHealActive && !healthIsFull) {
			SelfHeal();
		}

		HPCapCheck();

		CheckForRageMode();

		KillCharacter();
	}


	private void SelfHeal() {
		characterSheetScript.currentHealth += 0.1f;
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


	private void KillCharacter() {
		// Kill player if health is below 0
		if (characterSheetScript.currentHealth <= 0) {
			gameObject.SetActive(false);

			// Check for RESPAWN skill
			if (characterSheetScript.respawnChance > 0) {
				int rndRespawn = Random.Range(1, 101);

				if (rndRespawn > characterSheetScript.respawnChance) {
					
				} else {
					float minX = (levelGO.transform.localScale.x / 2) - levelGO.transform.localScale.x + clearance;
					float maxX = (levelGO.transform.localScale.x / 2) - clearance;
					float minZ = (levelGO.transform.localScale.z / 2) - levelGO.transform.localScale.z + clearance;
					float maxZ = (levelGO.transform.localScale.z / 2) - clearance;

					float posX = Random.Range(minX, maxX);
					float posZ = Random.Range(minZ, maxZ);

					this.gameObject.transform.position = new Vector3(posX, 1, posZ);

					characterSheetScript.currentHealth = characterSheetScript.maxHealth;

					gameObject.SetActive(true);

					// Give 1 ORB when ORB skill is perfected and character is being respawned
					if (characterSheetScript.respawnOrb) {
						characterSheetScript.currentOrbs += 1;
					}
				}
			}
		}
	}

}
