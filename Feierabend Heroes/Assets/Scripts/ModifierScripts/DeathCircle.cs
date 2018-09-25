using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCircle : MonoBehaviour {


	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Character") {
			other.GetComponent<HealthManager>().currentHealth = 0;
		}
	}


	void Update()
	{
		if (GameManager.enableModifier) {
			transform.localScale -= new Vector3(0.05f, 0, 0.05f);
        }
	}

}
