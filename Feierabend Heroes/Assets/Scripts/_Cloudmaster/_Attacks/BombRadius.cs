using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRadius : MonoBehaviour {

	public float bombDamage = 0;

	private Vector3 desiredScale;
	private float destroyScale;

	private bool didDamage;

	// These variables can be improved by advancing on the skill tree
	private float smoothSpeed = 8.0f;
	private float maxRadius = 10.0f;


	private void Start() {
		desiredScale = new Vector3(maxRadius, maxRadius, maxRadius);
		destroyScale = maxRadius - 0.1f;
	}


	private void OnTriggerEnter(Collider other) {
		if (!didDamage) {
			if (other.tag != "Attack" && other.tag != "Environment" && other.tag != transform.GetChild(0).tag) {
				CalculateDamage(other);
			}
		}
	}


	private void CalculateDamage(Collider other) {
		CharacterSheet characterSheetScript = other.gameObject.GetComponent<CharacterSheet>();

		didDamage = true;

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
			dealDamage = bombDamage - ((enemyDefense / 100) * bombDamage);

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


	private void Update() {
		Vector3 smoothedScale = Vector3.Lerp(transform.localScale, desiredScale, smoothSpeed * Time.deltaTime);
		
		transform.localScale = smoothedScale;

		if (transform.localScale.x >= destroyScale) {
			Destroy(gameObject);
		}
	}

}
