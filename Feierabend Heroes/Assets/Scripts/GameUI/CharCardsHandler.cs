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
	public GameObject readyGO;

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
			DisplayCurrentStatus();
		} else if (GameUIHandler.connectedGamepads < charID+1 && isConnected) {
			currentStatus = 0;
			DisplayCurrentStatus();
		}
	}


	private void CheckForInput() {
		if (currentStatus == 1 || currentStatus == 2) {
			if (selectButton) {
				currentStatus++;
				DisplayCurrentStatus();
			}
		}

		if (currentStatus == 2 || currentStatus == 3) {
			if (cancelButton) {
				currentStatus--;
				DisplayCurrentStatus();
			}
		}
	}


	private void DisplayCurrentStatus() {
		switch(currentStatus) {
			case 0:
				isConnected = false;
				gamepadIcon.color = new Color32(255, 255, 255, 50);
				pressToJoinGO.SetActive(false);
				charClassGO.SetActive(false);
				arrowNavGO.SetActive(false);
				readyGO.SetActive(false);
				break;
			case 1:
				isConnected = true;
				gamepadIcon.color = new Color32(255, 255, 255, 255);
				pressToJoinGO.SetActive(true);
				charClassGO.SetActive(false);
				arrowNavGO.SetActive(false);
				readyGO.SetActive(false);
				break;
			case 2:
				isConnected = true;
				gamepadIcon.color = new Color32(255, 255, 255, 0);
				pressToJoinGO.SetActive(false);
				charClassGO.SetActive(true);
				arrowNavGO.SetActive(true);
				readyGO.SetActive(false);
				break;
			case 3:
				isConnected = true;
				gamepadIcon.color = new Color32(255, 255, 255, 0);
				pressToJoinGO.SetActive(false);
				charClassGO.SetActive(true);
				arrowNavGO.SetActive(false);
				readyGO.SetActive(true);
				break;
		}
	}

}
