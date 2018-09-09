using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsSelector : MonoBehaviour {

	private int selectorID = 0;

	private int[] selectorPosY = {352, 322, 292, 262, 232};
	private int posIndex = 0;

	private bool dpadPressed = false;
	private bool stickPressed = false;


	void Update()
	{
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
	}

}
