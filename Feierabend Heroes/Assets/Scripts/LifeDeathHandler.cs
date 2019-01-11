using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDeathHandler : MonoBehaviour {

	private void LateUpdate() {
		KillCharacter();
	}


	private void KillCharacter() {
		if (GetComponent<CharacterSheet>().currentHealth <= 0) {
			gameObject.SetActive(false);
		}
	}

}
