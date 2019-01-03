using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour {

	// These variables can be improved by advancing on the skill tree
	public float healthPoints = 100.0f;
	public float defenseStat = 10.0f;


	private void Awake() {
		// Set stats in skill script
		GetComponent<CharacterSkills>().health = healthPoints;
		GetComponent<CharacterSkills>().defense = defenseStat;
	}


	private void LateUpdate() {
		KillCharacter();
	}


	private void KillCharacter() {
		if (healthPoints <= 0) {
			gameObject.SetActive(false);
		}
	}

}
