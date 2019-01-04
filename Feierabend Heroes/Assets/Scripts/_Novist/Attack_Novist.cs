using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Attack_Novist : MonoBehaviour {

	public GameObject attackOneGO;
	public GameObject attackTwoGO;
	private CharacterSkills skillsScript;

	public Transform attackSpawner;

	private int charID;

	// These variables can be improved by advancing on the skill tree
	private float attackOne = 0.5f;
	private float attackTwo = 2.0f;
	private float attackOneDelay;
	private float attackTwoDelay;
	private float attackOneDmg = 10.0f;
	private float attackTwoDmg = 0.05f;

	// Movement delay when attacking
	private float moveDelay = 0.1f;
	private float moveDelayTimer;

	// Attack One – Meteor Shot
	private float attackOneDelayTimer;
	private bool attackOneDelayActive = false;

	// Attack Two – ???
	private float attackTwoDelayTimer;
	private bool attackTwoDelayActive = false;

	// REWIRED
	private Player player;
	private bool performAttackOne;
	private bool performAttackTwo;


	private void Start() {
		charID = GetComponent<CharacterMovement>().playerID;
		player = ReInput.players.GetPlayer(charID);

		// Set stats in skill script
		GetComponent<CharacterSkills>().delayAttackOne = attackOne;
		GetComponent<CharacterSkills>().delayAttackTwo = attackTwo;

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
		performAttackOne = player.GetButton("X");
		performAttackTwo = player.GetButton("Square");
	}


	private void DelayMovement() {
		if (performAttackOne || performAttackTwo) {
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
		if (performAttackOne && !attackOneDelayActive) {
			attackOneDelayTimer = skillsScript.delayAttackOne;
			attackOneDelayActive = true;

			GameObject newAttackOne = Instantiate(attackOneGO, attackSpawner.position, attackSpawner.rotation);
			newAttackOne.transform.GetChild(0).gameObject.tag = "Character" + charID;
			newAttackOne.transform.GetComponent<MeteorShot>().casterDamage = attackOneDmg;
		}

		if (attackOneDelayActive) {
			attackOneDelayTimer -= Time.deltaTime;
			if (attackOneDelayTimer <= 0) {
				attackOneDelayActive = false;
			}
		}
	}


	private void AttackTwo() {
		if (performAttackTwo && !attackTwoDelayActive) {
			attackTwoDelayTimer = skillsScript.delayAttackTwo;
			attackTwoDelayActive = true;

			GameObject newAttackTwo = Instantiate(attackTwoGO, attackSpawner.position, attackSpawner.rotation);
			newAttackTwo.transform.GetChild(0).gameObject.tag = "Character" + charID;
			newAttackTwo.transform.GetComponent<FireBlock>().casterDamage = attackTwoDmg;
		}

		if (attackTwoDelayActive) {
			attackTwoDelayTimer -= Time.deltaTime;
			if (attackTwoDelayTimer <= 0) {
				attackTwoDelayActive = false;
			}
		}
	}

}
