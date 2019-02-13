﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRadius : MonoBehaviour {

	public float casterDamage = 0;
	public float casterCritChance = 0;
	public float casterCritDMG = 0;

	private Vector3 desiredScale;
	private float destroyScale;

	public int damagerID;


	// These variables can be improved by advancing on the skill tree
	private float smoothSpeed = 10.0f;
	private float maxRadius = 16.0f;


	private void Start() {
		desiredScale = new Vector3(maxRadius, maxRadius, maxRadius);
		destroyScale = maxRadius - 0.1f;
	}


	private void OnTriggerEnter(Collider other) {
		if (other.tag != "Attack" && other.tag != "Apple" && other.tag != "Orb" && other.tag != "Environment" && other.tag != transform.GetChild(0).tag) {
			CalculateDamage(other);
			int charID = other.GetComponent<CharacterMovement>().playerID;
			GameObject.Find("PlayerCamera" + charID).GetComponent<CameraShake>().enabled = true;
		}
	}


	private void CalculateDamage(Collider other) {
		CharacterSheet characterSheetScript = other.gameObject.GetComponent<CharacterSheet>();

		// didDamage = true;

		float dealDamage = 0;
		float enemyDefense = characterSheetScript.charDefense;
		int enemyDodge = characterSheetScript.dodgeChance;
		bool enemyDodgeHeal = characterSheetScript.dodgeHeal;

		int dodgeChance = Random.Range(1, 101);
		int critChance = Random.Range(1, 101);

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
			if (casterCritChance <= critChance) {
				dealDamage *= casterCritDMG;
			}

			characterSheetScript.currentHealth -= dealDamage;
			other.gameObject.GetComponent<LifeDeathHandler>().lastDamagerID = damagerID;
		} else {
			// Healing when dodging an attack
			if (enemyDodgeHeal) {
				characterSheetScript.currentHealth += characterSheetScript.currentHealth * 0.2f;
			}
			print("Enemy dodged!");
		}
	}


	private void Update() {
		Vector3 smoothedScale = Vector3.Lerp(transform.localScale, desiredScale, smoothSpeed * Time.deltaTime);
		
		transform.localScale = smoothedScale;

		if (transform.localScale.x >= destroyScale) {
			Destroy(gameObject);
		}
	}

}
