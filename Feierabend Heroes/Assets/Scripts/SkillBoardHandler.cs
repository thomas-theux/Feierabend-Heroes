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
    public Text orbsNeededTierTwo;
    public Text orbsNeededTierThree;
    public TMP_Text currentOrbCount;

    public AudioSource cursorMoveSound;
	public AudioSource activateSkillSound;
	public AudioSource skillCompleteSound;
	public AudioSource skillLockedSound;
    public AudioSource unlockTierSound;

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

    private string newSkillTitle = "";
    private string newEnableInfo = "";
    private string newImproveInfo = "";

    private string newPassiveTitle = "";
    private string newPassiveInfo = "";

    // Character class skills
	private string[] activeSkillTitle = {"", "", "", ""};
	private string[] enableSkillInfo = {"", "", "", ""};
	private string[] improveSkillInfo = {"", "", "", ""};
	private string[] passiveSkillTitle = {"", "", "", ""};
	private string[] passiveSkillInfo = {"", "", "", ""};


    public void InitializeSkillUI() {
        characterSheetScript = this.gameObject.transform.parent.GetComponent<CharacterSheet>();
        skillsHandlerScript = GetComponent<SkillsHandler>();
        charID = this.gameObject.transform.parent.GetComponent<CharacterMovement>().playerID;

        // Get all info texts from CharClassContent script
        for (int i = 0; i < CharClassContent.classTexts.Length; i++) {
            activeSkillTitle[i] = CharClassContent.skillTitles[characterSheetScript.charClass];
            passiveSkillTitle[i] = CharClassContent.passiveTitles[characterSheetScript.charClass];
            enableSkillInfo[i] = CharClassContent.enableSkillTexts[characterSheetScript.charClass];
            improveSkillInfo[i] = CharClassContent.improveSkillTexts[characterSheetScript.charClass];
            passiveSkillInfo[i] = CharClassContent.passiveSkillTexts[characterSheetScript.charClass];
        }

        // Get texts from character sheet script
        newSkillTitle = activeSkillTitle[characterSheetScript.charClass];
        newEnableInfo = enableSkillInfo[characterSheetScript.charClass];
        newImproveInfo = improveSkillInfo[characterSheetScript.charClass];

        newPassiveTitle = passiveSkillTitle[characterSheetScript.charClass];
        newPassiveInfo = passiveSkillInfo[characterSheetScript.charClass];
        
        // Tier ONE skills and stats
        healthSkillDict.Add("Title", "Health");
        healthSkillDict.Add("Info", "+10%");
        // healthSkillDict.Add("Costs", new int[] {1, 1, 2, 2, 3});
        healthSkillDict.Add("Costs", 1);
        healthSkillDict.Add("Level", -1);
        healthSkillDict.Add("Cap", 10);
        healthSkillDict.Add("Unlocked", true);
        
        damageSkillDict.Add("Title", "Damage");
        damageSkillDict.Add("Info", "+15%");
        // damageSkillDict.Add("Costs", new int[] {1, 1, 2, 2, 3});
        damageSkillDict.Add("Costs", 1);
        damageSkillDict.Add("Level", -1);
        damageSkillDict.Add("Cap", 10);
        damageSkillDict.Add("Unlocked", true);
        
        defenseSkillDict.Add("Title", "Defense");
        defenseSkillDict.Add("Info", "+20%");
        // defenseSkillDict.Add("Costs", new int[] {1, 1, 2, 2, 3});
        defenseSkillDict.Add("Costs", 1);
        defenseSkillDict.Add("Level", -1);
        defenseSkillDict.Add("Cap", 10);
        defenseSkillDict.Add("Unlocked", true);
        
        critSkillDict.Add("Title", "Secondary Attack");
        critSkillDict.Add("Info", "+6%");
        // critSkillDict.Add("Costs", new int[] {1, 1, 2, 2, 3});
        critSkillDict.Add("Costs", 2);
        critSkillDict.Add("Level", -1);
        critSkillDict.Add("Cap", 10);
        critSkillDict.Add("Unlocked", false);
        
        // Tier TWO skills and stats
        ASPDSkillDict.Add("Title", "Attack Speed");
        ASPDSkillDict.Add("Info", "+10%");
        // ASPDSkillDict.Add("Costs", new int[] {1, 2, 2, 3, 4});
        ASPDSkillDict.Add("Costs", 2);
        ASPDSkillDict.Add("Level", -1);
        ASPDSkillDict.Add("Cap", 10);
        ASPDSkillDict.Add("Unlocked", false);

        MSPDSkillDict.Add("Title", "Move Speed");
        MSPDSkillDict.Add("Info", "+10%");
        // MSPDSkillDict.Add("Costs", new int[] {1, 2, 2, 3, 4});
        MSPDSkillDict.Add("Costs", 2);
        MSPDSkillDict.Add("Level", -1);
        MSPDSkillDict.Add("Cap", 10);
        MSPDSkillDict.Add("Unlocked", false);
        
        // Tier THREE skills and stats
        classSkillDict.Add("Title", newSkillTitle);
        classSkillDict.Add("Info", "???");
        // classSkillDict.Add("Costs", new int[] {3, 1, 2, 3, 4, 5});
        classSkillDict.Add("Costs", 3);
        classSkillDict.Add("Level", -1);
        classSkillDict.Add("Cap", 10);
        classSkillDict.Add("Unlocked", false);

        passiveSkillDict.Add("Title", newPassiveTitle);
        passiveSkillDict.Add("Info", newPassiveInfo);
        // passiveSkillDict.Add("Costs", new int[] {5});
        passiveSkillDict.Add("Costs", 3);
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
        int costsForSkill = (int)skillData[currentIndex]["Costs"];
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
            // skillArray[currentIndex].transform.GetChild(1).GetComponent<Text>().text = ((int)skillData[currentIndex]["Level"] + 1) + "/" + (int)skillData[currentIndex]["Cap"];
            skillArray[currentIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LV " + ((int)skillData[currentIndex]["Level"] + 1);
        } else {
            skillArray[currentIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LV " + ((int)skillData[currentIndex]["Level"] + 1);
            // skillArray[currentIndex].transform.GetChild(2).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }


    private void UpdateTiers() {
        // Update orbs needed to unlock tier TWO
        if (!tierTwoActive) {
            orbsNeededTierTwo.text = (SettingsHolder.tierTwoCosts - spentOrbs) + "";
        }
         if (!tierThreeActive) {
            orbsNeededTierThree.text = (SettingsHolder.tierThreeCosts - spentOrbs) + "";
        }
    }


    private void DisplayNewTexts() {
        // Color texts depending if they're highlighted, locked, or regular
        for (int i = 0; i < skillArray.Count; i++) {
            if (i == currentIndex) {
                skillArray[currentIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Colors.keyPaper;
                skillArray[currentIndex].transform.GetChild(2).GetComponent<Text>().color = Colors.keyPaper;
                skillArray[currentIndex].transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = Colors.keyPaper;
            } else {
                skillArray[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Colors.blue20;
                skillArray[i].transform.GetChild(2).GetComponent<Text>().color = Colors.blue20;
                skillArray[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = Colors.blue20;
            }
        }

        // Get current stats and write texts for skillboard
        switch (currentIndex) {
            // HEALTH
            case 0:
                if ((int)skillData[currentIndex]["Level"] < (int)skillData[currentIndex]["Cap"] - 1) {
                    skillData[currentIndex]["Info"] =
                    characterSheetScript.maxHealth.ToString("F0") +
                    " → " +
                    (characterSheetScript.maxHealth + (characterSheetScript.maxHealth * SkillsHandler.increaseHP)).ToString("F0");
                } else {
                    skillData[currentIndex]["Title"] = "Health";
                    skillData[currentIndex]["Info"] = characterSheetScript.maxHealth.ToString("F0");
                }
                break;
            // DAMAGE
            case 1:
                float averageDmg = (characterSheetScript.attackOneDmg + characterSheetScript.attackTwoDmg) / 2;
                if ((int)skillData[currentIndex]["Level"] < (int)skillData[currentIndex]["Cap"] - 1) {
                    skillData[currentIndex]["Info"] =
                    averageDmg.ToString("F0") +
                    " → " +
                    (averageDmg + (averageDmg * SkillsHandler.increaseDMG)).ToString("F0");
                } else {
                    skillData[currentIndex]["Title"] = "Damage";
                    skillData[currentIndex]["Info"] = averageDmg.ToString("F0");
                }
                break;
            // DEFENSE
            case 2:
                if ((int)skillData[currentIndex]["Level"] < (int)skillData[currentIndex]["Cap"] - 1) {
                    skillData[currentIndex]["Info"] =
                    characterSheetScript.charDefense.ToString("F0") +
                    " → " +
                    (characterSheetScript.charDefense + (characterSheetScript.charDefense * SkillsHandler.increaseDEF)).ToString("F0");
                } else {
                    skillData[currentIndex]["Title"] = "Defense";
                    skillData[currentIndex]["Info"] = characterSheetScript.charDefense.ToString("F0");
                }
                break;
            // CRIT HIT
            case 3:
                if ((int)skillData[currentIndex]["Level"] < (int)skillData[currentIndex]["Cap"] - 1) {
                    skillData[currentIndex]["Info"] =
                    characterSheetScript.critChance.ToString("F0") + "%" +
                    " → " +
                    (characterSheetScript.critChance + SkillsHandler.increaseCRT).ToString("F0") + "%";
                } else {
                    skillData[currentIndex]["Title"] = "Critical Hit";
                    skillData[currentIndex]["Info"] = characterSheetScript.critChance.ToString("F0") + "%";
                }
                break;
            // ATTACK SPEED
            case 4:
                // NOCH KEINE AHNUNG WIE ICH DEN SCHEISS ANZEIGEN SOLL :D
                break;
            // MOVE SPEED
            case 5:
                if ((int)skillData[currentIndex]["Level"] < (int)skillData[currentIndex]["Cap"] - 1) {
                    skillData[currentIndex]["Info"] =
                    characterSheetScript.moveSpeed.ToString("F1") +
                    " → " +
                    (characterSheetScript.moveSpeed + (characterSheetScript.moveSpeed * SkillsHandler.increaseMSPD)).ToString("F1");
                } else {
                    skillData[currentIndex]["Title"] = "Move Speed";
                    skillData[currentIndex]["Info"] = characterSheetScript.moveSpeed.ToString("F1");
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
            // PASSIVE SKILL
            case 7:
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
            int costsForSkill = (int)skillData[currentIndex]["Costs"];
            skillCostText.text = costsForSkill + "";
            // skillCostText.alignment = TextAnchor.MiddleRight;
            orbIcon.color = new Color32(255, 255, 255, 255);
        } else {
            skillCostText.text = "✔";
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
            // Set skill icons transparency to 100%
            // skillArray[j].GetComponent<Image>().color = new Color32(255, 255, 255, 255);

            // Show Skill texts and icons
            skillArray[j].transform.GetChild(2).GetComponent<Text>().text = "";
            skillArray[j].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = (string)skillData[j]["Title"];
            
            // Show LevelDisplayer
            // skillArray[j].transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255);

            // Show current skill level
            // skillArray[j].transform.GetChild(1).GetComponent<Text>().text = "0/" + (int)skillData[j]["Cap"];
            skillArray[j].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LV 0";

            // Unlock the newly unlocked skills
            skillData[j]["Unlocked"] = true;
        }
    }


    private void ActivateTierThree() {
        for (int j = 6; j < 8; j++) {
            // Set skill icons transparency to 100%
            // skillArray[j].GetComponent<Image>().color = new Color32(255, 255, 255, 255);

            // Show Skill texts
            skillArray[j].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = (string)skillData[j]["Title"];
            
            // Show LevelDisplayer
            // skillArray[j].transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255);

            // Show current skill level
            // skillArray[j].transform.GetChild(1).GetComponent<Text>().text = "0/" + (int)skillData[j]["Cap"];
            skillArray[j].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LV 0";

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

                // int nextLevel = (int)skillData[currentIndex]["Level"] + 1;
                // int nextLevelCosts = ((int[])skillData[currentIndex]["Costs"])[nextLevel];
                int nextLevelCosts = (int)skillData[currentIndex]["Costs"];

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
                // Skill fully acivated
                // print("Skill already complete");
            }
		}
    }

}
