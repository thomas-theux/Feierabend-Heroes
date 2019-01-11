using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlock : MonoBehaviour {

	public float casterDamage = 0;

	private void Awake() {
		Destroy(gameObject, 8.0f);
	}


	private void OnTriggerStay(Collider other) {
		if (other.tag != transform.GetChild(0).tag) {
			CalculateDamage(other);
		}
	}


	private void CalculateDamage(Collider other) {
		float dealDamage = 0;
		float victimDefense = other.gameObject.GetComponent<CharacterSheet>().charDefense;

		dealDamage = casterDamage - ((victimDefense / 100) * casterDamage);
		other.gameObject.GetComponent<CharacterSheet>().currentHealth -= dealDamage;
	}

}
