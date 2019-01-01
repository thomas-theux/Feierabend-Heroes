using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CharacterMovement : MonoBehaviour {

	public int playerID;
	private CharacterController cc;
	private CharacterSkills skillsScript;

	public bool isAttacking = false;

	// These variables can be improved by advancing on the skill tree
	public float moveSpeed;

	// REWIRED
	private float moveHorizontal;
	private float moveVertical;


	private void Awake() {
		// Get stats from skill script
		moveSpeed = GetComponent<CharacterSkills>().moveSpeed;

		// Get stats from skill script
		skillsScript = GetComponent<CharacterSkills>();

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
		// movement = movement.normalized;
		if (!isAttacking) {
			cc.Move(movement * skillsScript.moveSpeed * Time.deltaTime);
		}

		// Rotate character depending on the direction they are going
		if (movement != Vector3.zero) {
			transform.forward = movement;
		}
	}

}
