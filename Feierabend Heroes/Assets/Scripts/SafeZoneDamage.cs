using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneDamage : MonoBehaviour {

	private bool isOutside = false;


	private void OnTriggerExit(Collider other) {
		other.GetComponent<LifeDeathHandler>().isOutside = true;
	}


	private void OnTriggerEnter(Collider other) {
		other.GetComponent<LifeDeathHandler>().isOutside = false;
	}

}
