using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndergroundMine : MonoBehaviour {

	public float casterDamage = 0;
	public float casterCritChance = 0;
	public float casterCritDMG = 0;
	public string casterTag;
	public int damagerID;

	public GameObject mineThrowSound;

	private Rigidbody rb;
	public GameObject mineRadius;

	private float moveSpeed = 10.0f;
	private float gravityScale = 4.0f;
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

		if (other.tag != "Attack" && other.tag != transform.GetChild(0).tag && other.tag != "Environment") {
			print("Triggered by " + other.name);
		// 	GameObject newMineRadius = Instantiate(mineRadius, transform.position, transform.rotation);
		// 	newMineRadius.transform.GetChild(0).gameObject.tag = casterTag;
		// 	newMineRadius.GetComponent<MineRadius>().casterDamage = casterDamage;
		// 	newMineRadius.GetComponent<MineRadius>().casterCritChance = casterCritChance;
		// 	newMineRadius.GetComponent<MineRadius>().casterCritDMG = casterCritDMG;
		// 	newMineRadius.GetComponent<MineRadius>().damagerID = damagerID;

			Instantiate(mineThrowSound);

			Destroy(gameObject);
		}
	}

}
