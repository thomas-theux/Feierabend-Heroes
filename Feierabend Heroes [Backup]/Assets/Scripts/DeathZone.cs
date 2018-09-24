using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Character") {
			other.GetComponent<HealthManager>().currentHealth = 0;
		}
	}

}
