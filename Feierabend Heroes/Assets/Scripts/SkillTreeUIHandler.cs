using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class SkillTreeUIHandler : MonoBehaviour {

	public GameObject buttonParent;
	public Image cursorImage;
	public Text currentOrbsText;
	public Text orbCostsText;

	public List<Image> buttonArr;
	public List<int> skillUpgradeCurrent;
	private List<int> skillUpgradeMax = new List<int>(new int[] {
		5, 5, 5, 1, 1, 1, 5, 1,
		5, 5, 1,
		1, 1, 1, 1, 1, 1, 1, 1,
		5, 4, 4, 5, 1, 5, 3, 4,
		1, 1, 1, 4, 1, 1
	});
	private List<int> skillCosts = new List<int>(new int[] {
		1, 1, 1, 2, 2, 2, 1, 2,
		2, 1, 4,
		1, 1, 2, 3, 0, 3, 2, 1,
		2, 1, 2, 3, 1, 3, 2, 2,
		5, 5, 5, 1, 5, 5
	});

	private int currentIndex = 15;
	private int currentOrbs = 200;


	private void Awake() {
		// Save all skill buttons in an array
		for (int i = 0; i < buttonParent.transform.childCount; i++) {
			buttonArr.Add(buttonParent.transform.GetChild(i).GetComponent<Image>());
			skillUpgradeCurrent.Add(-1);
		}

		// Initially enable skills that can be learned
		for (int i = 0; i < 8; i++) {
			skillUpgradeCurrent[i] = 0;
		}
		skillUpgradeCurrent[9] = 0;
		for (int j = 11; j < 19; j++) {
			skillUpgradeCurrent[j] = 0;
		}
		skillUpgradeCurrent[23] = 0;
		
		// This "skill" is already activated – because it's not really a skill but the class the player chose
		skillUpgradeCurrent[currentIndex] = 1;

		DisplayOrbs();
		DisplayActivations();
	}


	private void Update() {
		GetInput();

		DisplayCursorImage();
	}


	private void GetInput() {
		if (ReInput.players.GetPlayer(0).GetButtonDown("Up")) {
			currentIndex = buttonArr[currentIndex].GetComponent<ButtonNav>().navUp.GetComponent<ButtonNav>().indexID;
		}

		if (ReInput.players.GetPlayer(0).GetButtonDown("Down")) {
			currentIndex = buttonArr[currentIndex].GetComponent<ButtonNav>().navDown.GetComponent<ButtonNav>().indexID;
		}

		if (ReInput.players.GetPlayer(0).GetButtonDown("Left")) {
			currentIndex = buttonArr[currentIndex].GetComponent<ButtonNav>().navLeft.GetComponent<ButtonNav>().indexID;
		}

		if (ReInput.players.GetPlayer(0).GetButtonDown("Right")) {
			currentIndex = buttonArr[currentIndex].GetComponent<ButtonNav>().navRight.GetComponent<ButtonNav>().indexID;
		}

		if (ReInput.players.GetPlayer(0).GetButtonDown("X")) {
			if (skillCosts[currentIndex] <= currentOrbs) {
				SkillActivationProcess();
			} else {
				print("Not enough ORBS");
			}
		}
	}


	private void DisplayCursorImage() {
		// Display cursor at the right position
		cursorImage.transform.position = buttonArr[currentIndex].transform.position;

		// Show ORB costs for this skill
		orbCostsText.text = skillCosts[currentIndex] + "";
	}


	private void SkillActivationProcess() {
		if (skillUpgradeCurrent[currentIndex] >= 0) {
			if (skillUpgradeCurrent[currentIndex] < skillUpgradeMax[currentIndex]) {

				// Increase the selected skill in the array
				skillUpgradeCurrent[currentIndex]++;

				PayOrbs();
				GetComponent<SkillHandler>().ActivateSkill(skillUpgradeCurrent, currentIndex);
				DisplayOrbs();
				ActivateNext();
				DisplayActivations();
				CalculateNewCosts();
			} else {
				print("Already activated!");
			}
		} else {
			print("This skill is locked");
		}
	}


	private void PayOrbs() {
		// Subtract ORBS
		currentOrbs -= skillCosts[currentIndex];
	}


	private void DisplayOrbs() {
		// Update ORBS count
		currentOrbsText.text = currentOrbs + "";
	}


	private void ActivateNext() {
		// Activate the next button in line if it's predecessor is enbled
		if (skillUpgradeCurrent[3] == skillUpgradeMax[3] || skillUpgradeCurrent[4] == skillUpgradeMax[4] || skillUpgradeCurrent[5] == skillUpgradeMax[5]) {
			if (skillUpgradeCurrent[8] < 0) {
				skillUpgradeCurrent[8] = 0;
			}
		}

		if (skillUpgradeCurrent[7] == skillUpgradeMax[7] && skillUpgradeCurrent[10] < 0) {
			skillUpgradeCurrent[10] = 0;
		}

		for (int i = 11; i < 14; i++) {
			if (skillUpgradeCurrent[i] == skillUpgradeMax[i] && skillUpgradeCurrent[i+8] < 0) {
				skillUpgradeCurrent[i+8] = 0;
			}
		}

		if (skillUpgradeCurrent[14] == skillUpgradeMax[14] && skillUpgradeCurrent[22] < 0) {
			skillUpgradeCurrent[22] = 0;
		}

		if (skillUpgradeCurrent[16] == skillUpgradeMax[16] && skillUpgradeCurrent[24] < 0) {
			skillUpgradeCurrent[24] = 0;
		}

		for (int j = 17; j < 19; j++) {
			if (skillUpgradeCurrent[j] == skillUpgradeMax[j] && skillUpgradeCurrent[j+8] < 0) {
				skillUpgradeCurrent[j+8] = 0;
			}
		}

		if (skillUpgradeCurrent[23] == skillUpgradeMax[23] && skillUpgradeCurrent[30] < 0) {
			skillUpgradeCurrent[30] = 0;
		}

		// Activate the next button in line if it's predecessor is complete
		for (int k = 19; k < 22; k++) {
			if (skillUpgradeCurrent[k] == skillUpgradeMax[k] && skillUpgradeCurrent[k+8] < 0) {
				skillUpgradeCurrent[k+8] = 0;
			}
		}

		for (int l = 25; l < 27; l++) {
			if (skillUpgradeCurrent[l] == skillUpgradeMax[l] && skillUpgradeCurrent[l+6] < 0) {
				skillUpgradeCurrent[l+6] = 0;
			}
		}
	}


	private void DisplayActivations() {
		// Show the current level of activation
		switch(currentIndex) {
			case 0:
			case 1:
			case 2:
			case 6:
			case 8:
			case 9:
			case 19:
			case 20:
			case 21:
			case 22:
			case 24:
			case 25:
			case 26:
			case 30:
				buttonArr[currentIndex].transform.GetChild(1).GetComponent<Text>().text = skillUpgradeCurrent[currentIndex] + "/" + skillUpgradeMax[currentIndex];
				break;
			
			// Lock the other skills when activating these
			case 3:
				skillUpgradeCurrent[4] = -2;
				skillUpgradeCurrent[5] = -2;
				break;

			case 4:
				skillUpgradeCurrent[3] = -2;
				skillUpgradeCurrent[5] = -2;
				break;

			case 5:
				skillUpgradeCurrent[3] = -2;
				skillUpgradeCurrent[4] = -2;
				break;

			case 14:
				skillUpgradeCurrent[16] = -2;
				skillUpgradeCurrent[24] = -2;
				break;

			case 16:
				skillUpgradeCurrent[14] = -2;
				skillUpgradeCurrent[22] = -2;
				break;
		}

		// Enable all skill buttons that have a 0 – aka make it non-transparent
		for (int k = 0; k < buttonArr.Count; k++) {
			if (skillUpgradeCurrent[k] == -2) {
				buttonArr[k].GetComponent<Image>().color = new Color32(255, 255, 225, 30);
			}
			
			if (skillUpgradeCurrent[k] == 0) {
				buttonArr[k].GetComponent<Image>().color = new Color32(255, 255, 225, 255);
			}

			if (skillUpgradeCurrent[k] == 1) {
				buttonArr[k].GetComponent<Image>().color = new Color32(255, 255, 225, 255);
			}
		}

		// Set the selected skill to "done"
		if (skillUpgradeCurrent[currentIndex] == skillUpgradeMax[currentIndex]) {
			buttonArr[currentIndex].transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 0, 0, 255);
		}
	}


	private void CalculateNewCosts() {
		// Skill 00 / 01 / 02 – HP / Damage / Defense
		for (int i = 0; i < 3; i++) {
			if (skillUpgradeCurrent[i] == 2) {
				skillCosts[i] = 2;
			}
			if (skillUpgradeCurrent[i] == 4) {
				skillCosts[i] = 3;
			}
		}

		// Skill 03 – Time Upgrade – NOT NEEDED
		// Skill 04 – Chaos Upgrade – NOT NEEDED
		// Skill 05 – Venom Upgrade – NOT NEEDED

		// Skill 06 – Move Speed
		if (skillUpgradeCurrent[6] == 2) {
			skillCosts[6] = 2;
		}
		if (skillUpgradeCurrent[6] == 4) {
			skillCosts[6] = 3;
		}

		// Skill 07 – Single Jump – NOT NEEDED

		// Skill 08 – Weapon Upgrades
		if (skillUpgradeCurrent[8] == 1) {
			skillCosts[8] = 3;
		}
		if (skillUpgradeCurrent[8] == 3) {
			skillCosts[8] = 4;
		}
		if (skillUpgradeCurrent[8] == 4) {
			skillCosts[8] = 5;
		}

		// Skill 09 – Attack Speed
		if (skillUpgradeCurrent[9] == 2) {
			skillCosts[9] = 2;
		}
		if (skillUpgradeCurrent[9] == 4) {
			skillCosts[9] = 3;
		}

		// Skill 10 – Double Jump – NOT NEEDED
		// Skill 11 – Enable Crit – NOT NEEDED
		// Skill 12 – Enable Dodge – NOT NEEDED
		// Skill 13 – Enable Respawn – NOT NEEDED
		// Skill 14 – Skill One – NOT NEEDED
		// Skill 15 – Character Class – NOT NEEDED
		// Skill 16 – Skill Two – NOT NEEDED
		// Skill 17 – Enable Orb Finding – NOT NEEDED
		// Skill 18 – Enable Apple Finding – NOT NEEDED

		// Skill 19 – Improve Crit
		if (skillUpgradeCurrent[19] == 2) {
			skillCosts[19] = 3;
		}
		if (skillUpgradeCurrent[19] == 4) {
			skillCosts[19] = 4;
		}

		// Skill 20 – Improve Dodge
		for (int j = 1; j < 4; j++) {
			if (skillUpgradeCurrent[20] == j) {
				skillCosts[20] = j+1;
			}
		}

		// Skill 21 – Improve Respawn
		if (skillUpgradeCurrent[21] == 1) {
			skillCosts[21] = 3;
		}
		if (skillUpgradeCurrent[21] == 3) {
			skillCosts[21] = 4;
		}

		// Skill 22 – Improve Skill One
		if (skillUpgradeCurrent[22] == 2) {
			skillCosts[22] = 4;
		}
		if (skillUpgradeCurrent[22] == 4) {
			skillCosts[22] = 5;
		}

		// Skill 23 – Enable Rage Mode – NOT NEEDED

		// Skill 24 – Improve Skill Two
		if (skillUpgradeCurrent[24] == 2) {
			skillCosts[24] = 4;
		}
		if (skillUpgradeCurrent[24] == 4) {
			skillCosts[24] = 5;
		}

		// Skill 25 – Improve Orb Finding
		for (int k = 1; k < 3; k++) {
			if (skillUpgradeCurrent[25] == k) {
				skillCosts[25] = k+2;
			}
		}

		// Skill 26 – Improve Apple Finding
		if (skillUpgradeCurrent[26] == 2) {
			skillCosts[26] = 3;
		}

		// Skill 27 – Perfect Crit – NOT NEEDED
		// Skill 28 – Perfect Dodge – NOT NEEDED
		// Skill 29 – Perfect Respawn – NOT NEEDED

		// Skill 30 – Improve Rage Mode – NOT NEEDED
		for (int l = 1; l < 4; l++) {
			if (skillUpgradeCurrent[30] == l) {
				skillCosts[30] = l+1;
			}
		}

		// Skill 31 – Perfect Orb Finding – NOT NEEDED
		// Skill 32 – Perfect Apple Finding – NOT NEEDED

		// Set all completed skills to 0 costs
		for (int m = 0; m < buttonArr.Count; m++) {
			if (skillUpgradeCurrent[m] == skillUpgradeMax[m]) {
				skillCosts[m] = 0;
			}
		}
	}

}
