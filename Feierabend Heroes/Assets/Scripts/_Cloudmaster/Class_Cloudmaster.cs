using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Class_Cloudmaster : MonoBehaviour {

	private int charClass = 0;

	private GameObject attackOneGO;
	private GameObject attackTwoGO;
	private GameObject skillGO;
	private CharacterSheet characterSheetScript;
	private CharacterMovement charactMovementScript;
	private UIHandler uiHandlerScript;
	private LifeDeathHandler lifeDeathHandlerScript;

	private GameObject wrenchPunchSound;
	private GameObject bombThrowSound;
	
	private AudioSource skillCoolDownSound;
	private AudioSource skillNotAvailableSound;

	private Transform attackSpawner;

	private int charID;

	// private string[] attackNames = {"Wrench Punch (DPS)", "Bomb Throw (DPS)"};

	// These variables can be improved by advancing on the skill tree
	private float delayAttackOne = 0.4f;
	private float delayAttackTwo = 1.0f;
	// private float skillOne = 15.0f;
	private float delaySkill = 30.0f;

	private bool performAttacks = false;
	private float performTimeDef = 0.2f;
	private float performTime = 0;

	private float attackOneDelay;
	private float attackTwoDelay;

	private float attackOneDmg = 16.0f;
	private float attackTwoDmg = 36.0f;
	// private float skillOneDmg = 0.0f;
	private float skillDmg = 3.0f;

	// Initial variables and increase values for the skills
	// private float[] skillOneStats = {0.4f, 5.0f, 0.2f, 1.0f};

	// {shot delay, aggro radius, decrease shot delay, increase radius}
	private float[] charSkillStats = {0.5f, 14.0f, -0.02f, 3.0f};

	// Skill titles and texts for the skill board
	// private string classType = "CLOUD MASTER";
	// private string classText = "An Engineer that uses Cloud technology to defeat enemies.";
	// private string classPerk = "• Wrench Punch\n• Bomb Throw";
	
	// private string skillOneTitle = "HEALING BEACON";
	// private string skillTitle = "TURRET GUN";
	// private string skillOneText = "Place a Healing Beacon that gives back health over time.";
	// private string skillText = "Spawn a immobile Turret Gun that shoots enemies on sight.";
	// private string skillOnePerk = "• 0.4 HP\n• lasts 5 seconds";
	// private string skillPerk = "• 0.3 Damage\n• lasts 24 seconds";
	// private string skillOneStat = "HP";
	// private string skillStat = "Damage";
	// private string skillOneUpgradeText = "Improves the Beacon's healing speed and lifetime.";
	// private string skillUpgradeText = "Improves the Gun's attack speed and increases the radius.";
	// private string skillOneUpgradePerk = "+50% HP heal\n+1s lifetime";
	// private string skillUpgradePerk = "+16% shot speed\n+4m radius";

	// private float charHealth = 260.0f;
	// private float charDefense = 8.0f;
	// private float moveSpeed = 10.0f;

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
	// private float skillOneDelayTimer;
	// private bool skillOneDelayActive = false;

	// Skill Two – Turret Gun
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
		attackOneGO = Resources.Load<GameObject>("Attacks/WrenchPunch");
		attackTwoGO = Resources.Load<GameObject>("Attacks/Bomb");
		// skillOneGO = Resources.Load<GameObject>("Attacks/HealingBeacon");
		skillGO = Resources.Load<GameObject>("Attacks/TurretGun");

		wrenchPunchSound = Resources.Load<GameObject>("Sounds/WrenchPunchSound");
		bombThrowSound = Resources.Load<GameObject>("Sounds/BombThrowSound");

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

		characterSheetScript.currentHealth = CharClassContent.charHPTexts[characterSheetScript.charClass];
		characterSheetScript.maxHealth = CharClassContent.charHPTexts[characterSheetScript.charClass];
		characterSheetScript.moveSpeed = CharClassContent.charMSPDTexts[characterSheetScript.charClass];
		characterSheetScript.charDefense = CharClassContent.charDEFTexts[characterSheetScript.charClass];
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

			if (performAttacks) {
				PerformAttackDelay();
			}
			
			DelayMovement();
		}
	}


	private void GetInput() {
		performAttackOne = player.GetButton("X");
		performAttackTwo = player.GetButton("Square");

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
			int wrenchPunch = PlayerPrefs.GetInt("Wrench Punch");
			wrenchPunch++;
			PlayerPrefs.SetInt("Wrench Punch", wrenchPunch);
			
			performAttacks = true;
			performTime = performTimeDef;

			attackOneDelayTimer = characterSheetScript.delayAttackOne;
			attackOneDelayActive = true;

			Instantiate(wrenchPunchSound);

			attackSpawner.transform.localPosition = attackSpawner.transform.localPosition - new Vector3(0, 0, 1);
			GameObject newAttackOne = Instantiate(attackOneGO, attackSpawner.position, attackSpawner.rotation);
			newAttackOne.transform.GetChild(0).gameObject.tag = "Character" + charID;
			newAttackOne.GetComponent<WrenchPunch>().damagerID = charID;
			newAttackOne.tag = "Attack";
			newAttackOne.transform.parent = gameObject.transform;
			newAttackOne.GetComponent<WrenchPunch>().casterDamage = characterSheetScript.attackOneDmg;
			newAttackOne.GetComponent<WrenchPunch>().casterCritChance = characterSheetScript.critChance;
			newAttackOne.GetComponent<WrenchPunch>().casterCritDMG = characterSheetScript.critDMG;
			attackSpawner.transform.localPosition = attackSpawner.transform.localPosition + new Vector3(0, 0, 1);
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
			int bombThrow = PlayerPrefs.GetInt("Bomb Throw");
			bombThrow++;
			PlayerPrefs.SetInt("Bomb Throw", bombThrow);

			performAttacks = true;
			performTime = performTimeDef;

			attackTwoDelayTimer = characterSheetScript.delayAttackTwo;
			attackTwoDelayActive = true;

			Instantiate(bombThrowSound);
			
			attackSpawner.transform.rotation = transform.rotation * Quaternion.Euler(-35, 0, 0);
			GameObject newAttackTwo = Instantiate(attackTwoGO, attackSpawner.position, attackSpawner.rotation);
			newAttackTwo.GetComponent<Bomb>().casterTag = "Character" + charID;
			newAttackTwo.GetComponent<Bomb>().damagerID = charID;
			newAttackTwo.transform.GetChild(0).gameObject.tag = "Character" + charID;
			newAttackTwo.tag = "Attack";
			newAttackTwo.transform.GetComponent<Bomb>().casterDamage = characterSheetScript.attackTwoDmg;
			newAttackTwo.GetComponent<Bomb>().casterCritChance = characterSheetScript.critChance;
			newAttackTwo.GetComponent<Bomb>().casterCritDMG = characterSheetScript.critDMG;
			attackSpawner.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 0);
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
			int turretGun = PlayerPrefs.GetInt("Turret Gun");
			turretGun++;
			PlayerPrefs.SetInt("Turret Gun", turretGun);

			performAttacks = true;
			performTime = performTimeDef;

			skillDelayTimer = characterSheetScript.delaySkill;
			skillDelayActive = true;

			// Skill TURRET GUN
			attackSpawner.transform.localPosition = attackSpawner.transform.localPosition + new Vector3(0, 0, 2);

			GameObject newSkill = Instantiate(skillGO, attackSpawner.position, attackSpawner.rotation);
			newSkill.transform.GetChild(0).gameObject.tag = "Character" + charID;
			newSkill.transform.GetChild(4).GetComponent<TurretGun>().damagerID = charID;
			for (int i = 1; i < 5; i++) {
				newSkill.transform.GetChild(i).gameObject.tag = "Attack";
			}
			newSkill.tag = "Attack";

			// Set shoot speed and radius of turret gun from skillboard
			newSkill.transform.GetChild(4).GetComponent<TurretGun>().shotDelayDefault = characterSheetScript.charSkillStats[0];
			newSkill.transform.GetChild(4).transform.localScale = new Vector3(characterSheetScript.charSkillStats[1], characterSheetScript.charSkillStats[1] * 2, characterSheetScript.charSkillStats[1]);

			newSkill.transform.GetChild(4).GetComponent<TurretGun>().casterDamage = characterSheetScript.skillDmg;
			newSkill.transform.GetChild(4).GetComponent<TurretGun>().casterCritChance = characterSheetScript.critChance;
			newSkill.transform.GetChild(4).GetComponent<TurretGun>().casterCritDMG = characterSheetScript.critDMG;
			newSkill.transform.GetChild(4).GetComponent<TurretGun>().charID = charID;

			attackSpawner.transform.localPosition = attackSpawner.transform.localPosition - new Vector3(0, 0, 2);
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

}
