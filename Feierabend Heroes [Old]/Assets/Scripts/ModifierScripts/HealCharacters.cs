using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCharacters : MonoBehaviour {

	private float healSpeed = 0.2f;

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Character") {
			if (other.GetComponent<HealthManager>().currentHealth < other.GetComponent<HealthManager>().maxHealth) {
				other.GetComponent<HealthManager>().currentHealth += healSpeed;
			}
		}
	}

}
