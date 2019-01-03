using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchPunch : MonoBehaviour {

	public float casterDamage = 0;


	private void Awake() {
		Destroy(transform.parent.gameObject, 0.2f);
	}


	private void OnTriggerEnter(Collider other) {
		CalculateDamage(other);
	}


	private void CalculateDamage(Collider other) {
		float dealDamage = 0;
		float victimDefense = other.gameObject.GetComponent<HealthHandler>().defenseStat;

		dealDamage = casterDamage - ((victimDefense / 100) * casterDamage);
		other.gameObject.GetComponent<HealthHandler>().healthPoints -= dealDamage;
	}
}
