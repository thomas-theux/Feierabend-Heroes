using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour {

	public int projectileID;

	private float moveSpeed = 20f;

	private float damage;
	public float damageMin;
	public float damageMax;


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
