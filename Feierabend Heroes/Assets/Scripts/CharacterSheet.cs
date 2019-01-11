using System.Collections;
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

	// Special Skills
	public bool skillOne = false;
	public bool skillTwo = false;

}
