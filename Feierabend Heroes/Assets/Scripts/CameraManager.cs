using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraManager : MonoBehaviour {

	public Camera playerCamera;
	public GameObject listSkills;
	public GameObject cardSkills;
	private GameObject skillUIGO;
	public GameObject charUIGO;
	public GameObject timerUIGO;

	private float camWidth = 0.5f;
	private float camHeight = 0.5f;
	private float camPosX;
	private float camPosY;


	public void InstantiateCams() {
		// Destroy(GameObject.Find("DevCam"));

		for (int i = 0; i < SettingsHolder.playerCount; i++) {

			Camera newCam = Instantiate(playerCamera);
			// SettingsHolder.camArr.Add(newCam);

			switch (i) {
				case 0:
					camPosX = 0.0f;
					camPosY = 0.5f;
					break;
				case 1:
					camPosX = 0.5f;
					camPosY = 0.5f;
					break;
				case 2:
					camPosX = 0.0f;
					camPosY = 0.0f;
					break;
				case 3:
					camPosX = 0.5f;
					camPosY = 0.0f;
					break;
			}

			if (SettingsHolder.playerCount == 2) {
				camPosY = 0.0f;
				camHeight = 1.0f;
			}

			// Single player camera for dev testing
			if (SettingsHolder.playerCount == 1) {
				camPosX = 0.0f;
				camPosY = 0.0f;
				camHeight = 1.0f;
				camWidth = 1.0f;
			}

			newCam.name = "PlayerCamera" + i;
			newCam.GetComponent<CameraFollow>().cameraID = i;
			newCam.GetComponent<Camera>().cullingMask ^= (1 << 10+i); // (to toggle layer 10+i)
			newCam.rect = new Rect(camPosX, camPosY, camWidth, camHeight);

			// Set transforms for the lookAt target cameras | isometric and 3rd person
			newCam.GetComponent<CameraFollow>().target = GameObject.Find("Character" + i).transform;
			newCam.GetComponent<CameraFollow>().tpTarget = GameObject.Find("Character" + i).transform.GetChild(4).transform;

			SetSkillMode();

			// Instantiate skill UI for every player
			GameObject newSkillUI = Instantiate(skillUIGO);
			newSkillUI.name = "SkillUI" + i;
			newSkillUI.transform.SetParent(GameObject.Find("Character" + i).transform);
			newSkillUI.transform.GetChild(0).GetComponent<Canvas>().worldCamera = newCam;
			newSkillUI.transform.GetChild(0).GetComponent<Canvas>().planeDistance = 1;
			
			if (SettingsHolder.skillMode == 0) {
				newSkillUI.GetComponent<SkillBoardHandler>().InitializeSkillUI();
			} else {
				newSkillUI.GetComponent<SkillCardsHandler>().InitializeSkillUI();
			}

			// Instantiate character UI for every player
			GameObject newCharUI = Instantiate(charUIGO);
			newCharUI.name = "CharUI" + i;
			newCharUI.transform.SetParent(GameObject.Find("Character" + i).transform);
			newCharUI.GetComponent<Canvas>().worldCamera = newCam;
			newCharUI.GetComponent<Canvas>().planeDistance = 1;
			newCharUI.GetComponent<UIHandler>().InitializeCharUI();

			// Single player skill board UI for dev testing
			if (SettingsHolder.playerCount == 1) {
				// print(newCharUI.transform.GetChild(0).name);
				newSkillUI.transform.GetChild(0).GetComponent<UnityEngine.UI.CanvasScaler>().referenceResolution = new Vector2(1440, 900);
				newCharUI.GetComponent<UnityEngine.UI.CanvasScaler>().referenceResolution = new Vector2(1440, 900);
			}

		}
	}


	public void SpawnUITimer() {
		for (int i = 0; i < SettingsHolder.playerCount; i++) {
			Camera playerCam = GameObject.Find("PlayerCamera" + i).GetComponent<Camera>();

			// Instantiate timer UI for every player
			GameObject newTimerUI = Instantiate(timerUIGO);
			newTimerUI.name = "TimerUI" + i;
			// newTimerUI.transform.SetParent(playerCam.transform);
			newTimerUI.GetComponent<Canvas>().worldCamera = playerCam;
			newTimerUI.GetComponent<Canvas>().planeDistance = 1;
			GetComponent<TimeHandler>().levelStartTimerText.Add(newTimerUI.transform.GetChild(0).GetComponent<TMP_Text>());
		}
	}


	private void SetSkillMode() {
		if (SettingsHolder.skillMode == 0) {
			skillUIGO = listSkills;
		} else {
			skillUIGO = cardSkills;
		}
	}

}