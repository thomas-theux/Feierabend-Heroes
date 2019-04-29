using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerMovement : MonoBehaviour {

    private int playerID = 0;

    private CharacterController cc;

    public float moveSpeed;
    private Vector3 moveDirection;

    // Rewired
	private float moveHorizontal;
	private float moveVertical;


    private void Start() {
        cc = GetComponent<CharacterController>();
    }


    private void Update() {
        moveHorizontal = ReInput.players.GetPlayer(playerID).GetAxis("LS Horizontal");
        moveVertical = ReInput.players.GetPlayer(playerID).GetAxis("LS Vertical");
    }


    private void FixedUpdate() {
        moveDirection = (transform.forward * moveVertical) + (transform.right * moveHorizontal);
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1);

        cc.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

}
