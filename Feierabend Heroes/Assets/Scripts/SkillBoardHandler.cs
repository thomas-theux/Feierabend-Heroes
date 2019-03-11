using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class SkillBoardHandler : MonoBehaviour {

    private CharacterSheet characterSheetScript;
    private SkillsHandler skillsHandlerScript;

    public GameObject tierTwoImage;
    public GameObject tierThreeImage;
    public Text orbsNeededTierTwo;
    public Text orbsNeededTierThree;
    public Text currentOrbCount;

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
	private bool axisXActive;
	private bool axisYActive;

    private List<Hashtable> skillData = new List<Hashtable>();

    private Hashtable healthSkillDict = new Hashtable();
    private Hashtable damageSkillDict = new Hashtable();
    private Hashtable defenseSkillDict = new Hashtable();
    private Hashtable dodgeSkillDict = new Hashtable();
    private Hashtable critSkillDict = new Hashtable();

    private Hashtable ASPDSkillDict = new Hashtable();
    private Hashtable MSPDSkillDict = new Hashtable();
    private Hashtable doubleOrbSkillDict = new Hashtable();

    private Hashtable classSkillDict = new Hashtable();
    private Hashtable passiveSkillDict = new Hashtable();
    private Hashtable applesSkillDict = new Hashtable();

    private bool tierTwoActive = false;
    private bool tierThreeActive = false;

    public Text skillTitleText;
    public Text skillInfoText;
    public Text skillCostText;
    public Image orbIcon;

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


    // public void Start() {
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
        healthSkillDict.Add("Title", "Improve Health");
        healthSkillDict.Add("Info", "+10%");
        healthSkillDict.Add("Costs", new int[] {1, 1, 2, 2, 3});
        healthSkillDict.Add("Level", -1);
        healthSkillDict.Add("Cap", 5);
        
        damageSkillDict.Add("Title", "Improve Damage");
        damageSkillDict.Add("Info", "+15%");
        damageSkillDict.Add("Costs", new int[] {1, 1, 2, 2, 3});
        damageSkillDict.Add("Level", -1);
        damageSkillDict.Add("Cap", 5);
        
        defenseSkillDict.Add("Title", "Improve Defense");
        defenseSkillDict.Add("Info", "+20%");
        defenseSkillDict.Add("Costs", new int[] {1, 1, 2, 2, 3});
        defenseSkillDict.Add("Level", -1);
        defenseSkillDict.Add("Cap", 5);
        
        dodgeSkillDict.Add("Title", "Dodge Attacks");
        dodgeSkillDict.Add("Info", "+6%");
        dodgeSkillDict.Add("Costs", new int[] {1, 1, 2, 2, 3});
        dodgeSkillDict.Add("Level", -1);
        dodgeSkillDict.Add("Cap", 5);
        
        critSkillDict.Add("Title", "Critical Hit (2x Damage)");
        critSkillDict.Add("Info", "+6%");
        critSkillDict.Add("Costs", new int[] {1, 1, 2, 2, 3});
        critSkillDict.Add("Level", -1);
        critSkillDict.Add("Cap", 5);
        
        // Tier TWO skills and stats
        ASPDSkillDict.Add("Title", "Increase Attack Speed");
        ASPDSkillDict.Add("Info", "+10%");
        ASPDSkillDict.Add("Costs", new int[] {1, 2, 2, 3, 4});
        ASPDSkillDict.Add("Level", -1);
        ASPDSkillDict.Add("Cap", 5);

        MSPDSkillDict.Add("Title", "Increase Move Speed");
        MSPDSkillDict.Add("Info", "+10%");
        MSPDSkillDict.Add("Costs", new int[] {1, 2, 2, 3, 4});
        MSPDSkillDict.Add("Level", -1);
        MSPDSkillDict.Add("Cap", 5);

        doubleOrbSkillDict.Add("Title", "Increase Double Orb Chance");
        doubleOrbSkillDict.Add("Info", "+15%");
        doubleOrbSkillDict.Add("Costs", new int[] {2, 3, 4, 5, 6});
        doubleOrbSkillDict.Add("Level", -1);
        doubleOrbSkillDict.Add("Cap", 5);
        
        // Tier THREE skills and stats
        classSkillDict.Add("Title", newSkillTitle);
        classSkillDict.Add("Info", "???");
        classSkillDict.Add("Costs", new int[] {3, 1, 2, 3, 4, 5});
        classSkillDict.Add("Level", -1);
        classSkillDict.Add("Cap", 6);

        passiveSkillDict.Add("Title", newPassiveTitle);
        passiveSkillDict.Add("Info", newPassiveInfo);
        passiveSkillDict.Add("Costs", new int[] {5});
        passiveSkillDict.Add("Level", -1);
        passiveSkillDict.Add("Cap", 1);

        applesSkillDict.Add("Title", "Find Apples");
        applesSkillDict.Add("Info", "Apples heal your hero");
        applesSkillDict.Add("Costs", new int[] {3});
        applesSkillDict.Add("Level", -1);
        applesSkillDict.Add("Cap", 1);

        // Add dictionaries to a data list        
        skillData.Add(healthSkillDict);
        skillData.Add(damageSkillDict);
        skillData.Add(defenseSkillDict);
        skillData.Add(dodgeSkillDict);
        skillData.Add(critSkillDict);

        skillData.Add(ASPDSkillDict);
        skillData.Add(MSPDSkillDict);
        skillData.Add(doubleOrbSkillDict);

        skillData.Add(classSkillDict);
        skillData.Add(passiveSkillDict);
        skillData.Add(applesSkillDict);
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
        int currentLevel = (int)skillData[currentIndex]["Level"];
        int costsForSkill = ((int[])skillData[currentIndex]["Costs"])[currentLevel+1];
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
            skillArray[currentIndex].transform.GetChild(1).GetComponent<Text>().text = ((int)skillData[currentIndex]["Level"] + 1) + "/" + (int)skillData[currentIndex]["Cap"];
        } else {
            skillArray[currentIndex].transform.GetChild(1).GetComponent<Text>().text = "";
            skillArray[currentIndex].transform.GetChild(2).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
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
            // DODGE
            case 3:
                if ((int)skillData[currentIndex]["Level"] < (int)skillData[currentIndex]["Cap"] - 1) {
                    skillData[currentIndex]["Info"] =
                    characterSheetScript.dodgeChance.ToString("F0") + "%" +
                    " → " +
                    (characterSheetScript.dodgeChance + SkillsHandler.increaseDDG).ToString("F0") + "%";
                } else {
                    skillData[currentIndex]["Title"] = "Dodge Chance";
                    skillData[currentIndex]["Info"] = characterSheetScript.dodgeChance.ToString("F0") + "%";
                }
                break;
            // CRIT HIT
            case 4:
                if ((int)skillData[currentIndex]["Level"] < (int)skillData[currentIndex]["Cap"] - 1) {
                    skillData[currentIndex]["Info"] =
                    characterSheetScript.critChance.ToString("F0") + "%" +
                    " → " +
                    (characterSheetScript.critChance + SkillsHandler.increaseCRT).ToString("F0") + "%";
                } else {
                    skillData[currentIndex]["Title"] = "Critical Hit Chance";
                    skillData[currentIndex]["Info"] = characterSheetScript.critChance.ToString("F0") + "%";
                }
                break;
            // ATTACK SPEED
            case 5:
                // NOCH KEINE AHNUNG WIE ICH DEN SCHEISS ANZEIGEN SOLL :D
                break;
            // MOVE SPEED
            case 6:
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
            // ORB FINDING
            case 7:
                if ((int)skillData[currentIndex]["Level"] < (int)skillData[currentIndex]["Cap"] - 1) {
                    skillData[currentIndex]["Info"] =
                    characterSheetScript.doubleOrbChance.ToString("F0") + "%" +
                    " → " +
                    (characterSheetScript.doubleOrbChance + SkillsHandler.increaseDBLORB).ToString("F0") + "%";
                } else {
                    skillData[currentIndex]["Title"] = "Double Orb Chance";
                    skillData[currentIndex]["Info"] = characterSheetScript.doubleOrbChance.ToString("F0") + "%";
                }
                break;
            // CLASS SKILL
            case 8:
                if ((int)skillData[currentIndex]["Level"] < 0) {
                    skillData[currentIndex]["Info"] = newEnableInfo;
                } else {
                    skillData[currentIndex]["Info"] = newImproveInfo;
                }
                break;
            // PASSIVE SKILL
            case 9:
                break;
            // FIND APPLES
            case 10:
                break;
        }

        // Show correspronding title and text for current skill
        skillTitleText.text = (string)skillData[currentIndex]["Title"];
        skillInfoText.text = (string)skillData[currentIndex]["Info"];

        if ((int)skillData[currentIndex]["Level"] < (int)skillData[currentIndex]["Cap"] - 1) {
            int currentLevel = (int)skillData[currentIndex]["Level"];
            int costsForSkill = ((int[])skillData[currentIndex]["Costs"])[currentLevel+1];
            skillCostText.text = costsForSkill + "";
            orbIcon.color = new Color32(255, 255, 255, 255);
        } else {
            skillCostText.text = "✔";
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
        for (int i = 1; i < 4; i++) {
            // Assign unlocked skills to unlock navigation
            skillArray[i].GetComponent<ButtonNav>().navDown = skillArray[i+4].GetComponent<Image>();
        }

        for (int j = 5; j < 8; j++) {
            // Set skill icons transparency to 100%
            skillArray[j].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            
            // Show LevelDisplayer
            skillArray[j].transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255);

            // Show current skill level
            skillArray[j].transform.GetChild(1).GetComponent<Text>().text = "0/" + (int)skillData[j]["Cap"];
        }
    }


    private void ActivateTierThree() {
        for (int i = 5; i < 8; i++) {
            // Assign unlocked skills to unlock navigation
            skillArray[i].GetComponent<ButtonNav>().navDown = skillArray[i+3].GetComponent<Image>();
        }

        for (int j = 8; j < 11; j++) {
            // Set skill icons transparency to 100%
            skillArray[j].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            
            // Show LevelDisplayer
            skillArray[j].transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255);

            // Show current skill level
            skillArray[j].transform.GetChild(1).GetComponent<Text>().text = "0/" + (int)skillData[j]["Cap"];
        }
    }


    private void GetInput() {
        // UI navigation with the analog sticks
        if (ReInput.players.GetPlayer(charID).GetAxis("LS Vertical") > maxThreshold && !axisYActive) {
            if (skillArray[currentIndex].GetComponent<ButtonNav>().navUp) {
                axisYActive = true;
                currentIndex = skillArray[currentIndex].GetComponent<ButtonNav>().navUp.GetComponent<ButtonNav>().indexID;
                DisplayCursor();
                DisplayNewTexts();
            }
        }
        if (ReInput.players.GetPlayer(charID).GetAxis("LS Vertical") < -maxThreshold && !axisYActive) {
            if (skillArray[currentIndex].GetComponent<ButtonNav>().navDown) {
                axisYActive = true;
                currentIndex = skillArray[currentIndex].GetComponent<ButtonNav>().navDown.GetComponent<ButtonNav>().indexID;
                DisplayCursor();
                DisplayNewTexts();
            }
        }
        if (ReInput.players.GetPlayer(charID).GetAxis("LS Horizontal") > maxThreshold && !axisXActive) {
            if (skillArray[currentIndex].GetComponent<ButtonNav>().navRight) {
                axisXActive = true;
                currentIndex = skillArray[currentIndex].GetComponent<ButtonNav>().navRight.GetComponent<ButtonNav>().indexID;
                DisplayCursor();
                DisplayNewTexts();
            }
        }
        if (ReInput.players.GetPlayer(charID).GetAxis("LS Horizontal") < -maxThreshold && !axisXActive) {
            if (skillArray[currentIndex].GetComponent<ButtonNav>().navLeft) {
                axisXActive = true;
                currentIndex = skillArray[currentIndex].GetComponent<ButtonNav>().navLeft.GetComponent<ButtonNav>().indexID;
                DisplayCursor();
                DisplayNewTexts();
            }
        }

        if (ReInput.players.GetPlayer(charID).GetAxis("LS Vertical") <= minThreshold && ReInput.players.GetPlayer(charID).GetAxis("LS Vertical") >= -minThreshold) {
            axisYActive = false;
        }
        if (ReInput.players.GetPlayer(charID).GetAxis("LS Horizontal") <= minThreshold && ReInput.players.GetPlayer(charID).GetAxis("LS Horizontal") >= -minThreshold) {
            axisXActive = false;
        }

        // UI navigation with the D-Pad buttons
        if (ReInput.players.GetPlayer(charID).GetButtonDown("DPadUp")) {
            if (skillArray[currentIndex].GetComponent<ButtonNav>().navUp) {
                currentIndex = skillArray[currentIndex].GetComponent<ButtonNav>().navUp.GetComponent<ButtonNav>().indexID;
                DisplayCursor();
                DisplayNewTexts();
            }
        }
        if (ReInput.players.GetPlayer(charID).GetButtonDown("DPadDown")) {
            if (skillArray[currentIndex].GetComponent<ButtonNav>().navDown) {
                currentIndex = skillArray[currentIndex].GetComponent<ButtonNav>().navDown.GetComponent<ButtonNav>().indexID;
                DisplayCursor();
                DisplayNewTexts();
            }
        }
        if (ReInput.players.GetPlayer(charID).GetButtonDown("DPadLeft")) {
            if (skillArray[currentIndex].GetComponent<ButtonNav>().navLeft) {
                currentIndex = skillArray[currentIndex].GetComponent<ButtonNav>().navLeft.GetComponent<ButtonNav>().indexID;
                DisplayCursor();
                DisplayNewTexts();
            }
        }
        if (ReInput.players.GetPlayer(charID).GetButtonDown("DPadRight")) {
            if (skillArray[currentIndex].GetComponent<ButtonNav>().navRight) {
                currentIndex = skillArray[currentIndex].GetComponent<ButtonNav>().navRight.GetComponent<ButtonNav>().indexID;
                DisplayCursor();
                DisplayNewTexts();
            }
        }

        if (ReInput.players.GetPlayer(charID).GetButtonDown("X")) {
            if ((int)skillData[currentIndex]["Level"] < (int)skillData[currentIndex]["Cap"] - 1) {

                int nextLevel = (int)skillData[currentIndex]["Level"] + 1;
                int nextLevelCosts = ((int[])skillData[currentIndex]["Costs"])[nextLevel];

                if (nextLevelCosts <= characterSheetScript.currentOrbs) {
                    // Activate skill
                    BuySkill();
                } else {
                    // Skill not available due to not enough orbs
                    Instantiate(skillLockedSound);
                    // print("Not enough orbs");
                }

            } else {
                // Skill fully acivated
                // print("Skill already complete");
            }
		}
    }

}
