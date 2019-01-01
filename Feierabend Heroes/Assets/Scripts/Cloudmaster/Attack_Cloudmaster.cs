using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Attack_Cloudmaster : MonoBehaviour {

	public GameObject punchGO;
	public GameObject bombsGO;
	private CharacterSkills skillsScript;

	public Transform attackSpawner;

	// These variables can be improved by advancing on the skill tree
	private float startPunchDelay = 0.4f;
	private float startBombDelay = 0.6f;
	private float punchDelay;
	private float bombDelay;

	// Movement delay when attacking
	private float moveDelay = 0.1f;
	private float moveDelayTimer;

	// Attack Two – Wrench Punch
	private float punchDelayTimer;
	private bool punchDelayActive = false;

	// Attack One – Bombs
	private float bombDelayTimer;
	private bool bombDelayActive = false;

	// REWIRED
	private Player player;
	private bool performPunch;
	private bool fireBomb;


	private void Awake() {
		int charID = GetComponent<CharacterMovement>().playerID;
		player = ReInput.players.GetPlayer(charID);

		// Set stats in skill script
		GetComponent<CharacterSkills>().delayAttackOne = startPunchDelay;
		GetComponent<CharacterSkills>().delayAttackTwo = startBombDelay;

		// Get stats from skill script
		skillsScript = GetComponent<CharacterSkills>();
	}


	private void Update() {
		GetInput();

		AttackOne();
		AttackTwo();

		DelayMovement();
	}


	private void GetInput() {
		performPunch = player.GetButton("X");
		fireBomb = player.GetButton("Square");
	}


	private void DelayMovement() {
		if (performPunch || fireBomb) {
			moveDelayTimer = moveDelay;
		}

		if (moveDelayTimer > 0) {
			GetComponent<CharacterMovement>().isAttacking = true;
			moveDelayTimer -= Time.deltaTime;
		} else if (moveDelayTimer <= 0 && GetComponent<CharacterMovement>().isAttacking) {
			GetComponent<CharacterMovement>().isAttacking = false;
		}
	}


	private void AttackOne() {
		if (performPunch && !punchDelayActive) {
			punchDelayTimer = skillsScript.delayAttackOne;
			punchDelayActive = true;

			GameObject newPunch = Instantiate(punchGO, attackSpawner.position, attackSpawner.rotation);
			newPunch.transform.parent = gameObject.transform;
		}

		if (punchDelayActive) {
			punchDelayTimer -= Time.deltaTime;
			if (punchDelayTimer <= 0) {
				punchDelayActive = false;
			}
		}
	}


	private void AttackTwo() {
		if (fireBomb && !bombDelayActive) {
			bombDelayTimer = skillsScript.delayAttackTwo;
			bombDelayActive = true;
			
			attackSpawner.transform.rotation = transform.rotation * Quaternion.Euler(-35, 0, 0);
			Instantiate(bombsGO, attackSpawner.position, attackSpawner.rotation);
			attackSpawner.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 0);
		}

		if (bombDelayActive) {
			bombDelayTimer -= Time.deltaTime;
			if (bombDelayTimer <= 0) {
				bombDelayActive = false;
			}
		}
	}

}
