using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShot : MonoBehaviour {

	public float casterDamage = 0;
	public float casterCritChance = 0;
	public float casterCritDMG = 0;
	public int damagerID;

	public GameObject meteorShotHitSound;

	private Rigidbody rb;

	// These variables can be improved by advancing on the skill tree
	public float moveSpeed = 32.0f;


	void Awake () {
		rb = GetComponent<Rigidbody>();
		Destroy(gameObject, 3.0f);
	}


	private void Update() {
		rb.velocity = transform.forward * moveSpeed;
	}


	private void OnTriggerEnter(Collider other) {
		if (other.tag != "Attack" && other.tag != "Apple" && other.tag != "Orb" && other.tag != transform.GetChild(0).tag) {
			if (other.tag != "Environment") {
				CalculateDamage(other);
				int charID = other.GetComponent<CharacterMovement>().playerID;
				GameObject.Find("PlayerCamera" + charID).GetComponent<CameraShake>().enabled = true;
			}

			Instantiate(meteorShotHitSound);
			Destroy(gameObject);
		}
	}


	private void CalculateDamage(Collider other) {
		CharacterSheet characterSheetScript = other.gameObject.GetComponent<CharacterSheet>();

		float dealDamage = 0;
		float enemyDefense = characterSheetScript.charDefense;
		int enemyDodge = characterSheetScript.dodgeChance;

		int dodgeChance = Random.Range(1, 101);
		int critChance = Random.Range(1, 101);

		// Check if enemy dodges attack
		if (dodgeChance > enemyDodge) {
			// dealDamage = casterDamage - ((enemyDefense / 100) * casterDamage);	// my old formula – heals player if def > 100
			// dealDamage = casterDamage * casterDamage / (casterDamage + enemyDefense);
			dealDamage = casterDamage * (100 / (100 + enemyDefense));

			// If character lands a critical strike then multiply damage
			if (casterCritChance <= critChance) {
				dealDamage *= casterCritDMG;
			}

			characterSheetScript.currentHealth -= dealDamage;
			other.gameObject.GetComponent<LifeDeathHandler>().lastDamagerID = damagerID;
			other.GetComponent<LifeDeathHandler>().gotHit = true;
		}
	}

}
