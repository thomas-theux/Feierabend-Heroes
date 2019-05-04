using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Rewired;
using UnityEngine.SceneManagement;

public class MatchSettingsHandler : MonoBehaviour {

    public Image settingsCursor;
    public Image arrowNavLeft;
    public Image arrowNavRight;

    public TMP_Text modeTitle;
    public TMP_Text modeDescription;

    public AudioSource selectSound;
    public AudioSource cancelSound;
	public AudioSource navigateSound;

	private float minThreshold = 0.5f;
	private float maxThreshold = 0.5f;
	private bool axisXActive;
	private bool axisYActive;

    public GameObject navigationParent;
    private List<GameObject> navigationElementsArr = new List<GameObject>();

    public List<TMP_Text> settingsTextsArr = new List<TMP_Text>();
    public TMP_Text modeAmountsTitle;

    private int currentIndex = 0;

    // private int[] modeIndexes = {
    //     0,  // Battle Type
    //     0,  // Game Type
    //     1,  // Amounts
    //     2,  // Starting Orbs
    //     1,  // Spawning Orbs
    //     0   // Bounty
    // };

    // This array is only for DEF STUFF
    private int[] modeIndexes = {
        0,  // Battle Type
        0,  // Game Type
        0,  // Amounts
        3,  // Starting Orbs
        0,  // Spawning Orbs
        1   // Bounty
    };

    private List<int> maxIndexes = new List<int>();

    private string[] battleTypes = {
        "Free-for-all",
        // "2 vs 2",
        // "1 vs 3"
    };

    private string[] gameTypes = {
        "Rounds",
        // "Kills",
        // "Orbs"
    };

    private int[] modeAmounts = {
        1,
        5,
        10,
        20
    };

    private int[] startingOrbs = {
        0,
        30,
        60,
        3360
    };

    private string[] orbSpawn = {
        "None",
        "Few",
        "Normal",
        "More",
        "Lots"
    };
    private int[] orbSpawnMax = {
        0,
        5,
        15,
        30,
        90
    };

    private string[] bountyTypes = {
        "Leader",
        // "Democratic",
        "No Bounty"
    };

    // private float overallSec = 150.0f;

    // DESCRIPTIONS FOR THE VARIOUS MODES
    private string[] battleTypeDesc = {
        "Everyone is at war with each other!",
        "Build teams and fight each other!",
        "3 Davids fight 1 Goliath. Easy right?"
    };

    private string[] gameTypeDesc = {
        "Play a set amount of rounds.",
        "It's all about kills to win the match.",
        "Gather Orbs as fast as you can to win the match."
    };

    private string[] bountyTypeDesc = {
        "Killing the hero in first place is being rewarded.",
        // "A bounty is automatically placed on the leader.",
        // "Vote democratically after every round.",
        "Turn off the bounty system."
    };


    private void Awake() {
        // Write all children game objects in one array
        for (int i = 0; i < navigationParent.transform.childCount; i++) {
            navigationElementsArr.Add(navigationParent.transform.GetChild(i).gameObject);
            settingsTextsArr.Add(navigationParent.transform.GetChild(i).transform.GetChild(1).GetComponent<TMP_Text>());
        }

        // Populate the max indexes array
        maxIndexes.Add(battleTypes.Length);
        maxIndexes.Add(gameTypes.Length);
        maxIndexes.Add(modeAmounts.Length);
        maxIndexes.Add(startingOrbs.Length);
        maxIndexes.Add(orbSpawn.Length);
        maxIndexes.Add(bountyTypes.Length);

        // DisplayCursor();
        DisplayTitles();
        InitialTexts();
        // DisplayTexts();
    }


    private void DisplayCursor() {
        Instantiate(navigateSound);

        // Position the cursor
        settingsCursor.transform.localPosition = new Vector2(
            navigationElementsArr[currentIndex].transform.localPosition.x - 20,
            navigationElementsArr[currentIndex].transform.localPosition.y
        );

        // Color the navigation arrows
        if (modeIndexes[currentIndex] == 0) {
            arrowNavLeft.color = new Color32(Colors.keyPaper.r, Colors.keyPaper.g, Colors.keyPaper.b, 128);
            arrowNavRight.color = Colors.keyPaper;
        } else if (modeIndexes[currentIndex] == maxIndexes[currentIndex] - 1) {
            arrowNavLeft.color = Colors.keyPaper;
            arrowNavRight.color = new Color32(Colors.keyPaper.r, Colors.keyPaper.g, Colors.keyPaper.b, 128);
        } else {
            arrowNavLeft.color = Colors.keyPaper;
            arrowNavRight.color = Colors.keyPaper;
        }
    }


