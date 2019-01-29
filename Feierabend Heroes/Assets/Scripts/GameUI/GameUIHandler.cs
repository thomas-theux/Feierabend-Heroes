using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class GameUIHandler : MonoBehaviour {

	public GameObject[] gamepadsGO;
	public int connectedGamepads = 1;
	private int[] uiStates = {0, 0, 0, 0};
	private int maxGamepads = 4;


	private void Awake() {
		print(ReInput.controllers.controllerCount);

		// connectedGamepads = ReInput.controllers.controllerCount;
		if (connectedGamepads > maxGamepads) {
			connectedGamepads = maxGamepads;
		}

		SetConnectedGamepads();

		ActivateGamePads();
	}


	private void OnControllerConnected() {
		print("New controller detected");

        if (connectedGamepads < maxGamepads) {
			connectedGamepads++;
			ActivateGamePads();
		}
    }


	private void SetConnectedGamepads() {
		for (int i = 0; i < connectedGamepads; i++) {
			if (uiStates[i] < 1) {
				uiStates[i] = 1;
			}
		}
	}


	private void ActivateGamePads() {
		for (int i = 0; i < connectedGamepads; i++) {
			if (uiStates[i] == 1) {
				gamepadsGO[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
			}
		}
	}
}
