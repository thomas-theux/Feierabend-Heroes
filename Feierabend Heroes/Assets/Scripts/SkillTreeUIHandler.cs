using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class SkillTreeUIHandler : MonoBehaviour {

	private CharacterSheet characterSheetscript;
	private SkillHandler skillHandlerScript;

	public GameObject buttonParent;
	public Image cursorImage;
	public Image statsCursorImage;
	public Image statsCursorImageClone;
	public GameObject infoBox;
	public GameObject rageModeBox;

	public AudioSource cursorMoveSound;
	public AudioSource activateSkillSound;
	public AudioSource skillCompleteSound;
	public AudioSource skillLockedSound;

	public Text[] attackNames;

	public Text skillTitleText;
	public Text skillTextText;
	public Text skillPerkText;
	public Text skillCostsText;
	public Text[] rageModePerks;

	public Font plexBold;

	public Text charHP;
	public Text charDefense;
	public Text charMSPD;

	public Text charAttackOne;
	public Text charAttackTwo;

	public Text charDodge;
	public Text charCritHit;
	public Text charRespawn;
	public Text charOrbFinding;

	public Text currentOrbsText;

	private int charID;

	public List<Image> buttonArr;
	public Sprite[] activeButtonArr;
	public List<int> skillUpgradeCurrent;
	public Sprite skillLocked;
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
	public int currentOrbs = 0;

	private float minThreshold = 0.5f;
	private float maxThreshold = 0.5f;
	private bool axisXActive;
	private bool axisYActive;


	private void OnEnable() {
		DisplayOrbs();
	}

	private void OnDisable() {
		currentIndex = 15;
		cursorImage.transform.position = buttonArr[currentIndex].transform.position;
	}


	public void InitializeSkillUI() {
		characterSheetscript = this.gameObject.transform.parent.GetComponent<CharacterSheet>();
		skillHandlerScript = GetComponent<SkillHandler>();

		// Set stats in skill script
		characterSheetscript.currentOrbs = currentOrbs;

		// Set names of the attacks of the classes
		attackNames[0].text = characterSheetscript.attackNames[0];
		attackNames[1].text = characterSheetscript.attackNames[1];

		charID = this.gameObject.transform.parent.GetComponent<CharacterMovement>().playerID;

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
		if (this.gameObject.transform.parent.GetComponent<CharacterMovement>().skillBoardOn) {
			GetInput();
		}
	}


	private void GetInput() {

		// UI navigation with the analog sticks
		if (ReInput.players.GetPlayer(charID).GetAxis("LS Vertical") > maxThreshold && !axisYActive) {
			axisYActive = true;
			currentIndex = buttonArr[currentIndex].GetComponent<ButtonNav>().navUp.GetComponent<ButtonNav>().indexID;
			DisplayCursorImage();
		}
		if (ReInput.players.GetPlayer(charID).GetAxis("LS Vertical") < -maxThreshold && !axisYActive) {
			axisYActive = true;
			currentIndex = buttonArr[currentIndex].GetComponent<ButtonNav>().navDown.GetComponent<ButtonNav>().indexID;
			DisplayCursorImage();
		}
		if (ReInput.players.GetPlayer(charID).GetAxis("LS Horizontal") > maxThreshold && !axisXActive) {
			axisXActive = true;
			currentIndex = buttonArr[currentIndex].GetComponent<ButtonNav>().navRight.GetComponent<ButtonNav>().indexID;
			DisplayCursorImage();
		}
		if (ReInput.players.GetPlayer(charID).GetAxis("LS Horizontal") < -maxThreshold && !axisXActive) {
			axisXActive = true;
			currentIndex = buttonArr[currentIndex].GetComponent<ButtonNav>().navLeft.GetComponent<ButtonNav>().indexID;
			DisplayCursorImage();
		}

		if (ReInput.players.GetPlayer(charID).GetAxis("LS Vertical") <= minThreshold && ReInput.players.GetPlayer(charID).GetAxis("LS Vertical") >= -minThreshold) {
			axisYActive = false;
		}
		if (ReInput.players.GetPlayer(charID).GetAxis("LS Horizontal") <= minThreshold && ReInput.players.GetPlayer(charID).GetAxis("LS Horizontal") >= -minThreshold) {
			axisXActive = false;
		}

		// UI navigation with the D-Pad buttons
		if (ReInput.players.GetPlayer(charID).GetButtonDown("DPadUp")) {
			currentIndex = buttonArr[currentIndex].GetComponent<ButtonNav>().navUp.GetComponent<ButtonNav>().indexID;
			DisplayCursorImage();
		}
		if (ReInput.players.GetPlayer(charID).GetButtonDown("DPadDown")) {
			currentIndex = buttonArr[currentIndex].GetComponent<ButtonNav>().navDown.GetComponent<ButtonNav>().indexID;
			DisplayCursorImage();
		}
		if (ReInput.players.GetPlayer(charID).GetButtonDown("DPadLeft")) {
			currentIndex = buttonArr[currentIndex].GetComponent<ButtonNav>().navLeft.GetComponent<ButtonNav>().indexID;
			DisplayCursorImage();
		}
		if (ReInput.players.GetPlayer(charID).GetButtonDown("DPadRight")) {
			currentIndex = buttonArr[currentIndex].GetComponent<ButtonNav>().navRight.GetComponent<ButtonNav>().indexID;
			DisplayCursorImage();
		}

		if (ReInput.players.GetPlayer(charID).GetButtonDown("X")) {
			if (skillCosts[currentIndex] <= characterSheetscript.currentOrbs) {
				SkillActivationProcess();
			} else {
				Instantiate(skillLockedSound);
			}
		}
	}


	private void DisplayCursorImage() {
		// Display cursor at the right position
		cursorImage.transform.position = buttonArr[currentIndex].transform.position;
		Instantiate(cursorMoveSound);

		// Display skillboard titles, texts and perks
		infoBox.transform.GetChild(0).GetComponent<Text>().text = skillHandlerScript.skillTitles[currentIndex];
		infoBox.transform.GetChild(1).GetComponent<Text>().text = skillHandlerScript.skillTexts[currentIndex];
		infoBox.transform.GetChild(2).GetComponent<Text>().text = skillHandlerScript.skillPerks[currentIndex];

		// Show ORB costs for this skill
		if (skillUpgradeCurrent[currentIndex] < skillUpgradeMax[currentIndex] && skillUpgradeCurrent[currentIndex] > -2) {
			if (skillCosts[currentIndex] > 1) {
				skillCostsText.text = skillCosts[currentIndex] + " Orbs";
			} else {
				skillCostsText.text = skillCosts[currentIndex] + " Orb";
			}
		} else if (skillUpgradeCurrent[currentIndex] == skillUpgradeMax[currentIndex]) {
			skillCostsText.text = "✔ DONE";
		} else if (skillUpgradeCurrent[currentIndex] == -2) {
			skillCostsText.text = "✖ LOCKED";
		}
		
		// Show extra box for Rage Mode levels
		if (currentIndex == 30) {
			rageModeBox.SetActive(true);
			for (int i = 0; i < skillUpgradeCurrent[30]+1; i++) {
				rageModePerks[i].color = new Color32(255, 109, 1, 255);
				rageModePerks[i].font = plexBold;
			}
		} else {
			rageModeBox.SetActive(false);
		}

		// Highlight stat that will be improved with the selected skill
		switch (currentIndex) {
			default:
				statsCursorImage.transform.localPosition = new Vector3(statsCursorImage.transform.localPosition.x, 5000, statsCursorImage.transform.localPosition.z);
				statsCursorImageClone.gameObject.SetActive(false);
				break;
			// Health
			case 0:
				statsCursorImage.transform.localPosition = new Vector3(statsCursorImage.transform.localPosition.x, 153, statsCursorImage.transform.localPosition.z);
				statsCursorImageClone.gameObject.SetActive(false);
				break;
			// Defense
			case 2:
				statsCursorImage.transform.localPosition = new Vector3(statsCursorImage.transform.localPosition.x, 123, statsCursorImage.transform.localPosition.z);
				statsCursorImageClone.gameObject.SetActive(false);
				break;
			// Move Speed
			case 6:
				statsCursorImage.transform.localPosition = new Vector3(statsCursorImage.transform.localPosition.x, 93, statsCursorImage.transform.localPosition.z);
				statsCursorImageClone.gameObject.SetActive(false);
				break;
			// Damage && Attack Speed
			case 1:
			case 9:
				statsCursorImage.transform.localPosition = new Vector3(statsCursorImage.transform.localPosition.x, 11, statsCursorImage.transform.localPosition.z);
				statsCursorImageClone.gameObject.SetActive(true);
				break;
			// Critical Hit
			case 11:
			case 19:
			case 27:
				statsCursorImage.transform.localPosition = new Vector3(statsCursorImage.transform.localPosition.x, -99, statsCursorImage.transform.localPosition.z);
				statsCursorImageClone.gameObject.SetActive(false);
				break;
			// Dodge
			case 12:
			case 20:
			case 28:
				statsCursorImage.transform.localPosition = new Vector3(statsCursorImage.transform.localPosition.x, -129, statsCursorImage.transform.localPosition.z);
				statsCursorImageClone.gameObject.SetActive(false);
				break;
			// Respawn
			case 13:
			case 21:
			case 29:
				statsCursorImage.transform.localPosition = new Vector3(statsCursorImage.transform.localPosition.x, -159, statsCursorImage.transform.localPosition.z);
				statsCursorImageClone.gameObject.SetActive(false);
				break;
			// Orb Finding
			case 17:
			case 25:
			case 31:
				statsCursorImage.transform.localPosition = new Vector3(statsCursorImage.transform.localPosition.x, -189, statsCursorImage.transform.localPosition.z);
				statsCursorImageClone.gameObject.SetActive(false);
				break;
		}
	}


	private void SkillActivationProcess() {
		if (skillUpgradeCurrent[currentIndex] >= 0) {
			if (skillUpgradeCurrent[currentIndex] < skillUpgradeMax[currentIndex]) {

				// Increase the selected skill in the array
				skillUpgradeCurrent[currentIndex]++;

				if (skillUpgradeCurrent[currentIndex] == skillUpgradeMax[currentIndex]) {
					Instantiate(skillCompleteSound);
				} else {
					Instantiate(activateSkillSound);
				}

				PayOrbs();
				skillHandlerScript.ActivateSkill(skillUpgradeCurrent, currentIndex);
				DisplayOrbs();
				ActivateNext();
				DisplayActivations();
				CalculateNewCosts();
			} else {
				// print("Already activated!");
				Instantiate(skillLockedSound);
			}
		} else {
			// print("This skill is locked");
			Instantiate(skillLockedSound);
		}
	}


	private void PayOrbs() {
		// Subtract ORBS
		characterSheetscript.currentOrbs -= skillCosts[currentIndex];
		// Add one to stat "orbs spent"
		GameManager.orbsSpentStatsArr[characterSheetscript.charID]++;

		// DEV STUFF – Delete for production
		SettingsHolder.spentOrbs++;
		PlayerPrefs.SetInt("Spent Orbs", SettingsHolder.spentOrbs);
	}


	private void DisplayOrbs() {
		// Update ORBS count
		currentOrbsText.text = characterSheetscript.currentOrbs + "";

		// Update character stats
		charHP.text = characterSheetscript.currentHealth.ToString("F0") + "/" + characterSheetscript.maxHealth.ToString("F0");
		charDefense.text = characterSheetscript.charDefense + "";
		charMSPD.text = characterSheetscript.moveSpeed.ToString("F1");

		// charAttackOne.text = characterSheetscript.attackOneDmg.ToString("F0");
		// charAttackTwo.text = characterSheetscript.attackTwoDmg.ToString("F0");

		float attackOneDPS = (1.0f / characterSheetscript.delayAttackOne) * characterSheetscript.attackOneDmg;
		charAttackOne.text = attackOneDPS.ToString("F0");

		// If class is Novist then change DPS calculation
		float attackTwoDPS = 0;
		if (characterSheetscript.charClass == 1) {
			attackTwoDPS = characterSheetscript.attackTwoDmg * 100.0f;
		} else {
			attackTwoDPS = (1.0f / characterSheetscript.delayAttackTwo) * characterSheetscript.attackTwoDmg;
		}
		charAttackTwo.text = attackTwoDPS.ToString("F0");

		charDodge.text = characterSheetscript.dodgeChance + "%";
		charCritHit.text = characterSheetscript.critChance + "%";
		charRespawn.text = characterSheetscript.respawnChance + "%";
		charOrbFinding.text = characterSheetscript.doubleOrbChance + "%";
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
				buttonArr[currentIndex].transform.GetChild(2).GetComponent<Text>().text = skillUpgradeCurrent[currentIndex] + "/" + skillUpgradeMax[currentIndex];
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
				buttonArr[k].GetComponent<Image>().sprite = skillLocked;
				buttonArr[k].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
			}
			
			if (skillUpgradeCurrent[k] == 0) {
				buttonArr[k].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
				
				if (buttonArr[k].transform.Find("SkillLevel")) {
					buttonArr[k].transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
					buttonArr[k].transform.Find("SkillLevel").GetComponent<Text>().color = new Color32(35, 45, 55, 255);
					buttonArr[k].transform.Find("SkillLevel").GetComponent<Text>().text = skillUpgradeCurrent[k] + "/" + skillUpgradeMax[k];
				}
			}

			if (skillUpgradeCurrent[k] == 1) {
				if (buttonArr[k].transform.Find("SkillLevel")) {
					buttonArr[k].transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
				}
				buttonArr[currentIndex].GetComponent<Image>().sprite = activeButtonArr[currentIndex];
				buttonArr[k].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
			}
		}

		// Set the selected skill to "done"
		// if (skillUpgradeCurrent[currentIndex] == skillUpgradeMax[currentIndex]) {
		// 	if (buttonArr[currentIndex].transform.Find("SkillLevel")) {
		// 		buttonArr[currentIndex].transform.Find("SkillLevel").gameObject.SetActive(false);
		// 	}
		// 	buttonArr[currentIndex].transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
		// }
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
		// for (int m = 0; m < buttonArr.Count; m++) {
		// 	if (skillUpgradeCurrent[m] == skillUpgradeMax[m]) {
		// 		skillCosts[m] = 0;
		// 	}
		// }

		// Show ORB costs for this skill
		if (skillUpgradeCurrent[currentIndex] < skillUpgradeMax[currentIndex] && skillUpgradeCurrent[currentIndex] > -2) {
			if (skillCosts[currentIndex] > 1) {
				skillCostsText.text = skillCosts[currentIndex] + " Orbs";
			} else {
				skillCostsText.text = skillCosts[currentIndex] + " Orb";
			}
		} else if (skillUpgradeCurrent[currentIndex] == skillUpgradeMax[currentIndex]) {
			skillCostsText.text = "✔ DONE";
		} else if (skillUpgradeCurrent[currentIndex] == -2) {
			skillCostsText.text = "✖ LOCKED";
		}
	}

}
