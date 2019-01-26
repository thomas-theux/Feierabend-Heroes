using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CharacterMovement : MonoBehaviour {

	public int playerID;
	private CharacterController cc;
	private CharacterSheet characterSheetScript;

	public Animator anim;

	public bool isAttacking = false;
	public bool skillBoardOn;

	private float rageMoveSpeed = 1;
	private Vector3 charVelocity;

	// REWIRED
	private float moveHorizontal;
	private float moveVertical;
	public bool activationBtn;
	private bool showSkillUI;
	private bool closeSkillUI;


	private void Awake() {
		// Get stats from skill script
		characterSheetScript = GetComponent<CharacterSheet>();
		characterSheetScript.charID = playerID;

		cc = this.gameObject.GetComponent<CharacterController>();

		int rndAnim = Random.Range(0, 2);
		anim.SetInteger("standUp", rndAnim);
	}


	private void Update() {
		if (LevelTimers.startLevel) {
			GetInput();

			ToggleSkillboard();

			AddGravity();
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

		activationBtn = ReInput.players.GetPlayer(playerID).GetButtonDown("R1");

		showSkillUI = ReInput.players.GetPlayer(playerID).GetButtonDown("Triangle");
		closeSkillUI = ReInput.players.GetPlayer(playerID).GetButtonDown("Circle");
	}


	private void PlayerMovement() {
		// Movement of the character
		Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
		// movement = movement.normalized;
		movement = Vector3.ClampMagnitude(movement, 1);

		if (!isAttacking) {
			// Check if hero has rage mode on and HP < 10%
			if (characterSheetScript.rageModeOn && characterSheetScript.rageLevel >= 1) {
				rageMoveSpeed = 1.5f;
			} else if (!characterSheetScript.rageModeOn) {
				rageMoveSpeed = 1.0f;
			}

			cc.Move(movement * characterSheetScript.moveSpeed * rageMoveSpeed * Time.deltaTime);
		}

		// Rotate character depending on the direction they are going
		if (movement != Vector3.zero) {
			transform.forward = movement;
		}
		
		// Add gravity
		cc.Move(charVelocity * Time.deltaTime);

		anim.SetFloat("charSpeed", (Mathf.Abs(moveHorizontal) + Mathf.Abs(moveVertical)));
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


	private void AddGravity() {
		charVelocity.y += Physics.gravity.y * Time.deltaTime;
		if (cc.isGrounded && charVelocity.y < 0) {
			charVelocity.y = 0.0f;
		}
	}


	private void ShowSkillboard() {
		this.gameObject.transform.GetChild(2).gameObject.SetActive(skillBoardOn);
		this.gameObject.transform.GetChild(3).gameObject.SetActive(!skillBoardOn);
	}

}
