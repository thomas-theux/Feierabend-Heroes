using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

	private CharacterController cc;
	public float Speed = 10f;
	public float jumpHeight = 2f;
	public float jumpForce = 2f;
	public float groundDistance = 0.2f;
	public LayerMask Ground;

	private Vector3 velocity;
	private bool isGrounded = true;
	private Transform groundChecker;
	private int mag;


	void Start () {
		cc = GetComponent<CharacterController>();
		groundChecker = transform.GetChild(0);
	}


	void Update () {
		isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, Ground, QueryTriggerInteraction.Ignore);
		if (isGrounded && velocity.y < 0)
			velocity.y = 0f;

		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		move = move.normalized;
    	cc.Move(move * Time.deltaTime * Speed);
        // if (move != Vector3.zero)
        //     transform.forward = move;

		if (Input.GetButtonDown("Jump") && isGrounded)
			// velocity.y += Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
			velocity.y += jumpForce;

		velocity.y += Physics.gravity.y * Time.deltaTime;
		cc.Move(velocity * Time.deltaTime);


		// Tracking the characters speed
		var vel = cc.velocity;
		mag = Mathf.RoundToInt(vel.magnitude);
		print(mag);
	}

}
