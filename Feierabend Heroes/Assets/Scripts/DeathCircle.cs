using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCircle : MonoBehaviour {

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Character") {
			print(other.name + " got killed!");
			other.GetComponent<HealthManager>().currentHealth = 0;
		}
	}

}
