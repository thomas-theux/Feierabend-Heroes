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

	private bool setStraight = false;
	private bool isActive = false;

	private GameManager gameManagerScript;


	private void Awake () {
		// ACTIVATE FOR TESTING BOMB RANGE
		// gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
		// moveSpeed = gameManagerScript.moveSpeed;
		// gravityScale = gameManagerScript.gravityScale;

		rb = GetComponent<Rigidbody>();
		rb.velocity = transform.forward * moveSpeed;
	}


	private void FixedUpdate () {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }


	private void OnTriggerStay(Collider other) {
		if (!setStraight) {
			if (this.transform.position.y < 0.1f) {
				setStraight = true;

				// Set y position to 0
				this.transform.position = new Vector3(
					this.transform.position.x,
					0,
					this.transform.position.z
				);

				// Reset rotation
				this.transform.rotation = Quaternion.identity;

				// Set mine to isKinematic
				this.GetComponent<Rigidbody>().isKinematic = true;

				// Set trigger height to 100
				this.GetComponent<CapsuleCollider>().height = 100.0f;

				StartCoroutine(HideMine());
			}
		}
	}


	private void OnTriggerEnter(Collider other) {
		if (isActive) {
			if (other.tag != "Attack" && other.tag != transform.GetChild(0).tag && other.tag != "Environment" && other.tag != "Orb") {

				GameObject newMineRadius = Instantiate(mineRadius, transform.position, transform.rotation);
				newMineRadius.transform.GetChild(0).gameObject.tag = casterTag;
				newMineRadius.GetComponent<MineRadius>().casterDamage = casterDamage;
				newMineRadius.GetComponent<MineRadius>().casterCritChance = casterCritChance;
				newMineRadius.GetComponent<MineRadius>().casterCritDMG = casterCritDMG;
				newMineRadius.GetComponent<MineRadius>().damagerID = damagerID;

				Instantiate(mineThrowSound);

				Destroy(gameObject);
			}
		}
	}


	private void OnTriggerExit(Collider other) {
		if (isActive) {
			if (other.tag != "Attack" && other.tag != transform.GetChild(0).tag && other.tag != "Environment" && other.tag != "Orb") {

				GameObject newMineRadius = Instantiate(mineRadius, transform.position, transform.rotation);
				newMineRadius.transform.GetChild(0).gameObject.tag = casterTag;
				newMineRadius.GetComponent<MineRadius>().casterDamage = casterDamage;
				newMineRadius.GetComponent<MineRadius>().casterCritChance = casterCritChance;
				newMineRadius.GetComponent<MineRadius>().casterCritDMG = casterCritDMG;
				newMineRadius.GetComponent<MineRadius>().damagerID = damagerID;

				Instantiate(mineThrowSound);

				Destroy(gameObject);
			}
		}
	}
	

	private IEnumerator HideMine() {
		yield return new WaitForSeconds(0.5f);

		this.gameObject.layer = damagerID + 10;
		isActive = true;
	}
}
