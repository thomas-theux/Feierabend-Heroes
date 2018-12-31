using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Attack_Cloudmaster : MonoBehaviour {

	public GameObject bombsGO;
	public Transform attackSpawner;

	private float fireDelay = 0.2f;
	private float delayTimer;
	private bool delayActive;

	// REWIRED
	private Player player;
	private bool fireBomb;


	private void Awake() {
		int charID = GetComponent<CharacterMovement>().playerID;
		player = ReInput.players.GetPlayer(charID);
	}


	private void Update() {
		GetInput();

		PerformAttack();
	}


	private void GetInput() {
		fireBomb = player.GetButton("X");
	}


	private void PerformAttack() {
		if (fireBomb && !delayActive) {
			Instantiate(bombsGO, attackSpawner.position, attackSpawner.rotation);
			delayActive = true;
		}

		if (delayActive) {
			delayTimer -= Time.deltaTime;
			if (delayTimer <= 0) {
				delayActive = false;
				delayTimer = fireDelay;
			}
		}
	}

}
