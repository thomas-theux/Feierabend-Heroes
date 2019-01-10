using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	public Camera playerCamera;
	public GameObject skillUIGO;

	private float camWidth = 0.5f;
	private float camHeight = 0.5f;
	private float camPosX;
	private float camPosY;


	public void InstantiateCams() {
		Destroy(GameObject.Find("DevCam"));

		for (int i = 0; i < GameManager.playerCount; i++) {

			Camera newCam = Instantiate(playerCamera);

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

			if (GameManager.playerCount == 2) {
				camPosY = 0;
				camHeight = 1.0f;
			}

			newCam.name = "PlayerCamera" + i;
			newCam.rect = new Rect(camPosX, camPosY, camWidth, camHeight);
			newCam.GetComponent<CameraFollow>().target = GameObject.Find("Character" + i).transform;


			GameObject newSkillUI = Instantiate(skillUIGO);

			newSkillUI.name = "SkillUI" + i;
			newSkillUI.transform.parent = GameObject.Find("Character" + i).transform;
			newSkillUI.transform.GetChild(0).GetComponent<Canvas>().worldCamera = GameObject.Find("PlayerCamera" + i).gameObject.GetComponent<Camera>();
			newSkillUI.transform.GetChild(0).GetComponent<Canvas>().planeDistance = 1;

			newSkillUI.GetComponent<SkillTreeUIHandler>().InitializeSkillUI();
		}
	}

}