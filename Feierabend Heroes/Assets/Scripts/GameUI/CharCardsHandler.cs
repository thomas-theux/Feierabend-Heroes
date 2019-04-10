using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Rewired;

public class CharCardsHandler : MonoBehaviour {

	public int charID = 0;
	private int currentStatus = 0;
	private bool isConnected = false;
	private bool playerReady = false;
	
	private int currentIndex = 0;
	private int[] charClassesArr = {0, 1};

	private float cursorBase = -10;
	private float cursorDistance = -46;

	public Image gamepadIcon;
	public GameObject pressToJoinGO;
	public GameObject charClassGO;
	public Image selectionCursor;
	// public GameObject arrowNavGO;
	// public GameObject readyGO;
	public GameObject readyStampGO;
	public GameObject oneColumn;
	public GameObject twoColumns;

	public AudioSource selectSound;
	public AudioSource navigateSound;
	public AudioSource cancelSound;
	public AudioSource toggleSound;
	public AudioSource randomizeNameSound;

	// private Color[] charColors = {
	// 	new Color32(23, 155, 194, 255),
	// 	new Color32(231, 86, 84, 255),
	// 	new Color32(35, 194, 112, 255),
	// 	new Color32(182, 194, 35, 255)
	// };

	private CharClassContent charClassContentScript;

	// public Text randomNameText;
	public TMP_Text randomNameText;
	// public TMP_Text classText;
	public TMP_Text[] classTexts;
	public TMP_Text charHPStat;
	public TMP_Text charDEFStat;
	public TMP_Text charMSPDStat;
	public TMP_Text charClassDesc;
	public Image classImage;

	// public Image attackOneImage;
	// public Text attackOneTitleText;
	// public Image attackTwoImage;
	// public Text attackTwoTitleText;
	// public Text skillTitle;
	// public Text passiveTitle;

	// REWIRED
	private bool selectButton;
	private bool cancelButton;
	private bool randomizeNameButton;
	private bool dPadUp;
	private bool dPadDown;


	private void Start() {
		charClassContentScript = GetComponent<CharClassContent>();
		RandomizeName();
		
		// randomNameText.color = charColors[charID];
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

		dPadUp = ReInput.players.GetPlayer(charID).GetButtonDown("DPadUp");
		dPadDown = ReInput.players.GetPlayer(charID).GetButtonDown("DPadDown");
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
					// classText.text = "READY!";
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
					// SettingsHolder.charNames[charID] = "";
				}

				currentStatus--;
				DisplayCurrentStatus();
			}
		}
	}


	private void ClassNavigation() {
		if (dPadUp) {
			if (currentIndex > 0) {
				Instantiate(navigateSound);
				currentIndex--;
				DisplayClasses();
			}
		}

		if (dPadDown) {
			if (currentIndex < charClassesArr.Length - 1) {
				Instantiate(navigateSound);
				currentIndex++;
				DisplayClasses();
			}
		}
	}


	private void DisplayClasses() {
		// Display cursor
		selectionCursor.transform.localPosition = new Vector2(
			selectionCursor.transform.localPosition.x,
			cursorBase + cursorDistance * currentIndex
		);

		// Color class texts depending on highlight
		for (int i = 0; i < classTexts.Length; i++) {
			if (i == currentIndex) {
				classTexts[currentIndex].color = Colors.keyPaper;
			} else {
				classTexts[i].color = Colors.blue20;
			}
		}

		// classText.text = CharClassContent.classTexts[currentIndex];
		charHPStat.text = CharClassContent.charHPStats[currentIndex] + "";
		charDEFStat.text = CharClassContent.charDEFStats[currentIndex] + "";
		charMSPDStat.text = CharClassContent.charMSPDStats[currentIndex] + "";
		charClassDesc.text = CharClassContent.classDescriptions[currentIndex] + "";

		// attackOneTitleText.text = CharClassContent.attackOneTitleTexts[currentIndex] + "";
		// attackTwoTitleText.text = CharClassContent.attackTwoTitleTexts[currentIndex] + "";
		// attackOneImage.sprite = charClassContentScript.attackOneImages[currentIndex];
		// attackTwoImage.sprite = charClassContentScript.attackTwoImages[currentIndex];
		// skillTitle.text = CharClassContent.skillTitles[currentIndex] + "";
		// passiveTitle.text = CharClassContent.passiveTitles[currentIndex] + "";
		classImage.sprite = charClassContentScript.classImages[currentIndex];
	}


	private void DisplayCurrentStatus() {
		switch(currentStatus) {
			case 0:
				isConnected = false;
				oneColumn.SetActive(true);
				twoColumns.SetActive(false);
				gamepadIcon.color = new Color32(0, 0, 0, 0);
				pressToJoinGO.SetActive(false);
				charClassGO.SetActive(false);
				readyStampGO.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
				break;
			case 1:
				isConnected = true;
				oneColumn.SetActive(true);
				twoColumns.SetActive(false);
				gamepadIcon.color = Colors.keyPlayers[charID];
				pressToJoinGO.SetActive(true);
				charClassGO.SetActive(false);
				readyStampGO.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
				break;
			case 2:
				isConnected = true;
				oneColumn.SetActive(false);
				twoColumns.SetActive(true);
				gamepadIcon.color = Colors.keyPlayers[charID];
				pressToJoinGO.SetActive(false);
				charClassGO.SetActive(true);
				readyStampGO.GetComponent<Image>().color = new Color32(0, 0, 0, 0);

				DisplayClasses();
				break;
			case 3:
				isConnected = true;
				oneColumn.SetActive(false);
				twoColumns.SetActive(true);
				gamepadIcon.color = Colors.keyPlayers[charID];
				pressToJoinGO.SetActive(false);
				charClassGO.SetActive(true);
				readyStampGO.GetComponent<Image>().color = Colors.keyPlayers[charID];
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
