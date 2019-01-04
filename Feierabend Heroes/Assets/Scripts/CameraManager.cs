using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	public Camera playerCamera;

	private float camWidth = 0.5f;
	private float camHeight = 0.5f;
	private float camPosX;
	private float camPosY;


	private void Start() {
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

			newCam.rect = new Rect(camPosX, camPosY, camWidth, camHeight);
			newCam.GetComponent<CameraFollow>().target = GameObject.Find("Character" + i).transform;
		}
	}

}