using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombs : MonoBehaviour {

	public GameObject bombRadius;

	public float moveSpeed = 12.0f;


	void Awake () {
		Rigidbody rb = GetComponent<Rigidbody>();
		rb.velocity = transform.forward * moveSpeed;
	}


	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Environment") {
			Instantiate(bombRadius, transform.position, transform.rotation);
			Destroy(gameObject);
		}
		if (other.tag == "Character") {
			print("BINGO BONGO!");
		}
	}

}
