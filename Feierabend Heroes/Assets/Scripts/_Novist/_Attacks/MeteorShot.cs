using System.Collections;
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
		float dealDamage = 0;
		float victimDefense = other.gameObject.GetComponent<HealthHandler>().defenseStat;

		dealDamage = casterDamage - ((victimDefense / 100) * casterDamage);
		other.gameObject.GetComponent<HealthHandler>().currentHealthPoints -= dealDamage;
	}

}
