using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour {

	public int cursorID;

	private float[] textPositions = {80, 40, 0, -40, -80};
	private int positionIndex = 0;

	private bool dpadPressed;
	private bool stickPressed;

	private GameObject characterGO;

	public Text healthStat;
	public Text attackStat;
	public Text defenseStat;
	public Text speedStat;
	public Text luckStat;


	void Start()
	{
		characterGO = GameObject.Find("Character0" + cursorID + "(Clone)");
	}


	void Update()
	{
		// If the character has more than 0 stat points, they can assign them
		if (characterGO.GetComponent<CharacterStats>().currentStatPoints > 0) {

			this.gameObject.SetActive(true);

			// DPad navigation in UI
			if (!dpadPressed) {
				// DOWN
				if (Input.GetAxis(InputScript.gamepadInput[cursorID, 19]) == -1) {
					if (positionIndex == textPositions.Length -1) { positionIndex = 0; }
					else { positionIndex++; }
					dpadPressed = true;
				}
				// UP
				if (Input.GetAxis(InputScript.gamepadInput[cursorID, 19]) == 1) {
					if (positionIndex == 0) { positionIndex = textPositions.Length -1; }
					else { positionIndex--; }
					dpadPressed = true;
				}
			}
			// Stick navigation in UI
			if (!stickPressed) {
				// DOWN
				if (Input.GetAxisRaw(InputScript.gamepadInput[cursorID, 15]) <= -0.5f) {
					if (positionIndex == textPositions.Length -1) { positionIndex = 0; }
					else { positionIndex++; }
					stickPressed = true;
				}
				// UP
				if (Input.GetAxisRaw(InputScript.gamepadInput[cursorID, 15]) >= 0.5f) {
					if (positionIndex == 0) { positionIndex = textPositions.Length -1; }
					else { positionIndex--; }
					stickPressed = true;
				}
			}

			// Reset DPad input access
			if (Input.GetAxis(InputScript.gamepadInput[cursorID, 19]) == 0) {
				dpadPressed = false;
			}
			// Reset Stick input access
			if (Input.GetAxis(InputScript.gamepadInput[cursorID, 15]) >= -0.5f && Input.GetAxis(InputScript.gamepadInput[cursorID, 15]) <= 0.5f) {
				stickPressed = false;
			}

			// Display the cursor at the right position
			this.transform.localPosition = new Vector3(0, textPositions[positionIndex], 0);
		} else {
			this.gameObject.SetActive(false);
		}

		// Display the current stats of the character
		healthStat.text = characterGO.GetComponent<CharacterStats>().characterHealth + "";
		attackStat.text = characterGO.GetComponent<CharacterStats>().characterAttackMin + " – " + characterGO.GetComponent<CharacterStats>().characterAttackMax;
		defenseStat.text = characterGO.GetComponent<CharacterStats>().characterDefense + "";
		speedStat.text = characterGO.GetComponent<CharacterStats>().characterSpeed + "";
		luckStat.text = characterGO.GetComponent<CharacterStats>().characterLuck + "";
	}

}
