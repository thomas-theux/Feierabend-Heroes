﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSheet : MonoBehaviour {

	public int currentOrbs = 0;
	public int charID = -1;
	public int charClass = -1;

	// Movement
	public float moveSpeed = 0;

	// Attack
	public float attackOneDmg;
	public float attackTwoDmg;
	public float delayAttackOne;
	public float delayAttackTwo;
	public float delaySkill = 0;
	public float skillDmg = 0;
	public float[] charSkillStats = {0, 0, 0, 0};
	public bool skillActivated = false;

	// Health
	public float currentHealth;
	public float maxHealth;
	public int respawnChance = 0;

	// Defense
	public float charDefense;
	public int critChance = 0;
	public int critDMG = 2;
	public int dodgeChance = 0;

	// Passive Skills
	public bool canFindApples = false;
	public float healPercentage = 0.2f;
	public int doubleOrbChance = 0;

	public bool selfRepairActive = false;
	public bool slowingTendrilsActive = false;

	// public bool passiveActivated = false;
	
	// public string classType = "";
	// public string classText = "";
	// public string classPerk = "";

	// public bool respawnOrb = false;
	
	// public string[] attackNames = {"", ""};

	// public float[] skillOneStats = {0, 0, 0, 0};
	// public int skillActivated = 0;
	// public float delaySkillOne = 0;
	// public float skillOneDmg = 0;

	// Special Skills
	// public bool skillOne = false;
	// public bool skillTwo = false;

	// Skillboard skill texts
	// public string skillOneTitle = "";
	// public string skillTwoTitle = "";
	// public string skillOneText = "";
	// public string skillTwoText = "";
	// public string skillOnePerk = "";
	// public string skillTwoPerk = "";
	// public string skillOneStat = "";
	// public string skillTwoStat = "";
	// public string skillOneUpgradeText = "";
	// public string skillTwoUpgradeText = "";
	// public string skillOneUpgradePerk = "";
	// public string skillTwoUpgradePerk = "";


	private void Awake() {
		DontDestroyOnLoad(this.gameObject);
	}


	private void Update() {
		// Check if this player has enough orbs to win the match
		if (currentOrbs >= SettingsHolder.orbsToWin && !SettingsHolder.matchOver) {
			SettingsHolder.matchOver = true;
		}
	}

}