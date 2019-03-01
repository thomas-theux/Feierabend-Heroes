﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public float casterDamage = 0;
	public float casterCritChance = 0;
	public float casterCritDMG = 0;
	public string casterTag;
	public int damagerID;

	public GameObject bombThrowHitSound;

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
		if (other.tag != "Attack" && other.tag != transform.GetChild(0).tag) {
			GameObject newBombRadius = Instantiate(bombRadius, transform.position, transform.rotation);
			newBombRadius.transform.GetChild(0).gameObject.tag = casterTag;
			newBombRadius.GetComponent<BombRadius>().casterDamage = casterDamage;
			newBombRadius.GetComponent<BombRadius>().casterCritChance = casterCritChance;
			newBombRadius.GetComponent<BombRadius>().casterCritDMG = casterCritDMG;
			newBombRadius.GetComponent<BombRadius>().damagerID = damagerID;

			Instantiate(bombThrowHitSound);

			Destroy(gameObject);
		}
	}

}