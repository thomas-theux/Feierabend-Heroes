using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public int cameraID = 0;

	private GameObject characterModel;
	private Vector3 offset;
	private int camIdentifier;

	private Camera cam;

	void Start ()
	{
		characterModel = GameObject.Find("Character0" + (cameraID / 10) + ("(Clone)"));
		offset = new Vector3(0f, 24f, -24f);
		transform.Rotate(50f, 0, 0);

		camIdentifier = GameManager.playerCount + cameraID;
		cam = this.gameObject.GetComponent<Camera>();

		switch (camIdentifier) {
			case 1:
				// 1 Spieler – Position 1
				cam.rect = new Rect(0, 0, 1, 1);
				break;
			case 2:
				// 2 Spieler – Position 1
				cam.rect = new Rect(0, 0, 0.498f, 1);
				break;
			case 3:
				// 3 Spieler – Position 1
				cam.rect = new Rect(0.251f, 0.502f, 0.498f, 0.498f);
				break;
			case 4:
				// 4 Spieler – Position 1
				cam.rect = new Rect(0, 0.502f, 0.498f, 0.498f);
				break;

			case 12:
				// 2 Spieler – Position 2
				cam.rect = new Rect(0.501f, 0, 0.498f, 1);
				break;
			case 13:
				// 3 Spieler – Position 2
				cam.rect = new Rect(0, 0, 0.498f, 0.498f);
				break;
			case 14:
				// 4 Spieler – Position 2
				cam.rect = new Rect(0.501f, 0.502f, 0.498f, 0.498f);
				break;

			case 23:
				// 3 Spieler – Position 3
				cam.rect = new Rect(0.501f, 0, 0.498f, 0.498f);
				break;
			case 24:
				// 4 Spieler – Position 3
				cam.rect = new Rect(0, 0, 0.498f, 0.498f);
				break;

			case 34:
				// 4 Spieler – Position 4
				cam.rect = new Rect(0.501f, 0, 0.498f, 0.498f);
				break;
		}
	}

	void Update () {
		transform.position = characterModel.transform.position + offset;
	}

}
