using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CharacterMovement : MonoBehaviour {

	public int playerID;
	private CharacterController cc;
	private CharacterSheet characterSheetScript;

	public bool isAttacking = false;
	public bool skillBoardOn;

	// REWIRED
	private float moveHorizontal;
	private float moveVertical;
	private bool showSkillUI;
	private bool closeSkillUI;


	private void Awake() {
		// Get stats from skill script
		characterSheetScript = GetComponent<CharacterSheet>();

		cc = this.gameObject.GetComponent<CharacterController>();
	}


	private void Update() {
		if (LevelTimers.startLevel) {
			GetInput();

			ToggleSkillboard();
		}
	}


	private void FixedUpdate() {
		if (LevelTimers.startLevel) {
			if (!skillBoardOn) {
				PlayerMovement();
			}
		}
	}


	private void GetInput() {
		moveHorizontal = ReInput.players.GetPlayer(playerID).GetAxis("LS Horizontal");
		moveVertical = ReInput.players.GetPlayer(playerID).GetAxis("LS Vertical");

		showSkillUI = ReInput.players.GetPlayer(playerID).GetButtonDown("Triangle");
		closeSkillUI = ReInput.players.GetPlayer(playerID).GetButtonDown("Circle");
	}


	private void PlayerMovement() {
		// Movement of the character
		Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
		// movement = movement.normalized;
		if (!isAttacking) {
			cc.Move(movement * characterSheetScript.moveSpeed * Time.deltaTime);
		}

		// Rotate character depending on the direction they are going
		if (movement != Vector3.zero) {
			transform.forward = movement;
		}
	}


	private void ToggleSkillboard() {
		if (showSkillUI) {
			skillBoardOn = !skillBoardOn;
			ShowSkillboard();
		}
		if (skillBoardOn && closeSkillUI) {
			skillBoardOn = false;
			ShowSkillboard();
		}
	}


	private void ShowSkillboard() {
		this.gameObject.transform.GetChild(2).gameObject.SetActive(skillBoardOn);
	}

}
