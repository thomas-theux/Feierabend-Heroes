using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	private Rigidbody rb;
	public GameObject bombRadius;

	public float gravityScale = 2.0f;
    private float globalGravity = -9.81f;

	// These variables can be improved by advancing on the skill tree
	public float moveSpeed = 12.0f;


	void Awake () {
		rb = GetComponent<Rigidbody>();
		rb.velocity = transform.forward * moveSpeed;
	}


	void FixedUpdate () {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }


	private void OnTriggerEnter(Collider other) {
		if (other.tag != "Attack") {
			Instantiate(bombRadius, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}

}
