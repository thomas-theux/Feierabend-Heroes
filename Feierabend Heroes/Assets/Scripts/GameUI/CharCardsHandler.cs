using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class CharCardsHandler : MonoBehaviour {

	public int charID = 0;
	private int currentStatus = 0;
	private bool isConnected = false;

	public Image gamepadIcon;
	public GameObject pressToJoinGO;
	public GameObject charClassGO;
	public GameObject arrowNavGO;

	// REWIRED
	private bool selectButton;
	private bool cancelButton;


	private void Update() {
		GetInput();
		CheckForGamepad();
		CheckForInput();
	}


	private void GetInput() {
		selectButton = ReInput.players.GetPlayer(charID).GetButtonDown("X");
		cancelButton = ReInput.players.GetPlayer(charID).GetButtonDown("Circle");
	}


	private void CheckForGamepad() {
		if (GameUIHandler.connectedGamepads >= charID+1 && !isConnected) {
			currentStatus = 1;
			isConnected = true;
			DisplayCurrentStatus();
		} else if (GameUIHandler.connectedGamepads < charID+1 && isConnected) {
			currentStatus = 0;
			isConnected = false;
			DisplayCurrentStatus();
		}
	}


	private void CheckForInput() {
		if (currentStatus == 1) {
			if (selectButton) {
				currentStatus = 2;
			}
		}
	}


	private void DisplayCurrentStatus() {
		switch(currentStatus) {
			case 0:
				gamepadIcon.color = new Color32(255, 255, 255, 50);
				pressToJoinGO.SetActive(false);
				break;
			case 1:
				gamepadIcon.color = new Color32(255, 255, 255, 255);
				pressToJoinGO.SetActive(true);

				charClassGO.SetActive(false);
				arrowNavGO.SetActive(false);
				break;
			case 2:
				gamepadIcon.color = new Color32(255, 255, 255, 0);
				pressToJoinGO.SetActive(false);

				charClassGO.SetActive(true);
				arrowNavGO.SetActive(true);
				break;
			case 3:
				break;
		}
	}

}
