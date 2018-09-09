using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsSelector : MonoBehaviour {

	private GameObject characterInstance;
	private int selectorID;

	private float[] selectorPosY = {389, 359, 329, 299, 269};
	private int posIndex = 0;
	private float test;

	private bool dpadPressed = false;
	private bool stickPressed = false;


	void Start()
	{
		characterInstance = GameObject.Find("Character0" + selectorID + "(Clone)");
		selectorID = characterInstance.GetComponent<CharacterMovement>().charID;

		// test = GameObject.Find("HealthText").transform.position.y;
		// selectorPosY = {test, test+30, test+60, test+90, test+120};
	}


	void Update()
	{
		// If player has stat points they can navigate and assign them
		if (characterInstance.GetComponent<CharacterStats>().currentStatPoints > 0) {

			// DPad navigation in UI
			if (!dpadPressed) {
				// DOWN
				if (Input.GetAxis(InputScript.gamepadInput[selectorID, 19]) == -1) {
					if (posIndex == selectorPosY.Length -1) { posIndex = 0; }
					else { posIndex++; }
					dpadPressed = true;
				}
				// UP
				if (Input.GetAxis(InputScript.gamepadInput[selectorID, 19]) == 1) {
					if (posIndex == 0) { posIndex = selectorPosY.Length -1; }
					else { posIndex--; }
					dpadPressed = true;
				}
			}

			// Stick navigation in UI
			if (!stickPressed) {
				// DOWN
				if (Input.GetAxisRaw(InputScript.gamepadInput[selectorID, 15]) <= -0.5f) {
					if (posIndex == selectorPosY.Length -1) { posIndex = 0; }
					else { posIndex++; }
					stickPressed = true;
				}
				// UP
				if (Input.GetAxisRaw(InputScript.gamepadInput[selectorID, 15]) >= 0.5f) {
					if (posIndex == 0) { posIndex = selectorPosY.Length -1; }
					else { posIndex--; }
					stickPressed = true;
				}
			}

			// Reset DPad input access
			if (Input.GetAxis(InputScript.gamepadInput[selectorID, 19]) == 0) {
				dpadPressed = false;
			}
			// Reset Stick input access
			if (Input.GetAxis(InputScript.gamepadInput[selectorID, 15]) >= -0.5f && Input.GetAxis(InputScript.gamepadInput[selectorID, 15]) <= 0.5f) {
				stickPressed = false;
			}

			// Displaying the selector
			this.gameObject.GetComponent<RectTransform>().position = new Vector2(
				this.gameObject.GetComponent<RectTransform>().position.x,
				selectorPosY[posIndex]
			);

			// Assign stat point by pressing button
			if (Input.GetButtonDown(InputScript.gamepadInput[selectorID, 0])) {
				switch (posIndex) {
					case 0:
						characterInstance.GetComponent<CharacterStats>().characterHealth += 10;
						characterInstance.GetComponent<CharacterStats>().currentStatPoints--;
						break;
					case 1:
						characterInstance.GetComponent<CharacterStats>().characterAttackMin += 4;
						characterInstance.GetComponent<CharacterStats>().characterAttackMax += 4;
						characterInstance.GetComponent<CharacterStats>().currentStatPoints--;
						break;
					case 2:
						characterInstance.GetComponent<CharacterStats>().characterDefense += 4;
						characterInstance.GetComponent<CharacterStats>().currentStatPoints--;
						break;
					case 3:
						characterInstance.GetComponent<CharacterStats>().characterSpeed += 0.5f;
						characterInstance.GetComponent<CharacterStats>().currentStatPoints--;
						break;
					case 4:
						characterInstance.GetComponent<CharacterStats>().characterLuck += 2;
						characterInstance.GetComponent<CharacterStats>().currentStatPoints--;
						break;
				}
			}

		} else {
			this.gameObject.SetActive(false);
		}
		
	}

}
