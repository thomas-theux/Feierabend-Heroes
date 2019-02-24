using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingBeacon : MonoBehaviour {

	public float lifeTime = 5.0f;
	public float healAmount = 0.1f;


	private void Start() {
		Destroy(transform.parent.gameObject, lifeTime);
	}


	private void OnTriggerStay(Collider other) {
		if (other.tag != "Attack" && other.tag != "Apple" && other.tag != "Orb" && other.tag != "Environment") {
			other.GetComponent<CharacterSheet>().currentHealth += healAmount;
		}
	}

}