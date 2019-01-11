﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSheet : MonoBehaviour {

	// Movement
	public float moveSpeed = 10.0f;

	// Attack
	public float attackOneDmg;
	public float attackTwoDmg;

	public float delayAttackOne;
	public float delayAttackTwo;

	// Health
	public float currentHealth;
	public float maxHealth;

	// Defense
	public float charDefense;
	public int critChance = 0;
	public int critDMG = 2;
	public int dodgeChance = 0;
	public bool dodgeHeal = false;
	public int respawnChance = 0;

	// Special Skills
	public bool skillOne = false;
	public bool skillTwo = false;
	public bool rageSkillActivated = false;
	public int rageLevel = 0;
	public bool rageModeOn = false;

}