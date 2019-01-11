using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Class_Cloudmaster : MonoBehaviour {

	public GameObject attackOneGO;
	public GameObject attackTwoGO;
	private CharacterSheet characterSheetScript;
	private CharacterMovement charactMovementScript;

	private Transform attackSpawner;

	private int charID;

	// These variables can be improved by advancing on the skill tree
	private float attackOne = 0.4f;
	private float attackTwo = 0.6f;

	private float attackOneDelay;
	private float attackTwoDelay;

	private float attackOneDmg = 24.0f;
	private float attackTwoDmg = 50.0f;

	private float charHealth = 340.0f;
	
	private float charDefense = 12.0f;

	// Movement delay when attacking
	private float moveDelay = 0.1f;
	private float moveDelayTimer;

	// Attack One – Wrench Punch
	private float attackOneDelayTimer;
	private bool attackOneDelayActive = false;

	// Attack Two – Bombs
	private float attackTwoDelayTimer;
	private bool attackTwoDelayActive = false;

	// REWIRED
	private Player player;
	private bool performAttackOne;
	private bool performAttackTwo;


	private void Start() {
		// Get stats from skill script
		characterSheetScript = GetComponent<CharacterSheet>();
		charactMovementScript = GetComponent<CharacterMovement>();

		// Get gameobjects for classes
		attackSpawner = transform.GetChild(1).gameObject.transform;

		charID = charactMovementScript.playerID;
		player = ReInput.players.GetPlayer(charID);

		// Set stats in skill script
		characterSheetScript.delayAttackOne = attackOne;
		characterSheetScript.delayAttackTwo = attackTwo;

		characterSheetScript.attackOneDmg = attackOneDmg;
		characterSheetScript.attackTwoDmg = attackTwoDmg;

		characterSheetScript.currentHealth = charHealth;
		characterSheetScript.maxHealth = charHealth;

		characterSheetScript.charDefense = charDefense;
	}


	private void Update() {
		if (!charactMovementScript.skillBoardOn) {
			GetInput();
		}

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
			charactMovementScript.isAttacking = true;
			moveDelayTimer -= Time.deltaTime;
		} else if (moveDelayTimer <= 0 && charactMovementScript.isAttacking) {
			charactMovementScript.isAttacking = false;
		}
	}


	private void AttackOne() {
		if (performAttackOne && !attackOneDelayActive) {
			attackOneDelayTimer = characterSheetScript.delayAttackOne;
			attackOneDelayActive = true;

			GameObject newAttackOne = Instantiate(attackOneGO, attackSpawner.position, attackSpawner.rotation);
			newAttackOne.transform.GetChild(0).gameObject.tag = "Character" + charID;
			newAttackOne.transform.parent = gameObject.transform;
			newAttackOne.GetComponent<WrenchPunch>().casterDamage = characterSheetScript.attackOneDmg;
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
			attackTwoDelayTimer = characterSheetScript.delayAttackTwo;
			attackTwoDelayActive = true;
			
			attackSpawner.transform.rotation = transform.rotation * Quaternion.Euler(-35, 0, 0);
			GameObject newAttackTwo = Instantiate(attackTwoGO, attackSpawner.position, attackSpawner.rotation);
			newAttackTwo.GetComponent<Bomb>().casterTag = "Character" + charID;
			newAttackTwo.transform.GetComponent<Bomb>().casterDamage = characterSheetScript.attackTwoDmg;
			attackSpawner.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 0);
		}

		if (attackTwoDelayActive) {
			attackTwoDelayTimer -= Time.deltaTime;
			if (attackTwoDelayTimer <= 0) {
				attackTwoDelayActive = false;
			}
		}
	}

}
