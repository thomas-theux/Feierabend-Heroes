using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Class_Cloudmaster : MonoBehaviour {

	public GameObject attackOneGO;
	public GameObject attackTwoGO;
	public GameObject skillOneGO;
	public GameObject skillTwoGO;
	private CharacterSheet characterSheetScript;
	private CharacterMovement charactMovementScript;

	private Transform attackSpawner;

	private int charID;

	// These variables can be improved by advancing on the skill tree
	private float attackOne = 0.4f;
	private float attackTwo = 1.0f;
	private float skillOne = 15.0f;
	private float skillTwo = 30.0f;

	private float rageAttackSpeed = 1.0f;

	private float attackOneDelay;
	private float attackTwoDelay;

	private float attackOneDmg = 16.0f;
	private float attackTwoDmg = 42.0f;
	private float skillOneDmg = 0.0f;
	private float skillTwoDmg = 3.0f;

	// Initial variables and increase values for the skills
	private float[] skillOneStats = {0.4f, 5.0f, 0.2f, 1.0f};
	private float[] skillTwoStats = {0.3f, 24.0f, -0.05f, 4.0f};

	private float charHealth = 300.0f;
	
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

	// Skill One – Healing Beacon
	private float skillOneDelayTimer;
	private bool skillOneDelayActive = false;

	// Skill Two – Turret Gun
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

		for (int i = 0; i < 4; i++) {
			characterSheetScript.skillOneStats[i] = skillOneStats[i];
			characterSheetScript.skillTwoStats[i] = skillTwoStats[i];
		}

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
			newAttackOne.transform.parent = gameObject.transform;
			newAttackOne.GetComponent<WrenchPunch>().casterDamage = characterSheetScript.attackOneDmg;
			newAttackOne.GetComponent<WrenchPunch>().casterCritChance = characterSheetScript.critChance;
			newAttackOne.GetComponent<WrenchPunch>().casterCritDMG = characterSheetScript.critDMG;
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
			
			attackSpawner.transform.rotation = transform.rotation * Quaternion.Euler(-35, 0, 0);
			GameObject newAttackTwo = Instantiate(attackTwoGO, attackSpawner.position, attackSpawner.rotation);
			newAttackTwo.GetComponent<Bomb>().casterTag = "Character" + charID;
			newAttackTwo.tag = "Attack";
			newAttackTwo.transform.GetComponent<Bomb>().casterDamage = characterSheetScript.attackTwoDmg;
			newAttackTwo.GetComponent<Bomb>().casterCritChance = characterSheetScript.critChance;
			newAttackTwo.GetComponent<Bomb>().casterCritDMG = characterSheetScript.critDMG;
			attackSpawner.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 0);
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

			// Skill HEALING BEACON
			attackSpawner.transform.localPosition = attackSpawner.transform.localPosition + new Vector3(0, 0, 2);

			GameObject newSkillOne = Instantiate(skillOneGO, attackSpawner.position, attackSpawner.rotation);
			newSkillOne.transform.GetChild(0).gameObject.tag = "Character" + charID;
			newSkillOne.transform.GetChild(1).gameObject.tag = "Attack";
			newSkillOne.tag = "Attack";

			newSkillOne.transform.GetChild(1).GetComponent<HealingBeacon>().healAmount = characterSheetScript.skillOneStats[0];
			newSkillOne.transform.GetChild(1).GetComponent<HealingBeacon>().lifeTime = characterSheetScript.skillOneStats[1];
			
			attackSpawner.transform.localPosition = attackSpawner.transform.localPosition - new Vector3(0, 0, 2);
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

			// Skill TURRET GUN
			attackSpawner.transform.localPosition = attackSpawner.transform.localPosition + new Vector3(0, 0, 2);

			GameObject newSkillTwo = Instantiate(skillTwoGO, attackSpawner.position, attackSpawner.rotation);
			newSkillTwo.transform.GetChild(0).gameObject.tag = "Character" + charID;
			for (int i = 1; i < 5; i++) {
				newSkillTwo.transform.GetChild(i).gameObject.tag = "Attack";
			}
			newSkillTwo.tag = "Attack";

			// Set shoot speed and radius of turret gun from skillboard
			newSkillTwo.transform.GetChild(4).GetComponent<TurretGun>().shotDelayDefault = characterSheetScript.skillTwoStats[0];
			newSkillTwo.transform.GetChild(4).transform.localScale = new Vector3(characterSheetScript.skillTwoStats[1], characterSheetScript.skillTwoStats[1] * 2, characterSheetScript.skillTwoStats[1]);

			newSkillTwo.transform.GetChild(4).GetComponent<TurretGun>().casterDamage = characterSheetScript.skillTwoDmg;
			newSkillTwo.transform.GetChild(4).GetComponent<TurretGun>().casterCritChance = characterSheetScript.critChance;
			newSkillTwo.transform.GetChild(4).GetComponent<TurretGun>().casterCritDMG = characterSheetScript.critDMG;
			newSkillTwo.transform.GetChild(4).GetComponent<TurretGun>().charID = charID;

			attackSpawner.transform.localPosition = attackSpawner.transform.localPosition - new Vector3(0, 0, 2);
		}

		if (skillTwoDelayActive) {
			skillTwoDelayTimer -= Time.deltaTime;
			if (skillTwoDelayTimer <= 0) {
				skillTwoDelayActive = false;
			}
		}
	}

}
