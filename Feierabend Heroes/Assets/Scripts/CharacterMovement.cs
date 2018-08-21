using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

	public int charID = 0;

	private CharacterController controller;
	public LayerMask Ground;

	private float moveSpeed;
	private float jumpHeight;
	private bool stopMovement;

	private float _gravity;
	private Vector3 _velocity;
	// private int mag;

	private bool _isGrounded = true;
	private Transform groundChecker;
	private float groundDistance = 0.2f;


	void Start ()
	{
		// Get external components
		controller = this.gameObject.GetComponent<CharacterController>();
		groundChecker = transform.GetChild(0);
		_gravity = Physics.gravity.y * 4f;

		// Grabbing the stats from the Stats Sheet
		moveSpeed = CharacterStats.characterSpeed[charID];
		jumpHeight = CharacterStats.characterJumpHeight[charID];
	}


	void Update ()
	{
		// Check if the character is grounded
		_isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, Ground, QueryTriggerInteraction.Ignore);
        if (_isGrounded && _velocity.y < 0) {
            _velocity.y = 0f;
		}

		// Get the input and normalize diagonal movement
		Vector3 move = new Vector3(Input.GetAxis(InputScript.gamepadInput[charID, 14]), 0, Input.GetAxis(InputScript.gamepadInput[charID, 15]));
		move = move.normalized;

		// Move the character – but only if they are not attacking
		if (!stopMovement) {
			controller.Move(move * Time.deltaTime * moveSpeed);
		}
        if (move != Vector3.zero) {
            transform.forward = move;
		}
		
		// Tracking the characters speed
		// var vel = controller.velocity;
		// mag = Mathf.RoundToInt(vel.magnitude);
		// print(mag);

		// Jumping 
		if (Input.GetButtonDown(InputScript.gamepadInput[charID, 5]) && _isGrounded) {
			_velocity.y += Mathf.Sqrt(jumpHeight * -2f * _gravity);
		}

		// Attacking
		if (Input.GetButtonDown(InputScript.gamepadInput[charID, 0])) {
			stopMovement = true;
			this.gameObject.GetComponent<Attack>().shootProjectile();
		} else {
			stopMovement = false;
		}

		// Applying gravity to the character
		_velocity.y += _gravity * Time.deltaTime;
		controller.Move(_velocity * Time.deltaTime);
	}

}
