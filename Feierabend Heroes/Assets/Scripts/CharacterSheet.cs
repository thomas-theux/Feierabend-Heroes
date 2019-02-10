using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSheet : MonoBehaviour {

	public int currentOrbs = 0;
	public int charID = 0;
	public int charClass = 0;
	public string classType = "";
	public string classText = "";
	public string classPerk = "";

	// Movement
	public float moveSpeed = 10.0f;

	// Attack
	public string[] attackNames = {"", ""};
	public float attackOneDmg;
	public float attackTwoDmg;
	public float delayAttackOne;
	public float delayAttackTwo;
	public int skillActivated = 0;
	public float delaySkillOne = 0;
	public float delaySkillTwo = 0;
	public float skillOneDmg = 0;
	public float skillTwoDmg = 0;
	public float[] skillOneStats = {0, 0, 0, 0};
	public float[] skillTwoStats = {0, 0, 0, 0};

	// Health
	public float currentHealth;
	public float maxHealth;
	public int respawnChance = 0;
	public bool respawnOrb = false;

	// Defense
	public float charDefense;
	public int critChance = 0;
	public int critDMG = 2;
	public int dodgeChance = 0;
	public bool dodgeHeal = false;

	// Special Skills
	public bool skillOne = false;
	public bool skillTwo = false;
	public bool rageSkillActivated = false;
	public int rageLevel = 0;
	public bool rageModeOn = false;

	// Finding Skills
	public bool canFindApples = false;
	public float healPercentage = 0.2f;
	public bool selfHealActive = false;
	public int doubleOrbChance = 0;
	public bool findThreeOrbs = false;

	// Skillboard skill texts
	public string skillOneTitle = "";
	public string skillTwoTitle = "";
	public string skillOneText = "";
	public string skillTwoText = "";
	public string skillOnePerk = "";
	public string skillTwoPerk = "";
	public string skillOneStat = "";
	public string skillTwoStat = "";
	public string skillOneUpgradeText = "";
	public string skillTwoUpgradeText = "";
	public string skillOneUpgradePerk = "";
	public string skillTwoUpgradePerk = "";


	private void Awake() {
		DontDestroyOnLoad(this.gameObject);
	}

}
