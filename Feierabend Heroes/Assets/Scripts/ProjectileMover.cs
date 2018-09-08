using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour {

	private float moveSpeed = 20f;

	private float damage;
	private float damageMin = 8;
	private float damageMax = 12;


	void Start ()
	{
		Rigidbody rb = GetComponent<Rigidbody>();
		rb.velocity = transform.forward * moveSpeed;
		Destroy(gameObject, 5);
	}

	void OnTriggerEnter(Collider other)
	{
		// If the other object is a character, they are being damaged
		if (other.tag == "Character") {
			damage = Random.Range(damageMin, damageMax);
			other.GetComponent<HealthManager>().getHit(damage);
		}

		Destroy(gameObject);
	}

}
