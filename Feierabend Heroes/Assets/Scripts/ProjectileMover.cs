using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour {

	public int projectileID;

	private float moveSpeed = 20f;

	private float damage;
	public float damageMin;
	public float damageMax;

	private int critHit = 1;
	public int characterLuck;


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

			// Check if the hit was a crit
			int critChance = Random.Range(0, 100);
			if (critChance < characterLuck) { critHit = 2; }
			else { critHit = 1; }

			// Apply crit damage – if applicable
			damage = (Random.Range(damageMin, damageMax) * critHit);
			
			// Get enemy defense and calculate damage
			float enemyDefense = other.GetComponent<CharacterStats>().characterDefense * 0.01f;
			damage -= damage * enemyDefense;
			damage = Mathf.Ceil(damage);

			// Hit enemy with calculated damage
			other.GetComponent<HealthManager>().getHit(damage);
		}

		if (other.tag != "Stat Point") {
			Destroy(gameObject);
		}
	}

}
