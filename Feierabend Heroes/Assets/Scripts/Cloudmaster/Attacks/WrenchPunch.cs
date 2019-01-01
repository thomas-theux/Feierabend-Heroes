using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchPunch : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {
		Destroy(gameObject, 0.2f);
	}
}