    private void DisplayTitles() {
        for (int i = 0; i < navigationElementsArr.Count; i++) {
            if (i == currentIndex) {
                navigationElementsArr[currentIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Colors.keyPaper;
                navigationElementsArr[currentIndex].transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Colors.keyPaper;
            } else {
                navigationElementsArr[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Colors.blue20;
                navigationElementsArr[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Colors.blue20;
            }
        }
    }


    private void DisplayTexts() {
        switch (currentIndex) {
            case 0:
                settingsTextsArr[currentIndex].text = battleTypes[modeIndexes[currentIndex]];
                modeTitle.text = battleTypes[modeIndexes[currentIndex]];
                modeDescription.text = battleTypeDesc[modeIndexes[currentIndex]];
                break;
            case 1:
                settingsTextsArr[currentIndex].text = gameTypes[modeIndexes[currentIndex]];
                modeAmountsTitle.text = settingsTextsArr[currentIndex].text;
                modeTitle.text = gameTypes[modeIndexes[currentIndex]];
                modeDescription.text = gameTypeDesc[modeIndexes[currentIndex]];
                break;
            case 2:
                settingsTextsArr[currentIndex].text = modeAmounts[modeIndexes[currentIndex]] + "";
                modeTitle.text = modeAmounts[modeIndexes[currentIndex]] + " " + settingsTextsArr[1].text;
                if (modeIndexes[currentIndex - 1] == 0) {
                    modeDescription.text = "How many rounds will you be playing?";
                } else {
                    modeDescription.text = "The amount of " + settingsTextsArr[1].text + " needed to win the match.";
                }
                break;
            case 3:
                settingsTextsArr[currentIndex].text = startingOrbs[modeIndexes[currentIndex]] + "";
                modeTitle.text = startingOrbs[modeIndexes[currentIndex]] + " Starting Orbs";
                modeDescription.text = "The amount of Orbs the heroes start with.";
                break;
            case 4:
                settingsTextsArr[currentIndex].text = orbSpawn[modeIndexes[currentIndex]];
                modeTitle.text = "Spawn " + orbSpawn[modeIndexes[currentIndex]];
                modeDescription.text = "The amount of Orbs that will be spawned per round.";
                break;
            case 5:
                settingsTextsArr[currentIndex].text = bountyTypes[modeIndexes[currentIndex]];
                modeTitle.text = bountyTypes[modeIndexes[currentIndex]];
                modeDescription.text = bountyTypeDesc[modeIndexes[currentIndex]];
                break;
        }

        // Display the mode title and description
        
    }


    private void SaveMatchSettings() {
        // Set battle type
        SettingsHolder.battleType = modeIndexes[0];

        // Set game type
        SettingsHolder.gameType = modeIndexes[1];

        // Set amount of rounds/kills/orbs
        SettingsHolder.amountOfRounds = modeAmounts[modeIndexes[2]];

        // Set starting orbs
        SettingsHolder.startingOrbs = startingOrbs[modeIndexes[3]];

        // Set orb spawn
        SettingsHolder.orbSpawnMax = orbSpawnMax[modeIndexes[4]];

        // int doubledVal = 1;
        // int calcVal = 0;

        // for (int i = 0; i < orbSpawnMax[modeIndexes[4]]; i++) {
        //     calcVal += doubledVal;
        //     doubledVal *= 2;
        // }

        // // SettingsHolder.spawnDelayTime = overallSec / calcVal;
        // float initialWait = overallSec / calcVal;

        // for (int j = 0; j < orbSpawnMax[modeIndexes[4]]; j++) {
        //     GameManager.spawnWaits.Add(initialWait);
        //     initialWait *= 2.0f;
        // }

        // Set bounty type
        SettingsHolder.bountyType = modeIndexes[5];

        // Check all settings
        // print(SettingsHolder.battleType);
        // print(SettingsHolder.gameType);
        // print(SettingsHolder.amountOfRounds);
        // print(SettingsHolder.startingOrbs);
        // print(SettingsHolder.orbSpawnMax);
        // print(SettingsHolder.bountyType);

        // Load map selection screen
        SceneManager.LoadScene("2 Map Selection");
    }


    private void Update() {
        GetInput();
    }


    private void GetInput() {
        // UI navigation with the D-Pad buttons
        if (ReInput.players.GetPlayer(0).GetButtonDown("DPadUp")) {
            if (currentIndex > 0) {
                currentIndex--;

                DisplayCursor();
                DisplayTitles();
                DisplayTexts();
            }
        }
        if (ReInput.players.GetPlayer(0).GetButtonDown("DPadDown")) {
            if (currentIndex < modeIndexes.Length - 1) {
                currentIndex++;

                DisplayCursor();
                DisplayTitles();
                DisplayTexts();
            }
        }
        if (ReInput.players.GetPlayer(0).GetButtonDown("DPadLeft")) {
            if (modeIndexes[currentIndex] > 0) {
                modeIndexes[currentIndex]--;

                DisplayCursor();
                DisplayTitles();
                DisplayTexts();
            }
        }
        if (ReInput.players.GetPlayer(0).GetButtonDown("DPadRight")) {
            if (modeIndexes[currentIndex] < maxIndexes[currentIndex] - 1) {
                modeIndexes[currentIndex]++;

                DisplayCursor();
                DisplayTitles();
                DisplayTexts();
            }
        }

        // UI navigation with the analog sticks
        // LEFT
        if (ReInput.players.GetPlayer(0).GetAxis("LS Horizontal") < -maxThreshold && !axisXActive) {
            axisXActive = true;

            if (modeIndexes[currentIndex] > 0) {
                modeIndexes[currentIndex]--;

                DisplayCursor();
                DisplayTitles();
                DisplayTexts();
            }
        }
        // RIGHT
        if (ReInput.players.GetPlayer(0).GetAxis("LS Horizontal") > maxThreshold && !axisXActive) {
            axisXActive = true;

            if (modeIndexes[currentIndex] < maxIndexes[currentIndex] - 1) {
                modeIndexes[currentIndex]++;

                DisplayCursor();
                DisplayTitles();
                DisplayTexts();
            }
        }
        // UP
        if (ReInput.players.GetPlayer(0).GetAxis("LS Vertical") > maxThreshold && !axisYActive) {
            axisYActive = true;

            if (currentIndex > 0) {
                currentIndex--;

                DisplayCursor();
                DisplayTitles();
                DisplayTexts();
            }
        }
        // DOWN
        if (ReInput.players.GetPlayer(0).GetAxis("LS Vertical") < -maxThreshold && !axisYActive) {
            axisYActive = true;

            if (currentIndex < modeIndexes.Length - 1) {
                currentIndex++;

                DisplayCursor();
                DisplayTitles();
                DisplayTexts();
            }
        }

        if (ReInput.players.GetPlayer(0).GetAxis("LS Horizontal") <= minThreshold && ReInput.players.GetPlayer(0).GetAxis("LS Horizontal") >= -minThreshold) {
            axisXActive = false;
        }
        if (ReInput.players.GetPlayer(0).GetAxis("LS Vertical") <= minThreshold && ReInput.players.GetPlayer(0).GetAxis("LS Vertical") >= -minThreshold) {
            axisYActive = false;
        }

        if (ReInput.players.GetPlayer(0).GetButtonDown("X")) {
            // Set map to Aeras
            // SettingsHolder.selectedMap = "4 Aeras";

            Instantiate(selectSound);

            SaveMatchSettings();
        }

        if (ReInput.players.GetPlayer(0).GetButtonDown("Circle")) {
            Instantiate(cancelSound);
            SceneManager.LoadScene("0 Main Screen");
        }

        if (ReInput.players.GetPlayer(0).GetButtonDown("Triangle")) {
            // Set map to Hunted
            // SettingsHolder.selectedMap = "5 Hunted";

            Instantiate(selectSound);

            SaveMatchSettings();
        }
    }


    private void InitialTexts() {
        for (int i = 0; i < modeIndexes.Length; i++) {
            switch (i) {
                case 0:
                    settingsTextsArr[i].text = battleTypes[modeIndexes[i]];
                    break;
                case 1:
                    settingsTextsArr[i].text = gameTypes[modeIndexes[i]];
                    modeAmountsTitle.text = settingsTextsArr[i].text;
                    break;
                case 2:
                    settingsTextsArr[i].text = modeAmounts[modeIndexes[i]] + "";
                    break;
                case 3:
                    settingsTextsArr[i].text = startingOrbs[modeIndexes[i]] + "";
                    break;
                case 4:
                    settingsTextsArr[i].text = orbSpawn[modeIndexes[i]];
                    break;
                case 5:
                    settingsTextsArr[i].text = bountyTypes[modeIndexes[i]];
                    break;
            }
        }
    }

}
