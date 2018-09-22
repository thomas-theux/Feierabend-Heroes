using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour {

	public int cursorID;

	private float[] textPositions = {84, 52, 20, -12, -44};
	private int positionIndex = 0;

	private bool dpadPressed;
	private bool stickPressed;

	private GameObject characterInstance;

	public Text healthStat;
	public Text attackStat;
	public Text defenseStat;
	public Text speedStat;
	public Text luckStat;
	public Text availablePoints;

	private int healthIncrease = 10;
	private float attackIncrease = 2;
	private int defenseIncrease = 2;
	private float speedIncrease = 0.3f;
	private int luckIncrease = 1;

	public static bool enableDistribution;


	void Start()
	{
		characterInstance = GameObject.Find("Character0" + cursorID + "(Clone)");
	}


	void Update()
	{
		if (enableDistribution) {
			// If the character has more than 0 stat points, they can assign them
			if (characterInstance.GetComponent<CharacterStats>().currentStatPoints > 0) {

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
				this.transform.localPosition = new Vector3(14, textPositions[positionIndex], 0);

				// Assign stat point by pressing button
				if (Input.GetButtonDown(InputScript.gamepadInput[cursorID, 0])) {
					switch (positionIndex) {
						case 0:
							characterInstance.GetComponent<CharacterStats>().characterHealth += healthIncrease;
							characterInstance.GetComponent<CharacterStats>().currentStatPoints--;
							break;
						case 1:
							characterInstance.GetComponent<CharacterStats>().characterAttackMin += attackIncrease;
							characterInstance.GetComponent<CharacterStats>().characterAttackMax += attackIncrease;
							characterInstance.GetComponent<CharacterStats>().currentStatPoints--;
							break;
						case 2:
							characterInstance.GetComponent<CharacterStats>().characterDefense += defenseIncrease;
							characterInstance.GetComponent<CharacterStats>().currentStatPoints--;
							break;
						case 3:
							characterInstance.GetComponent<CharacterStats>().characterSpeed += speedIncrease;
							characterInstance.GetComponent<CharacterStats>().currentStatPoints--;
							break;
						case 4:
							characterInstance.GetComponent<CharacterStats>().characterLuck += luckIncrease;
							characterInstance.GetComponent<CharacterStats>().currentStatPoints--;
							break;
					}
				}
			} else {
				this.gameObject.SetActive(false);
			}
		}

		// Display the current stats of the character
		healthStat.text = characterInstance.GetComponent<CharacterStats>().characterHealth + "";
		attackStat.text = characterInstance.GetComponent<CharacterStats>().characterAttackMin + "-" + characterInstance.GetComponent<CharacterStats>().characterAttackMax;
		defenseStat.text = characterInstance.GetComponent<CharacterStats>().characterDefense + "";
		speedStat.text = characterInstance.GetComponent<CharacterStats>().characterSpeed + "";
		luckStat.text = characterInstance.GetComponent<CharacterStats>().characterLuck + "";
		availablePoints.text = characterInstance.GetComponent<CharacterStats>().currentStatPoints + "";
	}

}
