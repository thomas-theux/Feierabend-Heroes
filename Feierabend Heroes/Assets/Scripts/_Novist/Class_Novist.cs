using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Class_Novist : MonoBehaviour {

	private int charClass = 1;

	public GameObject attackOneGO;
	public GameObject attackTwoGO;
	public GameObject skillGO;
	public GameObject passiveSkillGO;

	private CharacterSheet characterSheetScript;
	private CharacterMovement charactMovementScript;
	private UIHandler uiHandlerScript;
	private LifeDeathHandler lifeDeathHandlerScript;

	public GameObject meteorShotSound;
	public GameObject fireBlockCastSound;
	
	public AudioSource skillCoolDownSound;
	public AudioSource skillNotAvailableSound;

	private Transform attackSpawner;

	private int charID;

	// private string[] attackNames = {"Meteor Shot (DPS)", "Fire Block (DPS)"};

	// These variables can be improved by advancing on the skill tree
	private float delayAttackOne = 0.5f;
	private float delayAttackTwo = 10.0f;
	// private float skillOne = 18.0f;
	private float delaySkill = 32.0f;

	private bool performAttacks = false;
	private float performTimeDef = 0.2f;
	private float performTime = 0;

	private bool passiveSkillActivated = false;

	private float attackOneDelay;
	private float attackTwoDelay;

	private float attackOneDmg = 20.0f;
	private float attackTwoDmg = 0.08f;
	// private float skillOneDmg = 0.0f;
	private float skillDmg = 6.0f;

	// Initial variables and increase values for the skills
	// private float[] skillOneStats = {2.0f, 8.0f, 0.5f, 2.0f};

	// {aggro radius, lifetime, increase aggro radius, increase lifetime}
	private float[] charSkillStats = {26.0f, 12.0f, 3.0f, 4.0f};

	// Skill titles and texts for the skill board
	// private string classType = "NOVIST";
	// private string classText = "A Mage that has mastered the powers of the nova star.";
	// private string classPerk = "• Meteor Shot\n• Fire Block";

	
	// private string skillOneTitle = "LIKE A TANK";
	// private string skillTitle = "COMPANION";
	// private string skillOneText = "Increase your characters health for a limited time.";
	// private string skillText = "Spawn a companion that helps your character fight in battle.";
	// private string skillOnePerk = "• 2.0x HP\n• lasts 8 seconds";
	// private string skillPerk = "• 20m aggro radius\n• lasts 12 seconds";
	// private string skillOneStat = "HP";
	// private string skillStat = "Damage";
	// private string skillOneUpgradeText = "Increases the HP mutliplier and the overall duration.";
	// private string skillUpgradeText = "Increases the aggro radius and the companion's lifetime.";
	// private string skillOneUpgradePerk = "+0.5x HP heal\n+2s duration";
	// private string skillUpgradePerk = "+3m radius\n+4s lifetime";

	// private float charHealth = 380.0f;
	// private float charDefense = 18.0f;
	// private float moveSpeed = 8.0f;

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
	// private float skillOneDelayTimer;
	// private bool skillOneDelayActive = false;
	// private bool HPSkillActive = false;
	// private float skillOneTimer;
	// private float savedMultiplier;

	// Skill Two – Spawn Companion
	public float skillDelayTimer;
	private bool skillDelayActive = false;

	// REWIRED
	private Player player;
	private bool performAttackOne;
	private bool performAttackTwo;
	private bool castSkill;


	private void Awake() {
		// Get stats from skill script
		characterSheetScript = GetComponent<CharacterSheet>();
		charactMovementScript = GetComponent<CharacterMovement>();
		lifeDeathHandlerScript = GetComponent<LifeDeathHandler>();

		// Set character class in character sheet
		characterSheetScript.charClass = charClass;

		// Set names of attacks in character sheet
		// characterSheetScript.attackNames[0] = attackNames[0];
		// characterSheetScript.attackNames[1] = attackNames[1];

		// Attach attack GameObjects to script
		attackOneGO = Resources.Load<GameObject>("Attacks/MeteorShot");
		attackTwoGO = Resources.Load<GameObject>("Attacks/FireBlock");
		skillGO = Resources.Load<GameObject>("Attacks/Companion");
		passiveSkillGO = Resources.Load<GameObject>("Attacks/Tendrils");

		meteorShotSound = Resources.Load<GameObject>("Sounds/MeteorShotSound");
		fireBlockCastSound = Resources.Load<GameObject>("Sounds/FireBlockCastSound");

		skillCoolDownSound = Resources.Load<GameObject>("Sounds/SkillCooldownReadySound").GetComponent<AudioSource>();
		skillNotAvailableSound = Resources.Load<GameObject>("Sounds/SkillNotAvailableSound").GetComponent<AudioSource>();
	}


	private void Start() {
		// Get stats from skill script
		uiHandlerScript = transform.GetChild(transform.childCount-1).GetComponent<UIHandler>();

		// Get gameobjects for classes
		attackSpawner = transform.GetChild(1).gameObject.transform;

		charID = charactMovementScript.playerID;
		player = ReInput.players.GetPlayer(charID);

		// characterSheetScript.classType = classType;
		// characterSheetScript.classText = classText;
		// characterSheetScript.classPerk = classPerk;

		// Set stats in skill script
		characterSheetScript.delayAttackOne = delayAttackOne;
		characterSheetScript.delayAttackTwo = delayAttackTwo;
		// characterSheetScript.delaySkillOne = skillOne;
		characterSheetScript.delaySkill = delaySkill;

		characterSheetScript.attackOneDmg = attackOneDmg;
		characterSheetScript.attackTwoDmg = attackTwoDmg;
		// characterSheetScript.skillOneDmg = skillOneDmg;
		characterSheetScript.skillDmg = skillDmg;

		for (int i = 0; i < 4; i++) {
			// characterSheetScript.skillOneStats[i] = skillOneStats[i];
			characterSheetScript.charSkillStats[i] = charSkillStats[i];
		}

		// characterSheetScript.skillOneTitle = skillOneTitle;
		// characterSheetScript.skillTitle = skillTitle;
		// characterSheetScript.skillOneText = skillOneText;
		// characterSheetScript.skillText = skillText;
		// characterSheetScript.skillOnePerk = skillOnePerk;
		// characterSheetScript.skillPerk = skillPerk;
		// characterSheetScript.skillOneStat = skillOneStat;
		// characterSheetScript.skillStat = skillStat;
		// characterSheetScript.skillOneUpgradeText = skillOneUpgradeText;
		// characterSheetScript.skillUpgradeText = skillUpgradeText;
		// characterSheetScript.skillOneUpgradePerk = skillOneUpgradePerk;
		// characterSheetScript.skillUpgradePerk = skillUpgradePerk;

		// characterSheetScript.currentHealth = charHealth;
		// characterSheetScript.maxHealth = charHealth;
		// characterSheetScript.moveSpeed = moveSpeed;
		// characterSheetScript.charDefense = charDefense;

		characterSheetScript.currentHealth = CharClassContent.charHPStats[characterSheetScript.charClass];
		characterSheetScript.maxHealth = CharClassContent.charHPStats[characterSheetScript.charClass];
		characterSheetScript.moveSpeed = CharClassContent.charMSPDStats[characterSheetScript.charClass];
		characterSheetScript.charDefense = CharClassContent.charDEFStats[characterSheetScript.charClass];
	}


	private void Update() {
		if (!lifeDeathHandlerScript.charIsDead) {
			if (!charactMovementScript.skillBoardOn && TimeHandler.startBattle) {
				GetInput();
			} else {
				performAttackOne = false;
				performAttackTwo = false;
				castSkill = false;
			}

			if (!GameManager.pauseMenuOpen) {
				AttackOne();
				AttackTwo();
				CastSkill();
			}

			if (characterSheetScript.slowingTendrilsActive && !passiveSkillActivated) {
				ActivatePassive();
			}

			if (performAttacks) {
				PerformAttackDelay();
			}

			DelayMovement();
		}
	}


	private void GetInput() {
		performAttackOne = player.GetButton("X");
		if (characterSheetScript.secondaryActivated) {
			performAttackTwo = player.GetButton("Square");
		}

		if (!charactMovementScript.skillboardBlocksCasting) {
			castSkill = player.GetButtonDown("Circle");
		}
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


	private void PerformAttackDelay() {
		performTime -= Time.deltaTime;

		if (performTime <= 0) {
			performAttacks = false;
		}
	}


	private void AttackOne() {
		if (performAttackOne && !attackOneDelayActive && !performAttacks) {

			// DEV STUFF – Collect data on how many times an attack has been used
			int meteorShot = PlayerPrefs.GetInt("Meteor Shot");
			meteorShot++;
			PlayerPrefs.SetInt("Meteor Shot", meteorShot);
			
			performAttacks = true;
			performTime = performTimeDef;

			attackOneDelayTimer = characterSheetScript.delayAttackOne;
			attackOneDelayActive = true;

			Instantiate(meteorShotSound);
			
			GameObject newAttackOne = Instantiate(attackOneGO, attackSpawner.position, attackSpawner.rotation);
			newAttackOne.transform.GetChild(0).gameObject.tag = "Character" + charID;
			newAttackOne.GetComponent<MeteorShot>().damagerID = charID;
			newAttackOne.transform.GetChild(newAttackOne.transform.childCount-1).gameObject.tag = "Attack";
			newAttackOne.tag = "Attack";
			newAttackOne.transform.GetComponent<MeteorShot>().casterDamage = characterSheetScript.attackOneDmg;
			newAttackOne.GetComponent<MeteorShot>().casterCritChance = characterSheetScript.critChance;
			newAttackOne.GetComponent<MeteorShot>().casterCritDMG = characterSheetScript.critDMG;
		}

		if (attackOneDelayActive) {
			attackOneDelayTimer -= Time.deltaTime;
			uiHandlerScript.attackOneDelayTimer = attackOneDelayTimer;
			if (attackOneDelayTimer <= 0) {
				attackOneDelayActive = false;
			}
		}
	}


	private void AttackTwo() {
		if (performAttackTwo && !attackTwoDelayActive && !performAttacks) {

			// DEV STUFF – Collect data on how many times an attack has been used
			int fireBlock = PlayerPrefs.GetInt("Fire Block");
			fireBlock++;
			PlayerPrefs.SetInt("Fire Block", fireBlock);
			
			performAttacks = true;
			performTime = performTimeDef;

			attackTwoDelayTimer = characterSheetScript.delayAttackTwo;
			attackTwoDelayActive = true;

			Instantiate(fireBlockCastSound);

			attackSpawner.transform.localPosition = attackSpawner.transform.localPosition + new Vector3(0, 0, 2);
			GameObject newAttackTwo = Instantiate(attackTwoGO, attackSpawner.position, attackSpawner.rotation);
			newAttackTwo.transform.GetChild(0).gameObject.tag = "Character" + charID;
			newAttackTwo.GetComponent<FireBlock>().damagerID = charID;
			newAttackTwo.tag = "Attack";
			newAttackTwo.transform.GetComponent<FireBlock>().casterDamage = characterSheetScript.attackTwoDmg;
			newAttackTwo.GetComponent<FireBlock>().casterCritChance = characterSheetScript.critChance;
			newAttackTwo.GetComponent<FireBlock>().casterCritDMG = characterSheetScript.critDMG;
			attackSpawner.transform.localPosition = attackSpawner.transform.localPosition - new Vector3(0, 0, 2);
		}

		if (attackTwoDelayActive) {
			attackTwoDelayTimer -= Time.deltaTime;
			uiHandlerScript.attackTwoDelayTimer = attackTwoDelayTimer;
			if (attackTwoDelayTimer <= 0) {
				attackTwoDelayActive = false;
			}
		}
	}


	private void CastSkill() {
		if (castSkill && characterSheetScript.skillActivated && !skillDelayActive && !performAttacks) {

			// DEV STUFF – Collect data on how many times an attack has been used
			int spawnCompanion = PlayerPrefs.GetInt("Companion");
			spawnCompanion++;
			PlayerPrefs.SetInt("Companion", spawnCompanion);
			
			performAttacks = true;
			performTime = performTimeDef;

			skillDelayTimer = characterSheetScript.delaySkill;
			skillDelayActive = true;

			// Skill SPAWN COMPANION
			GameObject newSkill = Instantiate(skillGO, attackSpawner.position, attackSpawner.rotation);
			newSkill.transform.GetChild(0).gameObject.tag = "Character" + charID;
			newSkill.transform.GetChild(1).GetComponent<CompanionAggro>().damagerID = charID;
			newSkill.transform.GetChild(1).gameObject.tag = "Attack";
			newSkill.tag = "Attack";
			
			// Set size and lifetime of aggro radius from skillboard
			newSkill.transform.GetChild(1).transform.localScale = new Vector3(characterSheetScript.charSkillStats[0], characterSheetScript.charSkillStats[0], characterSheetScript.charSkillStats[0]);
			newSkill.transform.GetChild(1).GetComponent<CompanionAggro>().SetLifeTime(characterSheetScript.charSkillStats[1]);

			newSkill.transform.GetChild(1).GetComponent<CompanionAggro>().followCaster = this.gameObject;
			newSkill.transform.GetChild(1).GetComponent<CompanionAggro>().casterDamage = characterSheetScript.skillDmg;
			newSkill.transform.GetChild(1).GetComponent<CompanionAggro>().casterCritChance = characterSheetScript.critChance;
			newSkill.transform.GetChild(1).GetComponent<CompanionAggro>().casterCritDMG = characterSheetScript.critDMG;
		} else if (castSkill && skillDelayActive) {
			Instantiate(skillNotAvailableSound);
		}

		if (skillDelayActive) {
			skillDelayTimer -= Time.deltaTime;
			uiHandlerScript.skillDelayTimer = skillDelayTimer;
			if (skillDelayTimer <= 0) {
				Instantiate(skillCoolDownSound);
				skillDelayActive = false;
			}
		}
	}


	private void ActivatePassive() {
		passiveSkillActivated = true;
		GameObject newTendrils = Instantiate(passiveSkillGO);
		newTendrils.transform.parent = this.gameObject.transform;
		newTendrils.transform.localPosition = new Vector3(0, 0, 0);
	}

}
