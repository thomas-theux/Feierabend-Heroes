﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSheet : MonoBehaviour {

	public int currentOrbs = 0;

	// Movement
	public float moveSpeed = 10.0f;

	// Attack
	public float attackOneDmg;
	public float attackTwoDmg;
	public float delayAttackOne;
	public float delayAttackTwo;
	public int skillActivated = 0;
	public float delaySkillOne = 0;
	public float delaySkillTwo = 0;

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

}
