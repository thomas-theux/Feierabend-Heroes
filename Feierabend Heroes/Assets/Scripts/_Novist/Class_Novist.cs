using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Class_Novist : MonoBehaviour {

	public GameObject attackOneGO;
	public GameObject attackTwoGO;
	private CharacterSheet characterSheetScript;
	private CharacterMovement charactMovementScript;

	private Transform attackSpawner;

	private int charID;

	// These variables can be improved by advancing on the skill tree
	private float attackOne = 0.5f;
	private float attackTwo = 2.0f;

	private float rageAttackSpeed = 1.0f;

	private float attackOneDelay;
	private float attackTwoDelay;

	private float attackOneDmg = 32.0f;
	private float attackTwoDmg = 0.1f;

	private float charHealth = 280.0f;

	private float charDefense = 8.0f;

	// Movement delay when attacking
	private float moveDelay = 0.1f;
	private float moveDelayTimer;

	// Attack One – Meteor Shot
	private float attackOneDelayTimer;
	private bool attackOneDelayActive = false;

	// Attack Two – Fire Block
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
			// Check if rage mode is on and on level 3
			if (characterSheetScript.rageModeOn && characterSheetScript.rageLevel >= 3) {
				rageAttackSpeed = 2.0f;
			} else if (!characterSheetScript.rageModeOn) {
				rageAttackSpeed = 1.0f;
			}

			attackOneDelayTimer = characterSheetScript.delayAttackOne / rageAttackSpeed;
			attackOneDelayActive = true;

			GameObject newAttackOne = Instantiate(attackOneGO, attackSpawner.position, attackSpawner.rotation);
			newAttackOne.transform.GetChild(0).gameObject.tag = "Character" + charID;
			newAttackOne.tag = "Character" + charID;
			newAttackOne.transform.GetComponent<MeteorShot>().casterDamage = characterSheetScript.attackOneDmg;
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
			// Check if rage mode is on and on level 3
			if (characterSheetScript.rageModeOn && characterSheetScript.rageLevel >= 3) {
				rageAttackSpeed = 2.0f;
			} else if (!characterSheetScript.rageModeOn) {
				rageAttackSpeed = 1.0f;
			}

			attackTwoDelayTimer = characterSheetScript.delayAttackTwo / rageAttackSpeed;
			attackTwoDelayActive = true;

			GameObject newAttackTwo = Instantiate(attackTwoGO, attackSpawner.position, attackSpawner.rotation);
			newAttackTwo.transform.GetChild(0).gameObject.tag = "Character" + charID;
			newAttackTwo.tag = "Character" + charID;
			newAttackTwo.transform.GetComponent<FireBlock>().casterDamage = characterSheetScript.attackTwoDmg;
		}

		if (attackTwoDelayActive) {
			attackTwoDelayTimer -= Time.deltaTime;
			if (attackTwoDelayTimer <= 0) {
				attackTwoDelayActive = false;
			}
		}
	}

}
