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


	private void Awake() {
		characterSheetScript = GetComponent<CharacterSheet>();
	}


	private void LateUpdate() {
		CheckForRageMode();

		KillCharacter();
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

		// Activate rage mode if HP is below 10%
		// if (characterSheetScript.rageSkillActivated) {
		// 	if (characterSheetScript.currentHealth <= characterSheetScript.maxHealth / 10) {

		// 		if (characterSheetScript.rageLevel >= 0) {
		// 			defDefense = characterSheetScript.charDefense;
		// 			characterSheetScript.charDefense *= 2.0f;

		// 			if (characterSheetScript.rageLevel >= 1) {
		// 				defMoveSPD = characterSheetScript.moveSpeed;
		// 				characterSheetScript.moveSpeed *= 1.5f;

		// 				if (characterSheetScript.rageLevel >= 2) {
		// 					defOneDMG = characterSheetScript.attackOneDmg;
		// 					defTwoDMG = characterSheetScript.attackTwoDmg;
		// 					characterSheetScript.attackOneDmg *= 2.0f;
		// 					characterSheetScript.attackTwoDmg *= 2.0f;

		// 					if (characterSheetScript.rageLevel >= 3) {
		// 						defOneSPD = characterSheetScript.delayAttackOne;
		// 						defTwoSPD = characterSheetScript.delayAttackTwo;
		// 						characterSheetScript.delayAttackOne /= 2.0f;
		// 						characterSheetScript.delayAttackTwo /= 2.0f;
		// 					}
		// 				}
		// 			}
		// 		}
		// 	} else {
		// 	}
		// }
	}


	private void KillCharacter() {
		// Kill player if health is below 0
		if (characterSheetScript.currentHealth <= 0) {
			gameObject.SetActive(false);
		}
	}

}
