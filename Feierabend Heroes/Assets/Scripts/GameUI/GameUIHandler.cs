using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Rewired;

public class GameUIHandler : MonoBehaviour {

	public GameObject charCardGO;
	public GameObject cardsParent;

	public AudioSource startMatchSound;

	private float startPosX = 283;

	public static int connectedGamepads;
	private int playerMax = 4;

	private bool startedLevel = false;


	private void Awake() {
		// Reset all stats
		for (int i = 0; i < SettingsHolder.playerCount; i++) {
			SettingsHolder.playerClasses[i] = -1;
			SettingsHolder.charNames[i] = "";
			SettingsHolder.playerCount = 0;
			SettingsHolder.registeredPlayers = 0;
			SettingsHolder.matchOver = false;
			SettingsHolder.initialSpawn = false;
		}

		ReInput.ControllerConnectedEvent += OnControllerConnected;
		ReInput.ControllerDisconnectedEvent += OnControllerDisconnected;

		connectedGamepads = 2; //ReInput.controllers.joystickCount;

		for (int i = 0; i < playerMax; i++) {
			GameObject newCharCard = Instantiate(charCardGO);
			newCharCard.transform.SetParent(cardsParent.transform, false);
			newCharCard.transform.localPosition = new Vector3(startPosX * i, 0, 0);

			newCharCard.GetComponent<CharCardsHandler>().charID = i;
		}
	}


	private void Update() {
		if (ReInput.players.GetPlayer(0).GetButtonDown("Options") && !startedLevel) {
			if (SettingsHolder.registeredPlayers >= 2 && SettingsHolder.registeredPlayers == connectedGamepads) {
				startedLevel = true;

				Instantiate(startMatchSound);
				
				SettingsHolder.playerCount = connectedGamepads;
				SceneManager.LoadScene("3 Aeras");

				// DEV TESTING
				// SettingsHolder.playerCount = 4;
				// SceneManager.LoadScene("2 TestLevel");
			}
		}
	}


	void OnControllerConnected(ControllerStatusChangedEventArgs args) {
		if (connectedGamepads < playerMax) {
			connectedGamepads = ReInput.controllers.joystickCount;
			// print("A controller was connected! Name = " + args.name + " Id = " + args.controllerId + " Type = " + args.controllerType);
		} else {
			print("No more controllers allowed");
		}
  }


	void OnControllerDisconnected(ControllerStatusChangedEventArgs args) {
		if (connectedGamepads > 0) {
			connectedGamepads = ReInput.controllers.joystickCount;
			// print("A controller was disconnected! Name = " + args.name + " Id = " + args.controllerId + " Type = " + args.controllerType);
		} else {
			print("No more controllers to disconnect");
		}
  }
	
}