using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour {

	// These variables can be improved by advancing on the skill tree
	public float currentHealthPoints;
	public float maxHealthPoints = 100.0f;
	public float defenseStat = 10.0f;


	private void Awake() {
		currentHealthPoints = maxHealthPoints;

		// Set stats in skill script
		GetComponent<CharacterSkills>().maxHealth = maxHealthPoints;
		GetComponent<CharacterSkills>().currentHealth = currentHealthPoints;
		GetComponent<CharacterSkills>().defense = defenseStat;
	}


	private void LateUpdate() {
		KillCharacter();
	}


	private void KillCharacter() {
		if (currentHealthPoints <= 0) {
			gameObject.SetActive(false);
		}
	}

}
