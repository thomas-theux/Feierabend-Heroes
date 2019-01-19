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

	private string[] attackNames = {"Meteoo Shot", "Fire Block"};

	// These variables can be improved by advancing on the skill tree
	private float delayAttackOne = 0.5f;
	private float delayAttackTwo = 10.0f;
	private float skillOne = 18.0f;
	private float skillTwo = 32.0f;

	private float rageAttackSpeed = 1.0f;

	private float attackOneDelay;
	private float attackTwoDelay;

	private float attackOneDmg = 20.0f;
	private float attackTwoDmg = 0.08f;
	private float skillOneDmg = 0.0f;
	private float skillTwoDmg = 6.0f;

	// Initial variables and increase values for the skills
	private float[] skillOneStats = {2.0f, 8.0f, 0.5f, 2.0f};
	private float[] skillTwoStats = {20.0f, 12.0f, 3.0f, 4.0f};

	// Skill titles and texts for the skill board
	private string skillOneTitle = "LIKE A TANK";
	private string skillTwoTitle = "COMPANION";
	private string skillOneText = "Increase your characters health for a limited time.";
	private string skillTwoText = "Spawn a companion that helps your character fight in battle.";
	private string skillOnePerk = "• 2.0x HP\n• lasts 8 seconds";
	private string skillTwoPerk = "• 20m aggro radius\n• lasts 12 seconds";
	private string skillOneStat = "HP";
	private string skillTwoStat = "Damage";
	private string skillOneUpgradeText = "Increases the HP mutliplier and the overall duration.";
	private string skillTwoUpgradeText = "Increases the aggro radius and the companion's lifetime.";
	private string skillOneUpgradePerk = "+0.5x HP heal\n+2s duration";
	private string skillTwoUpgradePerk = "+3m radius\n+4s lifetime";

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
	private bool HPSkillActive = false;
	private float skillOneTimer;
	private float savedMultiplier;

	// Skill Two – Spawn Companion
	private float skillTwoDelayTimer;
	private bool skillTwoDelayActive = false;

	// REWIRED
	private Player player;
	private bool performAttackOne;
	private bool performAttackTwo;
	private bool castSkill;


	private void Awake() {
		// Get stats from skill script
		characterSheetScript = GetComponent<CharacterSheet>();
		charactMovementScript = GetComponent<CharacterMovement>();

		// Get gameobjects for classes
		attackSpawner = transform.GetChild(1).gameObject.transform;

		charID = charactMovementScript.playerID;
		player = ReInput.players.GetPlayer(charID);

		// Set names of attacks in character sheet
		characterSheetScript.attackNames[0] = attackNames[0];
		characterSheetScript.attackNames[1] = attackNames[1];

		// Set stats in skill script
		characterSheetScript.delayAttackOne = delayAttackOne;
		characterSheetScript.delayAttackTwo = delayAttackTwo;
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

		characterSheetScript.skillOneTitle = skillOneTitle;
		characterSheetScript.skillTwoTitle = skillTwoTitle;
		characterSheetScript.skillOneText = skillOneText;
		characterSheetScript.skillTwoText = skillTwoText;
		characterSheetScript.skillOnePerk = skillOnePerk;
		characterSheetScript.skillTwoPerk = skillTwoPerk;
		characterSheetScript.skillOneStat = skillOneStat;
		characterSheetScript.skillTwoStat = skillTwoStat;
		characterSheetScript.skillOneUpgradeText = skillOneUpgradeText;
		characterSheetScript.skillTwoUpgradeText = skillTwoUpgradeText;
		characterSheetScript.skillOneUpgradePerk = skillOneUpgradePerk;
		characterSheetScript.skillTwoUpgradePerk = skillTwoUpgradePerk;

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
			savedMultiplier = characterSheetScript.skillOneStats[0];
			characterSheetScript.currentHealth *= savedMultiplier;
			characterSheetScript.maxHealth *= savedMultiplier;
			skillOneTimer = characterSheetScript.skillOneStats[1];
			HPSkillActive = true;
		}

		if (skillOneDelayActive) {
			skillOneDelayTimer -= Time.deltaTime;
			if (skillOneDelayTimer <= 0) {
				skillOneDelayActive = false;
			}
		}

		if (HPSkillActive) {
			skillOneTimer -= Time.deltaTime;
			if (skillOneTimer <= 0) {
				characterSheetScript.currentHealth /= savedMultiplier;
				characterSheetScript.maxHealth /= savedMultiplier;
				HPSkillActive = false;
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
			
			// Set size and lifetime of aggro radius from skillboard
			newSkillTwo.transform.GetChild(1).transform.localScale = new Vector3(characterSheetScript.skillTwoStats[0], characterSheetScript.skillTwoStats[0], characterSheetScript.skillTwoStats[0]);
			newSkillTwo.transform.GetChild(1).GetComponent<CompanionAggro>().lifeTime = characterSheetScript.skillTwoStats[1];

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
