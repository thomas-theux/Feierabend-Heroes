using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Rewired;

public class SkillBoardHandler : MonoBehaviour {

    private CharacterSheet characterSheetScript;
    private SkillsHandler skillsHandlerScript;

    public GameObject tierTwoImage;
    public GameObject tierThreeImage;
    public TMP_Text currentOrbCount;
    public Text orbsNeededTierTwo;
    public Text orbsNeededTierThree;

    public Image gamepadIcon;
    public TMP_Text heroName;

    private Color32 opaqueSkills = new Color32(
        Colors.blue20.r,
        Colors.blue20.g,
        Colors.blue20.b,
        128
    );
    private Color32 opaquePapers = new Color32(
        Colors.keyPaper.r,
        Colors.keyPaper.g,
        Colors.keyPaper.b,
        128
    );

    // Icons from IcoFont for when unlocking skills in the list
    private string[] statIconsArr = {
        "",
        "",
        "",

        "",
        ""
    };

    public AudioSource cursorMoveSound;
	public AudioSource activateSkillSound;
	public AudioSource skillCompleteSound;
	public AudioSource skillLockedSound;
    public AudioSource unlockTierSound;

    private int costsForSkill = 0;
    private int costIncreaseOne = 10;
    private int costIncreaseTwo = 15;
    private int costIncreaseThree = 20;

    private int charID = 0;
    private int currentIndex = 0;
    private int spentOrbs = 0;

    public List<GameObject> skillArray;
    public Image cursorImage;

	private float minThreshold = 0.5f;
	private float maxThreshold = 0.5f;
	private bool axisYActive;

    private List<Hashtable> skillData = new List<Hashtable>();

    private Hashtable healthSkillDict = new Hashtable();
    private Hashtable damageSkillDict = new Hashtable();
    private Hashtable defenseSkillDict = new Hashtable();
    private Hashtable critSkillDict = new Hashtable();

    private Hashtable ASPDSkillDict = new Hashtable();
    private Hashtable MSPDSkillDict = new Hashtable();

    private Hashtable classSkillDict = new Hashtable();
    private Hashtable passiveSkillDict = new Hashtable();

    private bool tierTwoActive = false;
    private bool tierThreeActive = false;

    public TMP_Text skillTitleText;
    public TMP_Text skillInfoText;
    public TMP_Text skillCostText;
    public Image orbIcon;
    public Image currentSkillIcon;
    public Image bigLockIcon;

    private string secondaryEnableInfo = "";
    private string secondaryImproveInfo = "";

    private string newSkillTitle = "";
    private string newEnableInfo = "";
    private string newImproveInfo = "";

    private string newPassiveTitle = "";
    private string newPassiveInfo = "";

    // Character class skills
	private string[] enableSecondaryInfo = {"", "", "", ""};
	private string[] improveSecondaryInfo = {"", "", "", ""};

	private string[] activeSkillTitle = {"", "", "", ""};
	private string[] enableSkillInfo = {"", "", "", ""};
	private string[] improveSkillInfo = {"", "", "", ""};
	private string[] passiveSkillTitle = {"", "", "", ""};
	private string[] passiveSkillInfo = {"", "", "", ""};


