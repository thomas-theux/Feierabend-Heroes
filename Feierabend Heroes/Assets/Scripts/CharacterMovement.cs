using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class CharacterMovement : MonoBehaviour {

	public int playerID;
	private CharacterController cc;
	private CharacterSheet characterSheetScript;
	private LifeDeathHandler lifeDeathHandlerScript;

	private GameObject campfireTarget;

	public Animator anim;
	public AudioSource openSkillboardSound;
	public AudioSource closeSkillboardSound;

	public bool isAttacking = false;
	public bool skillBoardOn;

	private float rageMoveSpeed = 1;
	private Vector3 charVelocity;

	public bool skillboardBlocksCasting = false;

	// REWIRED
	private float moveHorizontal;
	private float moveVertical;
	public bool activationBtn;
	private bool showSkillUI;
	private bool closeSkillUI;


	private void Awake() {
		// Get stats from skill script
		characterSheetScript = GetComponent<CharacterSheet>();
		lifeDeathHandlerScript = GetComponent<LifeDeathHandler>();
		// characterSheetScript.charID = playerID;

		cc = this.gameObject.GetComponent<CharacterController>();
	}


	void OnEnable() {
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}
         
	void OnDisable() {
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}


	private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
		cc.enabled = true;
		anim.SetBool("charDies", false);
		anim.SetBool("charWins", false);

		characterSheetScript.charID = playerID;

		campfireTarget = GameObject.Find("CampfireTarget");
		// transform.LookAt(campfireTarget.transform);
	}


	private void Update() {
		GetInput();
		ToggleSkillboard();
		AddGravity();

		if (skillBoardOn) {
			anim.SetFloat("charSpeed", 0);
		}

		// Close skillboard when it's open and the match ends
		if (SettingsHolder.matchOver && skillBoardOn || TimeHandler.roundEnd && skillBoardOn) {
			skillBoardOn = false;
			ShowSkillboard();
		}
	}


	private void FixedUpdate() {
		if (!skillBoardOn && !lifeDeathHandlerScript.charIsDead) {
			PlayerMovement();
		}
	}


	private void GetInput() {
		if (!lifeDeathHandlerScript.charIsDead) {
			moveHorizontal = ReInput.players.GetPlayer(playerID).GetAxis("LS Horizontal");
			moveVertical = ReInput.players.GetPlayer(playerID).GetAxis("LS Vertical");
		
			if (TimeHandler.startLevel) {
				activationBtn = ReInput.players.GetPlayer(playerID).GetButtonDown("R1");

				showSkillUI = ReInput.players.GetPlayer(playerID).GetButtonDown("Triangle");
				closeSkillUI = ReInput.players.GetPlayer(playerID).GetButtonDown("Circle");
			}
		} else {
			moveHorizontal = 0;
			moveVertical = 0;

			cc.enabled = false;

			anim.SetBool("charDies", true);

			// Close skill board if it is open when this character dies
			if (skillBoardOn) {
				skillBoardOn = false;
				Instantiate(openSkillboardSound);
				ShowSkillboard();
			}
		}
	}


	private void PlayerMovement() {
		// Movement of the character
		Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
		// movement = movement.normalized;
		movement = Vector3.ClampMagnitude(movement, 1);

		if (!isAttacking && TimeHandler.startLevel) {
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

		if (!isAttacking || TimeHandler.startLevel) {
			anim.SetFloat("charSpeed", (Mathf.Abs(moveHorizontal) + Mathf.Abs(moveVertical)));
		}
		if (isAttacking || !TimeHandler.startLevel || skillBoardOn) {
			anim.SetFloat("charSpeed", 0);
		}
	}


	private void ToggleSkillboard() {
		if (showSkillUI) {
			skillBoardOn = !skillBoardOn;
			Instantiate(openSkillboardSound);
			ShowSkillboard();
		}
		if (skillBoardOn && closeSkillUI) {
			skillBoardOn = false;
			Instantiate(closeSkillboardSound);
			ShowSkillboard();
			
			skillboardBlocksCasting = true;
			StartCoroutine(SkillCastDelay());
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


	private IEnumerator SkillCastDelay() {
		yield return new WaitForSeconds(0.1f);
		skillboardBlocksCasting = false;
	}

}
