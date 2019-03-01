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
	private string[] activeSkillTitle = new string[] {
        "Build Turret Gun",
        "Spawn Companion",
        "Throw Crystal Ball",
        "Shift Shapes",
    };

	private string[] enableSkillInfo = new string[] {
        "Shoots at enemies on sight (lifetime 20s)",
        "Attacks enemies on sight (lifetime 12s)",
        "Throw a sphere that blinds enemies",
        "Fight faster, move quicker (duration 14s)",
    };

	private string[] improveSkillInfo = new string[] {
        "Attack Speed +5%, Radius +4m",
        "Radius +3m, Lifetime +4s",
        "Radius +2m, Blinding Duration +1s",
        "Duration +3s, Attack Speed +10%",
    };

	private string[] passiveSkillTitle = new string[] {
        "Self Repair (passive)",
        "Slowing Tendrils (passive)",
        "Foresight (passive)",
        "Invisibility Cloak (passive)",
    };

	private string[] passiveSkillInfo = new string[] {
        "Heals your character over time",
        "Slows down nearby enemies",
        "See other players locations",
        "Decreases your visibility by 50%",
    };


    // public void Start() {
    public void InitializeSkillUI() {
        characterSheetScript = this.gameObject.transform.parent.GetComponent<CharacterSheet>();
        skillsHandlerScript = GetComponent<SkillsHandler>();
        charID = this.gameObject.transform.parent.GetComponent<CharacterMovement>().playerID;

        // Get texts from character sheet script
        newSkillTitle = activeSkillTitle[characterSheetScript.charClass];
        newEnableInfo = enableSkillInfo[characterSheetScript.charClass];
        newImproveInfo = improveSkillInfo[characterSheetScript.charClass];

        newPassiveTitle = passiveSkillTitle[characterSheetScript.charClass];
        newPassiveInfo = passiveSkillInfo[characterSheetScript.charClass];
        
        // Tier ONE skills and stats
        healthSkillDict.Add("Title", "Health");
        healthSkillDict.Add("Info", "+10%");
        healthSkillDict.Add("Costs", new int[] {1, 1, 2, 2, 3});
        healthSkillDict.Add("Level", -1);
        healthSkillDict.Add("Cap", 5);
        
        damageSkillDict.Add("Title", "Damage");
        damageSkillDict.Add("Info", "+14");
        damageSkillDict.Add("Costs", new int[] {1, 1, 2, 2, 3});
        damageSkillDict.Add("Level", -1);
        damageSkillDict.Add("Cap", 5);
        
        defenseSkillDict.Add("Title", "Defense");
        defenseSkillDict.Add("Info", "+20%");
        defenseSkillDict.Add("Costs", new int[] {1, 1, 2, 2, 3});
        defenseSkillDict.Add("Level", -1);
        defenseSkillDict.Add("Cap", 5);
        
        dodgeSkillDict.Add("Title", "Dodge");
        dodgeSkillDict.Add("Info", "+6%");
        dodgeSkillDict.Add("Costs", new int[] {1, 1, 2, 2, 3});
        dodgeSkillDict.Add("Level", -1);
        dodgeSkillDict.Add("Cap", 5);
        
        critSkillDict.Add("Title", "Critical Hit");
        critSkillDict.Add("Info", "+6%");
        critSkillDict.Add("Costs", new int[] {1, 1, 2, 2, 3});
        critSkillDict.Add("Level", -1);
        critSkillDict.Add("Cap", 5);
        
        // Tier TWO skills and stats
        ASPDSkillDict.Add("Title", "Attack Speed");
        ASPDSkillDict.Add("Info", "+10%");
        ASPDSkillDict.Add("Costs", new int[] {1, 2, 2, 3, 4});
        ASPDSkillDict.Add("Level", -1);
        ASPDSkillDict.Add("Cap", 5);

        MSPDSkillDict.Add("Title", "Move Speed");
        MSPDSkillDict.Add("Info", "+10%");
        MSPDSkillDict.Add("Costs", new int[] {1, 2, 2, 3, 4});
        MSPDSkillDict.Add("Level", -1);
        MSPDSkillDict.Add("Cap", 5);

        doubleOrbSkillDict.Add("Title", "Double Orb");
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
        PayWithOrbs();
        UpdateOrbCount();
        CheckTiers();
        skillsHandlerScript.ActivateSkill(currentIndex, (int)skillData[currentIndex]["Level"]);
        IncreaseSkillLevel();
        UpdateSkillLevel();
        UpdateTiers();
        DisplayNewTexts();
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
        }

        if (spentOrbs >= SettingsHolder.tierThreeCosts && !tierThreeActive) {
            tierThreeActive = true;
            // tierThreeImage.enabled = false;
            tierThreeImage.SetActive(false);

            ActivateTierThree();
        }
    }


    private void IncreaseSkillLevel() {
        skillData[currentIndex]["Level"] = (int)skillData[currentIndex]["Level"] + 1;
    }


    private void UpdateSkillLevel() {
        // Display current skill level
        skillArray[currentIndex].transform.GetChild(1).GetComponent<Text>().text = ((int)skillData[currentIndex]["Level"] + 1) + "/" + (int)skillData[currentIndex]["Cap"];
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
        // Write texts for class and passive skills
        if (currentIndex == 8) {
            if ((int)skillData[currentIndex]["Level"] < 0) {
                skillData[currentIndex]["Info"] = newEnableInfo;
            } else {
                skillData[currentIndex]["Info"] = newImproveInfo;
            }
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
            skillCostText.text = "";
            orbIcon.color = new Color32(255, 255, 255, 0);
        }
    }
    
    
    private void DisplayCursor() {
        cursorImage.transform.position = skillArray[currentIndex].transform.position;
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
                    print("Not enough orbs");
                }

            } else {
                // Skill fully acivated
                print("Skill already complete");
            }
		}
    }

}