    public void InitializeSkillUI() {
        characterSheetScript = this.gameObject.transform.parent.GetComponent<CharacterSheet>();
        skillsHandlerScript = GetComponent<SkillsHandler>();
        charID = this.gameObject.transform.parent.GetComponent<CharacterMovement>().playerID;

        // Personalize UI with colored gamepad and hero name
        gamepadIcon.color = Colors.keyPlayers[charID];
        heroName.text = SettingsHolder.heroNames[charID];

        // Get all info texts from CharClassContent script
        for (int i = 0; i < CharClassContent.classTexts.Length; i++) {
            enableSecondaryInfo[i] = CharClassContent.enableSecondaryTexts[characterSheetScript.charClass];
            improveSecondaryInfo[i] = CharClassContent.improveSecondaryTexts[characterSheetScript.charClass];

            activeSkillTitle[i] = CharClassContent.skillTitles[characterSheetScript.charClass];
            passiveSkillTitle[i] = CharClassContent.passiveTitles[characterSheetScript.charClass];
            enableSkillInfo[i] = CharClassContent.enableSkillTexts[characterSheetScript.charClass];
            improveSkillInfo[i] = CharClassContent.improveSkillTexts[characterSheetScript.charClass];
            passiveSkillInfo[i] = CharClassContent.passiveSkillTexts[characterSheetScript.charClass];
        }

        // Get texts from character sheet script
        secondaryEnableInfo = enableSecondaryInfo[characterSheetScript.charClass];
        secondaryImproveInfo = improveSecondaryInfo[characterSheetScript.charClass];

        newSkillTitle = activeSkillTitle[characterSheetScript.charClass];
        newEnableInfo = enableSkillInfo[characterSheetScript.charClass];
        newImproveInfo = improveSkillInfo[characterSheetScript.charClass];

        newPassiveTitle = passiveSkillTitle[characterSheetScript.charClass];
        newPassiveInfo = passiveSkillInfo[characterSheetScript.charClass];
        
        // Tier ONE skills and stats
        healthSkillDict.Add("Title", "Health");
        healthSkillDict.Add("Info", "+10%");
        // healthSkillDict.Add("Costs", new int[] {1, 1, 2, 2, 3});
        healthSkillDict.Add("Costs", 10);
        healthSkillDict.Add("CostIncrease", costIncreaseOne);
        healthSkillDict.Add("Level", -1);
        healthSkillDict.Add("Cap", 10);
        healthSkillDict.Add("Unlocked", true);
        
        damageSkillDict.Add("Title", "Basic Attack");
        damageSkillDict.Add("Info", "+15%");
        // damageSkillDict.Add("Costs", new int[] {1, 1, 2, 2, 3});
        damageSkillDict.Add("Costs", 10);
        damageSkillDict.Add("CostIncrease", costIncreaseOne);
        damageSkillDict.Add("Level", -1);
        damageSkillDict.Add("Cap", 10);
        damageSkillDict.Add("Unlocked", true);
        
        defenseSkillDict.Add("Title", "Defense");
        defenseSkillDict.Add("Info", "+20%");
        // defenseSkillDict.Add("Costs", new int[] {1, 1, 2, 2, 3});
        defenseSkillDict.Add("Costs", 10);
        defenseSkillDict.Add("CostIncrease", costIncreaseOne);
        defenseSkillDict.Add("Level", -1);
        defenseSkillDict.Add("Cap", 10);
        defenseSkillDict.Add("Unlocked", true);
        
        critSkillDict.Add("Title", "Advanced Attack");
        critSkillDict.Add("Info", "+6%");
        // critSkillDict.Add("Costs", new int[] {1, 1, 2, 2, 3});
        critSkillDict.Add("Costs", 20);
        critSkillDict.Add("CostIncrease", costIncreaseTwo);
        critSkillDict.Add("Level", -1);
        critSkillDict.Add("Cap", 10);
        critSkillDict.Add("Unlocked", false);
        
        // Tier TWO skills and stats
        ASPDSkillDict.Add("Title", "Attack Speed");
        ASPDSkillDict.Add("Info", "+10%");
        // ASPDSkillDict.Add("Costs", new int[] {1, 2, 2, 3, 4});
        ASPDSkillDict.Add("Costs", 20);
        ASPDSkillDict.Add("CostIncrease", costIncreaseTwo);
        ASPDSkillDict.Add("Level", -1);
        ASPDSkillDict.Add("Cap", 10);
        ASPDSkillDict.Add("Unlocked", false);

        MSPDSkillDict.Add("Title", "Move Speed");
        MSPDSkillDict.Add("Info", "+10%");
        // MSPDSkillDict.Add("Costs", new int[] {1, 2, 2, 3, 4});
        MSPDSkillDict.Add("Costs", 20);
        MSPDSkillDict.Add("CostIncrease", costIncreaseTwo);
        MSPDSkillDict.Add("Level", -1);
        MSPDSkillDict.Add("Cap", 10);
        MSPDSkillDict.Add("Unlocked", false);
        
        // Tier THREE skills and stats
        classSkillDict.Add("Title", newSkillTitle);
        classSkillDict.Add("Info", "???");
        // classSkillDict.Add("Costs", new int[] {3, 1, 2, 3, 4, 5});
        classSkillDict.Add("Costs", 30);
        classSkillDict.Add("CostIncrease", costIncreaseThree);
        classSkillDict.Add("Level", -1);
        classSkillDict.Add("Cap", 6);
        classSkillDict.Add("Unlocked", false);

        passiveSkillDict.Add("Title", newPassiveTitle);
        passiveSkillDict.Add("Info", newPassiveInfo);
        // passiveSkillDict.Add("Costs", new int[] {5});
        passiveSkillDict.Add("Costs", 30);
        passiveSkillDict.Add("CostIncrease", costIncreaseThree);
        passiveSkillDict.Add("Level", -1);
        passiveSkillDict.Add("Cap", 1);
        passiveSkillDict.Add("Unlocked", false);

        // Add dictionaries to a data list        
        skillData.Add(healthSkillDict);
        skillData.Add(damageSkillDict);
        skillData.Add(defenseSkillDict);
        skillData.Add(critSkillDict);

        skillData.Add(ASPDSkillDict);
        skillData.Add(MSPDSkillDict);

        skillData.Add(classSkillDict);
        skillData.Add(passiveSkillDict);
    }


