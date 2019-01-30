using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class GameUIHandler : MonoBehaviour {

	public GameObject charCardGO;
	public GameObject cardsParent;

	private float startPosX = 283;

	public static int connectedGamepads;
	private int playerMax = 1;

	public static int[] playerClasses = {-1, -1, -1, -1};
	private List<int> activeGamepads = new List<int>();


	private void Awake() {
		ReInput.ControllerConnectedEvent += OnControllerConnected;
		connectedGamepads = ReInput.controllers.joystickCount;

		for (int i = 0; i < playerMax; i++) {
			GameObject newCharCard = Instantiate(charCardGO);
			newCharCard.transform.SetParent(cardsParent.transform, false);
			newCharCard.transform.localPosition = new Vector3(startPosX * i, 0, 0);

			newCharCard.GetComponent<CharCardsHandler>().charID = i;
		}
	}


	void OnControllerConnected(ControllerStatusChangedEventArgs args) {
		if (connectedGamepads < playerMax) {
			print("A controller was connected! Name = " + args.name + " Id = " + args.controllerId + " Type = " + args.controllerType);
		} else {
			print("No more controllers allowed");
		}
    }
	
}