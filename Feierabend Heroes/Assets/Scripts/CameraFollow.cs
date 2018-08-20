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

		camIdentifier = CharacterStats.playerCount + cameraID;
		cam = GetComponent<Camera>();

		switch (camIdentifier) {
			case 11:
				// 1 Spieler – Position 1
				cam.rect = new Rect(0, 0, 1, 1);
				break;
			case 12:
				// 2 Spieler – Position 1
				cam.rect = new Rect(0, 0, 0.5f, 1);
				break;
			case 13:
				// 3 Spieler – Position 1
				cam.rect = new Rect(0.25f, 0.5f, 0.5f, 0.5f);
				break;
			case 14:
				// 4 Spieler – Position 1
				cam.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
				break;

			case 22:
				// 2 Spieler – Position 2
				cam.rect = new Rect(0.5f, 0, 0.5f, 1);
				break;
			case 23:
				// 3 Spieler – Position 2
				cam.rect = new Rect(0, 0, 0.5f, 0.5f);
				break;
			case 24:
				// 4 Spieler – Position 2
				cam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
				break;

			case 33:
				// 3 Spieler – Position 3
				cam.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
				break;
			case 34:
				// 4 Spieler – Position 3
				cam.rect = new Rect(0, 0, 0.5f, 0.5f);
				break;

			case 44:
				// 4 Spieler – Position 4
				cam.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
				break;
		}
	}

	void Update () {
		transform.position = characterModel.transform.position + offset;
	}

}
