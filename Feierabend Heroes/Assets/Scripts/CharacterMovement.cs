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
	public GameManager gameManagerScript;

	private GameObject campfireTarget;

	public Animator anim;
	public AudioSource openSkillboardSound;
	public AudioSource closeSkillboardSound;

	public bool isAttacking = false;
	public bool skillBoardOn;

	private Vector3 charVelocity;

	public bool skillboardBlocksCasting = false;
	public bool toggleSkillsDisabled = false;

	private bool isDodging = false;
	private float dodgeDelayTime = 0.35f;
	private float dodgeDistance = 5.0f;
	private Vector3 dodgeDrag = new Vector3(10.0f, 10.0f, 10.0f);

	private float charMoveSpeed;

	// REWIRED
	private float moveHorizontal;
	private float moveVertical;
	public bool interactBtn;
	private bool dodgeBtn;
	private bool pauseBtn;
	private bool triangleBtn;
	private bool circleBtn;


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
		transform.LookAt(campfireTarget.transform);

		// Check if player is public enemy and transform their size
		if (SettingsHolder.publicEnemy == playerID) {
			transform.GetChild(0).transform.localScale = new Vector3(SettingsHolder.sizeIncrease, SettingsHolder.sizeIncrease, SettingsHolder.sizeIncrease);
			GetComponent<CapsuleCollider>().radius = SettingsHolder.sizeIncrease;
			GetComponent<CapsuleCollider>().height = GetComponent<CapsuleCollider>().height * SettingsHolder.sizeIncrease;
			GetComponent<CapsuleCollider>().center = new Vector3(0, 2.2f, 0);
			charMoveSpeed = characterSheetScript.moveSpeed * SettingsHolder.speedDecrease;
		} else {
			transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
			GetComponent<CapsuleCollider>().radius = 1.0f;
			GetComponent<CapsuleCollider>().height = 3.6f;
			GetComponent<CapsuleCollider>().center = new Vector3(0, 0.8f, 0);
			charMoveSpeed = characterSheetScript.moveSpeed;
		}
	}


	private void Update() {
		GetInput();
		ToggleSkillboard();
		AddGravity();

		if (skillBoardOn) {
			anim.SetFloat("charSpeed", 0);
		}

		if (pauseBtn) {
			CheckPauseMenu();
		}

		if (GameManager.pauseMenuOpen) {
			if (circleBtn && playerID == GameManager.whoPaused) {
				gameManagerScript.QuitMatch();
			}
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
			if (!isDodging) {
				moveHorizontal = ReInput.players.GetPlayer(playerID).GetAxis("LS Horizontal");
				moveVertical = ReInput.players.GetPlayer(playerID).GetAxis("LS Vertical");
			}
		
			if (TimeHandler.startLevel) {
				dodgeBtn = ReInput.players.GetPlayer(playerID).GetButtonDown("R1");
				interactBtn = ReInput.players.GetPlayer(playerID).GetButtonDown("L1");

				pauseBtn = ReInput.players.GetPlayer(playerID).GetButtonDown("Options");

				triangleBtn = ReInput.players.GetPlayer(playerID).GetButtonDown("Triangle");
				circleBtn = ReInput.players.GetPlayer(playerID).GetButtonDown("Circle");
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
			cc.Move(movement * charMoveSpeed * Time.deltaTime);
		}

		// Activate dodge movement
		if (dodgeBtn && !isDodging) {

			isDodging = true;
			anim.SetBool("isDodging", true);
			StartCoroutine(DodgeDelay());

			characterSheetScript.currentHealth -= characterSheetScript.maxHealth * 0.05f;

            charVelocity += Vector3.Scale(transform.forward, dodgeDistance * new Vector3(
				(Mathf.Log(1f / (Time.deltaTime * dodgeDrag.x + 1)) / -Time.deltaTime),
				0,
				(Mathf.Log(1f / (Time.deltaTime * dodgeDrag.z + 1)) / -Time.deltaTime))
			);

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

		// Add drag to character so that the dash doesn't go forever
		charVelocity.x /= 1 + dodgeDrag.x * Time.deltaTime;
		charVelocity.y /= 1 + dodgeDrag.y * Time.deltaTime;
		charVelocity.z /= 1 + dodgeDrag.z * Time.deltaTime;
	}


	private void ToggleSkillboard() {
		if (!GameManager.pauseMenuOpen && !toggleSkillsDisabled) {
			if (triangleBtn) {
				skillBoardOn = !skillBoardOn;
				Instantiate(openSkillboardSound);
				ShowSkillboard();
			}
			if (skillBoardOn && circleBtn) {
				skillBoardOn = false;
				Instantiate(closeSkillboardSound);
				ShowSkillboard();
				
				skillboardBlocksCasting = true;
				StartCoroutine(SkillCastDelay());
			}
		}
	}


	private void AddGravity() {
		charVelocity.y += Physics.gravity.y * Time.deltaTime;
		if (cc.isGrounded && charVelocity.y < 0) {
			charVelocity.y = 0.0f;
		}
	}


	private void ShowSkillboard() {
		this.gameObject.transform.GetChild(3).gameObject.SetActive(skillBoardOn);
		this.gameObject.transform.GetChild(4).gameObject.SetActive(!skillBoardOn);

		// Draw new cards if board is being opened
		if (skillBoardOn) {
			// this.gameObject.transform.GetChild(2).GetComponent<SkillBoardHandler>().DrawRandomCards();
		}
	}


	private IEnumerator SkillCastDelay() {
		yield return new WaitForSeconds(0.1f);
		skillboardBlocksCasting = false;
	}


	private IEnumerator DodgeDelay() {
		yield return new WaitForSeconds(dodgeDelayTime);
		isDodging = false;
		anim.SetBool("isDodging", false);
	}
	

	private void CheckPauseMenu() {
		if (!GameManager.pauseMenuOpen) {
			// Open pause menu
			GameManager.whoPaused = playerID;
			gameManagerScript.HandlePauseMenu();
		} else {
			// Check if the player who pressed the options button is the player who paused the game
			if (playerID == GameManager.whoPaused) {
				// Close pause menu
				gameManagerScript.HandlePauseMenu();
				GameManager.whoPaused = -1;
			}
		}
	}

}