    private void OnEnable() {
        DisplayNewTexts();
        UpdateOrbCount();
        ShowCurrentLevels();
        UpdateTiers();
    }


    private void BuySkill() {
        PlayActivationSounds();
        PayWithOrbs();
        UpdateOrbCount();
        CheckTiers();
        skillsHandlerScript.ActivateSkill(currentIndex, (int)skillData[currentIndex]["Level"]);
        IncreaseSkillLevel();
        UpdateSkillLevel();
        UpdateTiers();
        DisplayNewTexts();
    }


    private void PlayActivationSounds() {
        if ((int)skillData[currentIndex]["Level"] < (int)skillData[currentIndex]["Cap"] - 2) {
            Instantiate(activateSkillSound);
        } else {
            Instantiate(skillCompleteSound);
        }
    }


    private void PayWithOrbs() {
        // int currentLevel = (int)skillData[currentIndex]["Level"];
        // int costsForSkill = ((int[])skillData[currentIndex]["Costs"])[currentLevel+1];
        costsForSkill = (int)skillData[currentIndex]["Costs"] + ((int)skillData[currentIndex]["Level"] + 1) * (int)skillData[currentIndex]["CostIncrease"];
        characterSheetScript.currentOrbs -= costsForSkill;
        spentOrbs += costsForSkill;
    }


    private void UpdateOrbCount() {
        currentOrbCount.text = characterSheetScript.currentOrbs + "";
    }


    private void CheckTiers() {
        if (spentOrbs >= SettingsHolder.tierTwoCosts && !tierTwoActive) {
            tierTwoActive = true;
            // tierTwoImage.enabled = false;
            tierTwoImage.SetActive(false);

            ActivateTierTwo();
            Instantiate(unlockTierSound);
        }

        if (spentOrbs >= SettingsHolder.tierThreeCosts && !tierThreeActive) {
            tierThreeActive = true;
            // tierThreeImage.enabled = false;
            tierThreeImage.SetActive(false);

            ActivateTierThree();
            Instantiate(unlockTierSound);
        }
    }


    private void IncreaseSkillLevel() {
        skillData[currentIndex]["Level"] = (int)skillData[currentIndex]["Level"] + 1;
    }


