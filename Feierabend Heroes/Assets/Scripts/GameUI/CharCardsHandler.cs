using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class CharCardsHandler : MonoBehaviour {

	public int charID = 0;
	private int currentStatus = 0;
	private bool isConnected = false;
	
	private int currentIndex = 0;
	private int toggleSkill = 0;
	private int[] charClassesArr = {0, 1};

	public Image gamepadIcon;
	public GameObject pressToJoinGO;
	public GameObject charClassGO;
	public GameObject arrowNavGO;
	public GameObject readyGO;

	private CharClassContent charClassContentScript;

	public Text classText;
	public Text charHPText;
	public Text charDEFText;
	public Image classImage;
	public Text skillsText;
	public Image attackOneImage;
	public Text attackOneTitleText;
	public Text attackOneText;
	public Image attackTwoImage;
	public Text attackTwoTitleText;
	public Text attackTwoText;
	public Image pageControlOneGO;
	public Image pageControlTwoGO;

	// REWIRED
	private bool selectButton;
	private bool cancelButton;
	private bool toggleSkillButton;
	private bool dPadLeft;
	private bool dPadRight;


	private void Awake() {
		charClassContentScript = GetComponent<CharClassContent>();
	}


	private void Update() {
		GetInput();
		CheckForGamepad();
		CheckForInput();

		if (currentStatus == 2) {
			ClassNavigation();
		}
	}


	private void GetInput() {
		selectButton = ReInput.players.GetPlayer(charID).GetButtonDown("X");
		cancelButton = ReInput.players.GetPlayer(charID).GetButtonDown("Circle");
		toggleSkillButton = ReInput.players.GetPlayer(charID).GetButtonDown("Square");

		dPadLeft = ReInput.players.GetPlayer(charID).GetButtonDown("DPadLeft");
		dPadRight = ReInput.players.GetPlayer(charID).GetButtonDown("DPadRight");
	}


	private void CheckForGamepad() {
		if (GameUIHandler.connectedGamepads >= charID+1 && !isConnected) {
			currentStatus = 1;
			DisplayCurrentStatus();
		} else if (GameUIHandler.connectedGamepads < charID+1 && isConnected) {
			currentStatus = 0;
			DisplayCurrentStatus();
		}
	}


	private void CheckForInput() {
		if (currentStatus == 1 || currentStatus == 2) {
			if (selectButton) {
				currentStatus++;
				DisplayCurrentStatus();
			}
		}

		if (currentStatus == 2 || currentStatus == 3) {
			if (currentStatus == 2) {
				if (selectButton) {
					GameUIHandler.playerClasses[charID] = currentIndex;
				}
			}
			if (cancelButton) {
				if (currentStatus == 3) {
					GameUIHandler.playerClasses[charID] = -1;
				}
				currentStatus--;
				DisplayCurrentStatus();
			}
			if (toggleSkillButton) {
				toggleSkill = toggleSkill == 0 ? 1 : 0;
				DisplayClasses();
			}
		}
	}


	private void ClassNavigation() {
		if (dPadLeft) {
			if (currentIndex == 0) {
				currentIndex = charClassesArr.Length-1;
			} else {
				currentIndex--;
			}
			DisplayClasses();
		}
		if (dPadRight) {
			if (currentIndex == charClassesArr.Length-1) {
				currentIndex = 0;
			} else {
				currentIndex++;
			}
			DisplayClasses();
		}
	}


	private void DisplayClasses() {
		classText.text = charClassContentScript.classTexts[currentIndex];
		charHPText.text = charClassContentScript.charHPTexts[currentIndex];
		charDEFText.text = charClassContentScript.charDEFTexts[currentIndex];

		skillsText.text = charClassContentScript.skillsTexts[toggleSkill];

		if (toggleSkill == 0) {
			attackOneTitleText.text = charClassContentScript.attackOneTitleTexts[currentIndex];
			attackOneText.text = charClassContentScript.attackOneTexts[currentIndex];
			attackTwoTitleText.text = charClassContentScript.attackTwoTitleTexts[currentIndex];
			attackTwoText.text = charClassContentScript.attackTwoTexts[currentIndex];
			pageControlOneGO.color = new Color32(255, 255, 255, 255);
			pageControlTwoGO.color = new Color32(255, 255, 255, 75);
		} else if (toggleSkill == 1) {
			attackOneTitleText.text = charClassContentScript.skillOneTitleTexts[currentIndex];
			attackOneText.text = charClassContentScript.skillOneTexts[currentIndex];
			attackTwoTitleText.text = charClassContentScript.skillTwoTitleTexts[currentIndex];
			attackTwoText.text = charClassContentScript.skillTwoTexts[currentIndex];
			pageControlOneGO.color = new Color32(255, 255, 255, 75);
			pageControlTwoGO.color = new Color32(255, 255, 255, 255);
		}
	}


	private void DisplayCurrentStatus() {
		switch(currentStatus) {
			case 0:
				isConnected = false;
				gamepadIcon.color = new Color32(255, 255, 255, 50);
				pressToJoinGO.SetActive(false);
				charClassGO.SetActive(false);
				arrowNavGO.SetActive(false);
				readyGO.SetActive(false);
				break;
			case 1:
				isConnected = true;
				gamepadIcon.color = new Color32(255, 255, 255, 255);
				pressToJoinGO.SetActive(true);
				charClassGO.SetActive(false);
				arrowNavGO.SetActive(false);
				readyGO.SetActive(false);
				break;
			case 2:
				isConnected = true;
				gamepadIcon.color = new Color32(255, 255, 255, 0);
				pressToJoinGO.SetActive(false);
				charClassGO.SetActive(true);
				arrowNavGO.SetActive(true);
				readyGO.SetActive(false);

				DisplayClasses();
				break;
			case 3:
				isConnected = true;
				gamepadIcon.color = new Color32(255, 255, 255, 0);
				pressToJoinGO.SetActive(false);
				charClassGO.SetActive(true);
				arrowNavGO.SetActive(false);
				readyGO.SetActive(true);
				break;
		}
	}

}
