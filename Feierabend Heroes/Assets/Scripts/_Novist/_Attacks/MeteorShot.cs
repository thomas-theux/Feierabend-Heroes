﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShot : MonoBehaviour {

	public float casterDamage = 0;

	private Rigidbody rb;

	// These variables can be improved by advancing on the skill tree
	public float moveSpeed = 32.0f;


	void Awake () {
		rb = GetComponent<Rigidbody>();
		rb.velocity = transform.forward * moveSpeed;
		Destroy(gameObject, 3.0f);
	}


	private void OnTriggerEnter(Collider other) {
		if (other.tag != "Attack" && other.tag != transform.GetChild(0).tag) {
			if (other.tag != "Environment") {
				CalculateDamage(other);
			}
			Destroy(gameObject);
		}
	}


	private void CalculateDamage(Collider other) {
		CharacterSheet characterSheetScript = other.gameObject.GetComponent<CharacterSheet>();

		float dealDamage = 0;
		float enemyDefense = characterSheetScript.charDefense;
		int enemyDodge = characterSheetScript.dodgeChance;
		bool enemyDodgeHeal = characterSheetScript.dodgeHeal;
		int selfCrit = GetComponent<CharacterSheet>().critChance;
		int critDMG = GetComponent<CharacterSheet>().critDMG;

		int dodgeChance = Random.Range(0, 100);
		int critChance = Random.Range(0, 100);

		// Check if enemy has rage mode on
		if (characterSheetScript.rageModeOn) {
			enemyDefense *= 2;
		} else if (!characterSheetScript.rageModeOn) {
			enemyDefense *= 1;
		}

		// Check if enemy dodges attack
		if (dodgeChance > enemyDodge) {
			dealDamage = casterDamage - ((enemyDefense / 100) * casterDamage);

			// If rage mode is on and on level 2 then multiply damage
			if (characterSheetScript.rageModeOn && characterSheetScript.rageLevel >= 2) {
				dealDamage *= 2;
			} else if (!characterSheetScript.rageModeOn) {
				dealDamage *= 1;
			}

			// If character lands a critical strike then multiply damage
			if (selfCrit <= critChance) {
				dealDamage *= critDMG;
			}

			characterSheetScript.currentHealth -= dealDamage;
		} else {
			// Healing when dodging an attack
			if (enemyDodgeHeal) {
				characterSheetScript.currentHealth += characterSheetScript.currentHealth * 0.2f;
			}
			print("Enemy dodged!");
		}
	}

}