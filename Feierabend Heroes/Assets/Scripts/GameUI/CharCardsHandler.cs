using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class CharCardsHandler : MonoBehaviour {

	public int charID = 0;
	private int currentStatus = 0;
	private bool isConnected = false;
	private bool playerReady = false;
	
	private int currentIndex = 0;
	private int[] charClassesArr = {0, 1};

	public Image gamepadIcon;
	public GameObject pressToJoinGO;
	public GameObject charClassGO;
	public GameObject arrowNavGO;
	public GameObject readyGO;

	public AudioSource selectSound;
	public AudioSource navigateSound;
	public AudioSource cancelSound;
	public AudioSource toggleSound;
	public AudioSource randomizeNameSound;

	private Color[] charColors = {
		new Color32(23, 155, 194, 255),
		new Color32(194, 66, 64, 255),
		new Color32(35, 194, 112, 255),
		new Color32(182, 194, 35, 255)
	};

	private CharClassContent charClassContentScript;

	public Text randomNameText;
	public Text classText;
	public Text charHPText;
	public Text charDEFText;
	public Text charMSPDText;
	public Image classImage;

	public Image attackOneImage;
	public Text attackOneTitleText;
	public Image attackTwoImage;
	public Text attackTwoTitleText;
	public Text skillTitle;
	public Text passiveTitle;

	// REWIRED
	private bool selectButton;
	private bool cancelButton;
	private bool randomizeNameButton;
	private bool dPadLeft;
	private bool dPadRight;


	private void Awake() {
		charClassContentScript = GetComponent<CharClassContent>();
		RandomizeName();
	}


	private void Start() {
		randomNameText.color = charColors[charID];
		DisplayCurrentStatus();
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
		randomizeNameButton = ReInput.players.GetPlayer(charID).GetButtonDown("Square");

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
				Instantiate(selectSound);
				currentStatus++;
				DisplayCurrentStatus();
			}
		}

		if (currentStatus == 2 || currentStatus == 3) {
			if (currentStatus == 2) {
				if (randomizeNameButton) {
					Instantiate(randomizeNameSound);
					RandomizeName();
				}
			}
			
			if (currentStatus == 3) {
				if (selectButton && !playerReady) {
					playerReady = true;
					Instantiate(selectSound);
					classText.text = "READY!";
					SettingsHolder.playerClasses[charID] = currentIndex;
					SettingsHolder.registeredPlayers++;
				}
				if (randomizeNameButton) {
					Instantiate(randomizeNameSound);
					RandomizeName();
				}
			}
			if (cancelButton) {
				Instantiate(cancelSound);
				if (currentStatus == 3) {
					playerReady = false;
					SettingsHolder.playerClasses[charID] = -1;
					SettingsHolder.registeredPlayers--;
				}
				if (currentStatus == 2) {
					SettingsHolder.charNames[charID] = "";
				}

				currentStatus--;
				DisplayCurrentStatus();
			}
		}
	}


	private void ClassNavigation() {
		if (dPadLeft) {
			Instantiate(navigateSound);
			if (currentIndex == 0) {
				currentIndex = charClassesArr.Length-1;
			} else {
				currentIndex--;
			}
			DisplayClasses();
		}

		if (dPadRight) {
			Instantiate(navigateSound);
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
		charMSPDText.text = charClassContentScript.charMSPDTexts[currentIndex];

		attackOneTitleText.text = charClassContentScript.attackOneTitleTexts[currentIndex];
		attackTwoTitleText.text = charClassContentScript.attackTwoTitleTexts[currentIndex];
		attackOneImage.sprite = charClassContentScript.attackOneImages[currentIndex];
		attackTwoImage.sprite = charClassContentScript.attackTwoImages[currentIndex];
		skillTitle.text = charClassContentScript.skillTitles[currentIndex];
		passiveTitle.text = charClassContentScript.passiveTitles[currentIndex];
		classImage.sprite = charClassContentScript.classImages[currentIndex];
	}


	private void DisplayCurrentStatus() {
		switch(currentStatus) {
			case 0:
				isConnected = false;
				gamepadIcon.color = new Color32(255, 255, 255, 50);
				pressToJoinGO.SetActive(false);
				charClassGO.SetActive(false);
				arrowNavGO.SetActive(false);
				// readyGO.SetActive(false);
				classText.enabled = false;
				readyGO.GetComponent<Image>().color = new Color32(235, 245, 255, 0);
				break;
			case 1:
				isConnected = true;
				gamepadIcon.color = new Color32(255, 255, 255, 255);
				pressToJoinGO.SetActive(true);
				charClassGO.SetActive(false);
				arrowNavGO.SetActive(false);
				// readyGO.SetActive(false);
				classText.enabled = false;
				readyGO.GetComponent<Image>().color = new Color32(235, 245, 255, 0);
				break;
			case 2:
				isConnected = true;
				gamepadIcon.color = new Color32(255, 255, 255, 0);
				pressToJoinGO.SetActive(false);
				charClassGO.SetActive(true);
				arrowNavGO.SetActive(true);
				// readyGO.SetActive(true);
				classText.enabled = true;
				readyGO.GetComponent<Image>().color = new Color32(235, 245, 255, 255);

				DisplayClasses();
				break;
			case 3:
				isConnected = true;
				gamepadIcon.color = new Color32(255, 255, 255, 0);
				pressToJoinGO.SetActive(false);
				charClassGO.SetActive(true);
				arrowNavGO.SetActive(false);
				// readyGO.SetActive(true);
				classText.enabled = true;
				readyGO.GetComponent<Image>().color = new Color32(255, 109, 1, 255);
				break;
		}
	}


	private void RandomizeName() {
		int titleChance = Random.Range(0, 100);
		int adjectiveChance = Random.Range(0, 100);

		if (titleChance >= 50 && adjectiveChance >= 50) {
			titleChance = Random.Range(0, 100);
			adjectiveChance = Random.Range(0, 100);
		}

		string addTitle = "";
		string addAdjective = "";
		string addName = "";

		// 50% chance of adding a title to the name
		if (titleChance < 50) {
			int rndTitle = Random.Range(0, CharClassContent.titleTexts.Length);
			addTitle = CharClassContent.titleTexts[rndTitle] + " ";
		}

		// 50% chance of adding a adjective to the name
		if (adjectiveChance < 50) {
			int rndPrefix = Random.Range(0, CharClassContent.adjectiveTexts.Length);
			addAdjective = CharClassContent.adjectiveTexts[rndPrefix] + " ";
		}

		int rndName = Random.Range(0, CharClassContent.nameTexts.Length);
		addName = CharClassContent.nameTexts[rndName];

		// Display randomized name
		randomNameText.text = addTitle + addAdjective + addName;

		// Write name in SettingsHolder array
		SettingsHolder.charNames[charID] = randomNameText.text;
	}

}
