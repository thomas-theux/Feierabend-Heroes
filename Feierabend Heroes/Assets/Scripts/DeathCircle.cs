using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCircle : MonoBehaviour {


	void Start()
	{
		GameManager.enableModifier = true;
	}


	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Character") {
			print("Exited the trigger");
			other.GetComponent<HealthManager>().currentHealth = 0;
		}
	}


	void Update()
	{
		if (GameManager.enableModifier) {
			transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
        }
	}

}
