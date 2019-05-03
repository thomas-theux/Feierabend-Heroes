﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Rewired;

public class GameUIHandler : MonoBehaviour {

	public GameObject charCardGO;
	public GameObject cardsParent;

    public AudioSource cancelSound;
	public AudioSource startMatchSound;

	// private float startPosX = 283;
	private Vector2[] positionsArr = {
		new Vector2(0, 0),
		new Vector2(705, 0),
		new Vector2(0, -370),
		new Vector2(705, -370)
	};

	public static int connectedGamepads;
	public static bool selectionComplete = false;
	private int playerMax = 4;

	private bool startedLevel = false;


	private void Awake() {
		selectionComplete = false;

		// Reset all stats
		for (int i = 0; i < SettingsHolder.playerCount; i++) {
			SettingsHolder.playerClasses[i] = -1;
			SettingsHolder.heroNames[i] = "";
			SettingsHolder.playerCount = 0;
			SettingsHolder.registeredPlayers = 0;
			SettingsHolder.matchOver = false;
			SettingsHolder.initialSpawn = false;
		}

		ReInput.ControllerConnectedEvent += OnControllerConnected;
		ReInput.ControllerDisconnectedEvent += OnControllerDisconnected;

		// connectedGamepads = ReInput.controllers.joystickCount;
		connectedGamepads = 4;

		for (int i = 0; i < playerMax; i++) {
			GameObject newCharCard = Instantiate(charCardGO, cardsParent.transform, false);
			// newCharCard.transform.SetParent(cardsParent.transform, false);
			// newCharCard.transform.localPosition = new Vector3(startPosX * i, 0, 0);
			newCharCard.transform.localPosition = positionsArr[i];

			newCharCard.GetComponent<CharCardsHandler>().charID = i;
		}
	}


	// Wieviele verschiedene Möglichkeiten für random names gibt es?
	// private void Start() {		
	// 	int titles = CharClassContent.titleTexts.Length;
	// 	int adjectives = CharClassContent.adjectiveTexts.Length;
	// 	int names = CharClassContent.nameTexts.Length;

	// 	int optionsPartOne = titles * names;
	// 	int optionsPartTwo = adjectives * names;
	// 	int optionsPartThree = titles * adjectives * names;

	// 	int allOptions = optionsPartOne + optionsPartTwo + optionsPartThree;

	// 	print(allOptions.ToString("n0"));
	// }


	private void Update() {
		if (!startedLevel) {
			if (SettingsHolder.registeredPlayers >= 2 && SettingsHolder.registeredPlayers == connectedGamepads) {
				startedLevel = true;
				StartCoroutine(StartLevel());
			}
		}

		// if (ReInput.players.GetPlayer(0).GetButtonDown("Options") && !startedLevel) {
		// }

        if (ReInput.players.GetPlayer(0).GetButtonDown("Circle")) {
            Instantiate(cancelSound);
            SceneManager.LoadScene("2 Map Selection");
        }
	}


	void OnControllerConnected(ControllerStatusChangedEventArgs args) {
		if (connectedGamepads < playerMax) {
			// connectedGamepads = ReInput.controllers.joystickCount;
		} else {
			print("No more controllers allowed");
		}
	}


	void OnControllerDisconnected(ControllerStatusChangedEventArgs args) {
		if (connectedGamepads > 0) {
			// connectedGamepads = ReInput.controllers.joystickCount;
		} else {
			print("No more controllers to disconnect");
		}
	}


	private IEnumerator StartLevel() {
		selectionComplete = true;

		yield return new WaitForSeconds(0.5f);

		SettingsHolder.playerCount = connectedGamepads;
		Instantiate(startMatchSound);

		yield return new WaitForSeconds(0.1f);

		SceneManager.LoadScene(SettingsHolder.selectedMap);
	}
	
}