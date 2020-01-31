using System.Collections;
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

	private float moveSpeed = 20.0f;
	private float gravityScale = 2.4f;
    private float globalGravity = -9.81f;

	private GameManager gameManagerScript;


	void Awake () {
		// ACTIVATE FOR TESTING BOMB RANGE
		// gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
		// moveSpeed = gameManagerScript.moveSpeed;
		// gravityScale = gameManagerScript.gravityScale;

		rb = GetComponent<Rigidbody>();
		rb.velocity = transform.forward * moveSpeed;
	}


	void FixedUpdate () {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }


	private void OnTriggerEnter(Collider other) {
		if (other.tag != "Attack" && other.tag != "Apple" && other.tag != "Orb" && other.tag != transform.GetChild(0).tag) {
			GameObject newBombRadius = Instantiate(bombRadius, transform.position, transform.rotation);
			BombRadius newBombRadiusScript = newBombRadius.GetComponent<BombRadius>();

			newBombRadius.transform.GetChild(0).gameObject.tag = casterTag;
			newBombRadiusScript.casterDamage = casterDamage;
			newBombRadiusScript.casterCritChance = casterCritChance;
			newBombRadiusScript.casterCritDMG = casterCritDMG;
			newBombRadiusScript.damagerID = damagerID;

			Instantiate(bombThrowHitSound);

			Destroy(gameObject);
		}
	}

}
