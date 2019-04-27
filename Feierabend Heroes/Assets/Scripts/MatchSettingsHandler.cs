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
	public AudioSource navigateSound;

    public GameObject navigationParent;
    private List<GameObject> navigationElementsArr = new List<GameObject>();

    public List<TMP_Text> settingsTextsArr = new List<TMP_Text>();
    public TMP_Text modeAmountsTitle;

    private int currentIndex = 0;

    private int[] modeIndexes = {
        0,
        0,
        1,
        1,
        1,
        0
    };

    private string[] battleTypes = {
        "Free-for-all",
        "2 vs 2",
        "1 vs 3"
    };

    private string[] gameTypes = {
        "Rounds",
        "Kills",
        "Orbs"
    };

    private int[] modeAmounts = {
        5,
        10,
        20
    };

    private int[] startingOrbs = {
        0,
        30,
        60
    };

    private string[] orbSpawn = {
        "Few",
        "Normal",
        "Lots"
    };

    private string[] bountyTypes = {
        "Leader",
        "Democratic",
        "No Bounty"
    };

    private int[] orbSpawnMax = {
        10,
        40,
        120
    };

    private float overallSec = 150.0f;

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
        "A bounty is automatically placed on the leader.",
        "Vote democratically after every round.",
        "Turn off the bounty system."
    };


    private void Awake() {
        // Write all children game objects in one array
        for (int i = 0; i < navigationParent.transform.childCount; i++) {
            navigationElementsArr.Add(navigationParent.transform.GetChild(i).gameObject);
            settingsTextsArr.Add(navigationParent.transform.GetChild(i).transform.GetChild(1).GetComponent<TMP_Text>());
        }

        DisplayCursor();
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
        } else if (modeIndexes[currentIndex] == 2) {
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

        int doubledVal = 1;
        int calcVal = 0;

        for (int i = 0; i < orbSpawnMax[modeIndexes[4]]; i++) {
            calcVal += doubledVal;
            doubledVal *= 2;
        }

        // SettingsHolder.spawnDelayTime = overallSec / calcVal;
        float initialWait = overallSec / calcVal;

        for (int j = 0; j < orbSpawnMax[modeIndexes[4]]; j++) {
            GameManager.spawnWaits.Add(initialWait);
            initialWait *= 2.0f;
        }

        // Set bounty type
        SettingsHolder.bountyType = modeIndexes[5];

        // Check all settings
        // print(SettingsHolder.battleType);
        // print(SettingsHolder.gameType);
        // print(SettingsHolder.amountOfRounds);
        // print(SettingsHolder.startingOrbs);
        // print(SettingsHolder.orbSpawnMax);
        // print(SettingsHolder.bountyType);

        // Load character selection screen
        SceneManager.LoadScene("2 Character Selection");
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
            if (modeIndexes[currentIndex] < 2) {
                modeIndexes[currentIndex]++;

                DisplayCursor();
                DisplayTitles();
                DisplayTexts();
            }
        }

        if (ReInput.players.GetPlayer(0).GetButtonDown("X")) {
            // Set map to Hunted
            SettingsHolder.selectedMap = "4 Hunted";

            Instantiate(selectSound);

            SaveMatchSettings();
        }

        if (ReInput.players.GetPlayer(0).GetButtonDown("Triangle")) {
            // Set map to Aeras
            SettingsHolder.selectedMap = "3 Aeras";

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
