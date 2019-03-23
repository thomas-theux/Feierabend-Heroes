// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CameraFirstPerson : MonoBehaviour {

// 	public Camera playerCamera;
// 	public GameObject skillUIGO;
// 	public GameObject charUIGO;

// 	private float camWidth = 0.5f;
// 	private float camHeight = 0.5f;
// 	private float camPosX;
// 	private float camPosY;


// 	public void InstantiateCams() {
// 		Destroy(GameObject.Find("DevCam"));

// 		for (int i = 0; i < SettingsHolder.playerCount; i++) {

// 			Camera newCam = Instantiate(playerCamera);

// 			switch (i) {
// 				case 0:
// 					camPosX = 0.0f;
// 					camPosY = 0.5f;
// 					break;
// 				case 1:
// 					camPosX = 0.5f;
// 					camPosY = 0.5f;
// 					break;
// 				case 2:
// 					camPosX = 0.0f;
// 					camPosY = 0.0f;
// 					break;
// 				case 3:
// 					camPosX = 0.5f;
// 					camPosY = 0.0f;
// 					break;
// 			}

// 			if (SettingsHolder.playerCount == 2) {
// 				camPosY = 0.0f;
// 				camHeight = 1.0f;
// 			}

// 			// Single player camera for dev testing
// 			if (SettingsHolder.playerCount == 1) {
// 				camPosX = 0.0f;
// 				camPosY = 0.0f;
// 				camHeight = 1.0f;
// 				camWidth = 1.0f;
// 			}

// 			newCam.name = "PlayerCamera" + i;
// 			newCam.transform.parent = GameObject.Find("Character" + i).transform;
// 			newCam.rect = new Rect(camPosX, camPosY, camWidth, camHeight);

// 			// Instantiate skill UI for every player
// 			GameObject newSkillUI = Instantiate(skillUIGO);
// 			newSkillUI.name = "SkillUI" + i;
// 			newSkillUI.transform.parent = GameObject.Find("Character" + i).transform;
// 			newSkillUI.transform.GetChild(0).GetComponent<Canvas>().worldCamera = GameObject.Find("PlayerCamera" + i).gameObject.GetComponent<Camera>();
// 			newSkillUI.transform.GetChild(0).GetComponent<Canvas>().planeDistance = 1;
// 			newSkillUI.GetComponent<SkillTreeUIHandler>().InitializeSkillUI();

// 			// Instantiate character UI for every player
// 			GameObject newCharUI = Instantiate(charUIGO);
// 			newCharUI.name = "CharUI" + i;
// 			newCharUI.transform.SetParent(GameObject.Find("Character" + i).transform);
// 			newCharUI.GetComponent<Canvas>().worldCamera = GameObject.Find("PlayerCamera" + i).gameObject.GetComponent<Camera>();
// 			newCharUI.GetComponent<Canvas>().planeDistance = 1;
// 			newCharUI.GetComponent<UIHandler>().InitializeCharUI();
// 		}
// 	}

// }