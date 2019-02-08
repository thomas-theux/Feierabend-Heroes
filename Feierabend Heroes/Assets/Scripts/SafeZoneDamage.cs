using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneDamage : MonoBehaviour {

	private void OnTriggerExit(Collider other) {
		if (other.tag != "Attack" && other.tag != "Apple" && other.tag != "Orb" && other.tag != "Environment") {
			other.GetComponent<LifeDeathHandler>().isOutside = true;
		}
	}


	private void OnTriggerEnter(Collider other) {
		if (other.tag != "Attack" && other.tag != "Apple" && other.tag != "Orb" && other.tag != "Environment") {
			other.GetComponent<LifeDeathHandler>().isOutside = false;
		}
	}

}
