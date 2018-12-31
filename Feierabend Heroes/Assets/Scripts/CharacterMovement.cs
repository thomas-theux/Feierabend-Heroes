﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CharacterMovement : MonoBehaviour {

	public int playerID = 0;

	private CharacterController cc;

	public float moveSpeed = 10.0f;
	private float moveHorizontal;
	private float moveVertical;


	private void Start() {
		cc = this.gameObject.GetComponent<CharacterController>();
	}


	private void Update() {
		if (LevelTimers.startLevel) {
			GetInput();
		}
	}


	private void FixedUpdate() {
		if (LevelTimers.startLevel) {
			PlayerMovement();
		}
	}


	private void GetInput() {
		moveHorizontal = ReInput.players.GetPlayer(playerID).GetAxis("LS Horizontal");
		moveVertical = ReInput.players.GetPlayer(playerID).GetAxis("LS Vertical");
	}


	private void PlayerMovement() {
		// Movement of the character
		Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
		//movement = movement.normalized;
		cc.Move(movement * moveSpeed * Time.deltaTime);

		// Rotate character depending on the direction they are going
		if (movement != Vector3.zero) {
			transform.forward = movement;
		}
	}

}
