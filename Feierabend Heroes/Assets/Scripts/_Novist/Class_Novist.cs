using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Class_Novist : MonoBehaviour {

	public GameObject attackOneGO;
	public GameObject attackTwoGO;
	public GameObject skillTwoGO;
	private CharacterSheet characterSheetScript;
	private CharacterMovement charactMovementScript;

	private Transform attackSpawner;

	private int charID;

	// These variables can be improved by advancing on the skill tree
	private float attackOne = 0.5f;
	private float attackTwo = 10.0f;
	private float skillOne = 18.0f;
	private float skillTwo = 20.0f;

	private float rageAttackSpeed = 1.0f;

	private float attackOneDelay;
	private float attackTwoDelay;

	private float attackOneDmg = 20.0f;
	private float attackTwoDmg = 0.08f;
	private float skillOneDmg = 4.0f;
	private float skillTwoDmg = 6.0f;

	private float charHealth = 240.0f;

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

	// Skill One – Double HP
	private float skillOneDelayTimer;
	private bool skillOneDelayActive = false;

	// Skill Two – Spawn Companion
	private float skillTwoDelayTimer;
	private bool skillTwoDelayActive = false;

	// REWIRED
	private Player player;
	private bool performAttackOne;
	private bool performAttackTwo;
	private bool castSkill;


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
		characterSheetScript.delaySkillOne = skillOne;
		characterSheetScript.delaySkillTwo = skillTwo;

		characterSheetScript.attackOneDmg = attackOneDmg;
		characterSheetScript.attackTwoDmg = attackTwoDmg;
		characterSheetScript.skillOneDmg = skillOneDmg;
		characterSheetScript.skillTwoDmg = skillTwoDmg;

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

		if (characterSheetScript.skillActivated == 1) {
			SkillOne();
		} else if (characterSheetScript.skillActivated == 2) {
			SkillTwo();
		}

		DelayMovement();
	}


	private void GetInput() {
		performAttackOne = player.GetButton("X");
		performAttackTwo = player.GetButton("Square");

		castSkill = player.GetButtonDown("Circle");
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
			newAttackOne.tag = "Attack";
			newAttackOne.transform.GetComponent<MeteorShot>().casterDamage = characterSheetScript.attackOneDmg;
			newAttackOne.GetComponent<MeteorShot>().casterCritChance = characterSheetScript.critChance;
			newAttackOne.GetComponent<MeteorShot>().casterCritDMG = characterSheetScript.critDMG;
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

			attackSpawner.transform.localPosition = attackSpawner.transform.localPosition + new Vector3(0, 0, 2);
			GameObject newAttackTwo = Instantiate(attackTwoGO, attackSpawner.position, attackSpawner.rotation);
			newAttackTwo.transform.GetChild(0).gameObject.tag = "Character" + charID;
			newAttackTwo.tag = "Attack";
			newAttackTwo.transform.GetComponent<FireBlock>().casterDamage = characterSheetScript.attackTwoDmg;
			newAttackTwo.GetComponent<FireBlock>().casterCritChance = characterSheetScript.critChance;
			newAttackTwo.GetComponent<FireBlock>().casterCritDMG = characterSheetScript.critDMG;
			attackSpawner.transform.localPosition = attackSpawner.transform.localPosition - new Vector3(0, 0, 2);
		}

		if (attackTwoDelayActive) {
			attackTwoDelayTimer -= Time.deltaTime;
			if (attackTwoDelayTimer <= 0) {
				attackTwoDelayActive = false;
			}
		}
	}


	private void SkillOne() {
		if (castSkill && characterSheetScript.skillActivated == 1 && !skillOneDelayActive) {

			skillOneDelayTimer = characterSheetScript.delaySkillOne;
			skillOneDelayActive = true;

			// Skill DOUBLE HP
		}

		if (skillOneDelayActive) {
			skillOneDelayTimer -= Time.deltaTime;
			if (skillOneDelayTimer <= 0) {
				skillOneDelayActive = false;
			}
		}
	}


	private void SkillTwo() {
		if (castSkill && characterSheetScript.skillActivated == 2 && !skillTwoDelayActive) {

			skillTwoDelayTimer = characterSheetScript.delaySkillTwo;
			skillTwoDelayActive = true;

			// Skill SPAWN COMPANION
			GameObject newSkillTwo = Instantiate(skillTwoGO, attackSpawner.position, attackSpawner.rotation);
			newSkillTwo.transform.GetChild(0).gameObject.tag = "Character" + charID;
			newSkillTwo.transform.GetChild(1).gameObject.tag = "Attack";
			newSkillTwo.tag = "Attack";
			newSkillTwo.transform.GetChild(1).GetComponent<CompanionAggro>().followCaster = gameObject;
			newSkillTwo.transform.GetChild(1).GetComponent<CompanionAggro>().casterDamage = characterSheetScript.skillTwoDmg;
			newSkillTwo.transform.GetChild(1).GetComponent<CompanionAggro>().casterCritChance = characterSheetScript.critChance;
			newSkillTwo.transform.GetChild(1).GetComponent<CompanionAggro>().casterCritDMG = characterSheetScript.critDMG;
		}

		if (skillTwoDelayActive) {
			skillTwoDelayTimer -= Time.deltaTime;
			if (skillTwoDelayTimer <= 0) {
				skillTwoDelayActive = false;
			}
		}
	}

}
