using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Cloudmaster : MonoBehaviour {

	private CharacterSkills skillsScript;

	// These variables can be improved by advancing on the skill tree
	public float cmHealth = 100.0f;
	public float cmDefense = 10.0f;


	private void Awake() {
		// Set stats in skill script
		GetComponent<CharacterSkills>().health = cmHealth;
		GetComponent<CharacterSkills>().defense = cmDefense;

		// Get stats from skill script
		skillsScript = GetComponent<CharacterSkills>();
	}

}
