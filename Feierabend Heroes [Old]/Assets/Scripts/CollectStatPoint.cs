using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectStatPoint : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Character") {
			other.GetComponent<CharacterStats>().currentStatPoints++;
			Destroy(gameObject);
		}
	}

}
