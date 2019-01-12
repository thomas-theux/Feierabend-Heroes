using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDeathHandler : MonoBehaviour {

	private CharacterSheet characterSheetScript;

	private float defDefense;
	private float defMoveSPD;
	private float defOneDMG;
	private float defTwoDMG;
	private float defOneSPD;
	private float defTwoSPD;

	public bool healthIsFull = true;


	private void Awake() {
		characterSheetScript = GetComponent<CharacterSheet>();
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
			if (characterSheetScript.currentHealth <= characterSheetScript.maxHealth / 10) {
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
		}
	}

}
