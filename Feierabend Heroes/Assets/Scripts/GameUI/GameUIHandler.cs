using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class GameUIHandler : MonoBehaviour {

	public GameObject[] gamepadsGO;
	public static int connectedGamepads = 0;


	private void Awake() {
		print(ReInput.controllers.joystickCount);
		// print(ReInput.controllers.controllerCount);

		connectedGamepads = ReInput.controllers.joystickCount;
		ActivateGamePads();
	}


	private void OnControllerConnected() {
		print("New controller detected");

        if (connectedGamepads < 4) {
			connectedGamepads++;
			ActivateGamePads();
		}
    }


	private void ActivateGamePads() {
		for (int i = 0; i < connectedGamepads; i++) {
			if (gamepadsGO[i].GetComponent<Image>().color != new Color32(255, 255, 255, 255)) {
				gamepadsGO[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
			}
		}
	}
}