    private void UpdateSkillLevel() {
        // Display current skill level
        if ((int)skillData[currentIndex]["Level"] < (int)skillData[currentIndex]["Cap"] - 1) {
            skillArray[currentIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ((int)skillData[currentIndex]["Level"] + 1) + " / " + (int)skillData[currentIndex]["Cap"];
        } else {
            skillArray[currentIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "YES!";
        }
    }


    private void ShowCurrentLevels() {
        // Display all current levels
        for (int i = 0; i < skillArray.Count; i++) {
            if ((int)skillData[i]["Level"] < (int)skillData[i]["Cap"] - 1) {
                skillArray[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ((int)skillData[i]["Level"] + 1) + " / " + (int)skillData[i]["Cap"];
            } else {
                skillArray[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "YES!";
            }
        }
    }


    private void UpdateTiers() {
        // Update orbs needed to unlock tier TWO
        if (!tierTwoActive) {
            orbsNeededTierTwo.text = (SettingsHolder.tierTwoCosts - spentOrbs) + "";

            for (int i = 3; i < 6; i++) {
                skillArray[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "    " + (SettingsHolder.tierTwoCosts - spentOrbs);
            }
        }
         if (!tierThreeActive) {
            orbsNeededTierThree.text = (SettingsHolder.tierThreeCosts - spentOrbs) + "";

            for (int i = 6; i < 8; i++) {
                skillArray[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "    " + (SettingsHolder.tierThreeCosts - spentOrbs);
            }
        }
    }


    private void DisplayNewTexts() {
        // Color texts depending if they're highlighted, locked, or regular
        for (int j = 0; j < skillArray.Count; j++) {
            int skillCosts = (int)skillData[j]["Costs"] + ((int)skillData[j]["Level"] + 1) * (int)skillData[j]["CostIncrease"];

            bool isUnlocked = (bool)skillData[j]["Unlocked"];
            bool enoughOrbs = characterSheetScript.currentOrbs >= skillCosts;

            if (j == currentIndex) {
                if (!isUnlocked || !enoughOrbs) {
                    // Transparent paper
                    skillArray[j].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = opaquePapers;
                    skillArray[j].transform.GetChild(2).GetComponent<Text>().color = opaquePapers;
                    skillArray[j].transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = opaquePapers;
                } else {
                    // Paper
                    skillArray[currentIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Colors.keyPaper;
                    skillArray[currentIndex].transform.GetChild(2).GetComponent<Text>().color = Colors.keyPaper;
                    skillArray[currentIndex].transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = Colors.keyPaper;
                }
            } else {
                if (!isUnlocked || !enoughOrbs) {
                    // Transparent blue
                    skillArray[j].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = opaqueSkills;
                    skillArray[j].transform.GetChild(2).GetComponent<Text>().color = opaqueSkills;
                    skillArray[j].transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = opaqueSkills;
                } else {
                    // Blue
                    skillArray[j].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Colors.blue20;
                    skillArray[j].transform.GetChild(2).GetComponent<Text>().color = Colors.blue20;
                    skillArray[j].transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = Colors.blue20;
                }
            }
        }

        // Get current stats and write texts for skillboard
        switch (currentIndex) {
            // Secondary Attack
            case 3:
                if ((int)skillData[currentIndex]["Level"] < 0) {
                    skillData[currentIndex]["Info"] = secondaryEnableInfo;
                } else {
                    skillData[currentIndex]["Info"] = secondaryImproveInfo;
                }
                break;
            // CLASS SKILL
            case 6:
                if ((int)skillData[currentIndex]["Level"] < 0) {
                    skillData[currentIndex]["Info"] = newEnableInfo;
                } else {
                    skillData[currentIndex]["Info"] = newImproveInfo;
                }
                break;
        }

        // Show correspronding title and text for current skill
        if ((bool)skillData[currentIndex]["Unlocked"]) {
            
            // Show these if the skill is UNLOCKED
            skillTitleText.text = (string)skillData[currentIndex]["Title"];
            skillInfoText.text = (string)skillData[currentIndex]["Info"];

            currentSkillIcon.color = new Color32(255, 255, 255, 255);
            currentSkillIcon.sprite = skillArray[currentIndex].transform.GetChild(4).GetComponent<Image>().sprite;
            bigLockIcon.color = new Color32(255, 255, 255, 0);
        } else {

            // Show these if the skill is still LOCKED
            skillTitleText.text = "Locked";
            if (currentIndex == 3 || currentIndex == 4 || currentIndex == 5) {
                if (SettingsHolder.tierTwoCosts - spentOrbs > 1) {
                    skillInfoText.text = "Spend " + (SettingsHolder.tierTwoCosts - spentOrbs) + " more orbs to unlock";
                } else {
                    skillInfoText.text = "Spend " + (SettingsHolder.tierTwoCosts - spentOrbs) + " more orb to unlock";
                }
            } else if (currentIndex == 6 || currentIndex == 7) {
                if (SettingsHolder.tierThreeCosts - spentOrbs > 1) {
                    skillInfoText.text = "Spend " + (SettingsHolder.tierThreeCosts - spentOrbs) + " more orbs to unlock";
                } else {
                    skillInfoText.text = "Spend " + (SettingsHolder.tierThreeCosts - spentOrbs) + " more orb to unlock";
                }
            }

            currentSkillIcon.color = new Color32(255, 255, 255, 0);
            bigLockIcon.color = new Color32(255, 255, 255, 255);
        }

        if ((int)skillData[currentIndex]["Level"] < (int)skillData[currentIndex]["Cap"] - 1) {
            // int currentLevel = (int)skillData[currentIndex]["Level"];
            // int costsForSkill = ((int[])skillData[currentIndex]["Costs"])[currentLevel+1];
            // int costsForSkill = (int)skillData[currentIndex]["Costs"] + (int)skillData[currentIndex]["Level"] * (int)skillData[currentIndex]["CostIncrease"];
            costsForSkill = (int)skillData[currentIndex]["Costs"] + ((int)skillData[currentIndex]["Level"] + 1) * (int)skillData[currentIndex]["CostIncrease"];
            skillCostText.text = costsForSkill + "";
            // skillCostText.alignment = TextAnchor.MiddleRight;
            orbIcon.color = new Color32(255, 255, 255, 255);
        } else {
            skillCostText.text = "";
            // skillCostText.alignment = TextAnchor.MiddleCenter;
            orbIcon.color = new Color32(255, 255, 255, 0);
        }
    }
    
    
    private void DisplayCursor() {
        cursorImage.transform.position = skillArray[currentIndex].transform.position;
        Instantiate(cursorMoveSound);
    }


    private void Update() {
        if (this.gameObject.transform.parent.GetComponent<CharacterMovement>().skillBoardOn) {
            GetInput();
        }
    }


    private void ActivateTierTwo() {
        for (int j = 3; j < 6; j++) {
            // Show Skill texts and icons
            skillArray[j].transform.GetChild(2).GetComponent<Text>().text = statIconsArr[j-3];
            skillArray[j].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = (string)skillData[j]["Title"];

            // Show current skill level
            skillArray[j].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
            skillArray[j].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "0 / " + (int)skillData[j]["Cap"];

            // Unlock the newly unlocked skills
            skillData[j]["Unlocked"] = true;
        }
    }


    private void ActivateTierThree() {
        for (int j = 6; j < 8; j++) {
            // Show Skill texts and icons
            skillArray[j].transform.GetChild(2).GetComponent<Text>().text = statIconsArr[j-3];
            skillArray[j].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = (string)skillData[j]["Title"];

            // Show current skill level
            skillArray[j].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
            skillArray[j].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "0 / " + (int)skillData[j]["Cap"];

            // Unlock the newly unlocked skills
            skillData[j]["Unlocked"] = true;
        }
    }


    private void GetInput() {
        // UI navigation with the analog sticks
        if (ReInput.players.GetPlayer(charID).GetAxis("LS Vertical") > maxThreshold && !axisYActive) {
            if (skillArray[currentIndex].GetComponent<ButtonNavList>().navUp) {
                axisYActive = true;
                currentIndex = skillArray[currentIndex].GetComponent<ButtonNavList>().navUp.GetComponent<ButtonNavList>().indexID;
                DisplayCursor();
                DisplayNewTexts();
            }
        }
        if (ReInput.players.GetPlayer(charID).GetAxis("LS Vertical") < -maxThreshold && !axisYActive) {
            if (skillArray[currentIndex].GetComponent<ButtonNavList>().navDown) {
                axisYActive = true;
                currentIndex = skillArray[currentIndex].GetComponent<ButtonNavList>().navDown.GetComponent<ButtonNavList>().indexID;
                DisplayCursor();
                DisplayNewTexts();
            }
        }

        if (ReInput.players.GetPlayer(charID).GetAxis("LS Vertical") <= minThreshold && ReInput.players.GetPlayer(charID).GetAxis("LS Vertical") >= -minThreshold) {
            axisYActive = false;
        }

        // UI navigation with the D-Pad buttons
        if (ReInput.players.GetPlayer(charID).GetButtonDown("DPadUp")) {
            if (skillArray[currentIndex].GetComponent<ButtonNavList>().navUp) {
                currentIndex = skillArray[currentIndex].GetComponent<ButtonNavList>().navUp.GetComponent<ButtonNavList>().indexID;
                DisplayCursor();
                DisplayNewTexts();
            }
        }
        if (ReInput.players.GetPlayer(charID).GetButtonDown("DPadDown")) {
            if (skillArray[currentIndex].GetComponent<ButtonNavList>().navDown) {
                currentIndex = skillArray[currentIndex].GetComponent<ButtonNavList>().navDown.GetComponent<ButtonNavList>().indexID;
                DisplayCursor();
                DisplayNewTexts();
            }
        }

        if (ReInput.players.GetPlayer(charID).GetButtonDown("X")) {
            if ((int)skillData[currentIndex]["Level"] < (int)skillData[currentIndex]["Cap"] - 1 && (bool)skillData[currentIndex]["Unlocked"]) {

                int nextLevelCosts = (int)skillData[currentIndex]["Costs"] + ((int)skillData[currentIndex]["Level"] + 1) * (int)skillData[currentIndex]["CostIncrease"];

                if (nextLevelCosts <= characterSheetScript.currentOrbs) {
                    // Activate skill
                    BuySkill();
                } else {
                    // Skill not available due to not enough orbs
                    Instantiate(skillLockedSound);
                    // print("Not enough orbs");
                }

            } else {
                print("Not unlocked yet!");
                Instantiate(skillLockedSound);
                // print("Skill already complete");
            }
		}
    }

}
