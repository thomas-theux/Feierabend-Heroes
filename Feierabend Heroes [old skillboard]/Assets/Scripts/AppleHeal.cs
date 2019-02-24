﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleHeal : MonoBehaviour {

	public AudioSource collectAppleSound;

	private void OnTriggerEnter(Collider other) {

		if (other.tag.Contains("Character")) {
			CharacterSheet characterSheetScript = other.GetComponent<CharacterSheet>();

			// Check if character activated the APPLE FINDING skill
			if (characterSheetScript.canFindApples) {
				if (!other.GetComponent<LifeDeathHandler>().healthIsFull) {
					Instantiate(collectAppleSound);
					
					characterSheetScript.currentHealth += characterSheetScript.maxHealth * characterSheetScript.healPercentage;
					Destroy(gameObject);
				}
			}
		}

	}
	
}
