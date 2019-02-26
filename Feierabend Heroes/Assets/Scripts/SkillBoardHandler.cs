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


    private void Awake() {
        characterSheetScript = this.gameObject.transform.parent.GetComponent<CharacterSheet>();
        skillsHandlerScript = GetComponent<SkillsHandler>();
        charID = this.gameObject.transform.parent.GetComponent<CharacterMovement>().playerID;
        
        // Tier ONE skills and stats
        healthSkillDict.Add("Title", "Health");
        healthSkillDict.Add("Text", "+10%");
        healthSkillDict.Add("Costs", new int[] {1, 1, 2, 2, 3});
        healthSkillDict.Add("Level", -1);
        healthSkillDict.Add("Cap", 5);
        
        damageSkillDict.Add("Title", "Damage");
        damageSkillDict.Add("Text", "+14");
        damageSkillDict.Add("Costs", new int[] {1, 1, 2, 2, 3});
        damageSkillDict.Add("Level", -1);
        damageSkillDict.Add("Cap", 5);
        
        defenseSkillDict.Add("Title", "Defense");
        defenseSkillDict.Add("Text", "+20%");
        defenseSkillDict.Add("Costs", new int[] {1, 1, 2, 2, 3});
        defenseSkillDict.Add("Level", -1);
        defenseSkillDict.Add("Cap", 5);
        
        dodgeSkillDict.Add("Title", "Dodge");
        dodgeSkillDict.Add("Text", "+5%");
        dodgeSkillDict.Add("Costs", new int[] {1, 1, 2, 2, 3});
        dodgeSkillDict.Add("Level", -1);
        dodgeSkillDict.Add("Cap", 5);
        
        critSkillDict.Add("Title", "Critical Hit");
        critSkillDict.Add("Text", "+5%");
        critSkillDict.Add("Costs", new int[] {1, 1, 2, 2, 3});
        critSkillDict.Add("Level", -1);
        critSkillDict.Add("Cap", 5);
        
        // Tier TWO skills and stats
        ASPDSkillDict.Add("Title", "Attack Speed");
        ASPDSkillDict.Add("Text", "+10%");
        ASPDSkillDict.Add("Costs", new int[] {1, 2, 2, 3, 4});
        ASPDSkillDict.Add("Level", -1);
        ASPDSkillDict.Add("Cap", 5);

        MSPDSkillDict.Add("Title", "Move Speed");
        MSPDSkillDict.Add("Text", "+10%");
        MSPDSkillDict.Add("Costs", new int[] {1, 2, 2, 3, 4});
        MSPDSkillDict.Add("Level", -1);
        MSPDSkillDict.Add("Cap", 5);

        doubleOrbSkillDict.Add("Title", "Double Orb");
        doubleOrbSkillDict.Add("Text", "+15%");
        doubleOrbSkillDict.Add("Costs", new int[] {2, 3, 4, 5, 6});
        doubleOrbSkillDict.Add("Level", -1);
        doubleOrbSkillDict.Add("Cap", 5);
        
        // Tier THREE skills and stats
        classSkillDict.Add("Title", "Enable Skill");
        classSkillDict.Add("Text", "???");
        classSkillDict.Add("Costs", new int[] {3, 1, 2, 3, 4, 5});
        classSkillDict.Add("Level", -1);
        classSkillDict.Add("Cap", 6);

        passiveSkillDict.Add("Title", "Enable Passive");
        passiveSkillDict.Add("Text", "???");
        passiveSkillDict.Add("Costs", new int[] {5});
        passiveSkillDict.Add("Level", -1);
        passiveSkillDict.Add("Cap", 1);

        applesSkillDict.Add("Title", "Find Apples");
        applesSkillDict.Add("Text", "Apples heal your hero");
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

        DisplayNewTexts();
    }


    private void BuySkill() {
        PayWithOrbs();
        UpdateOrbCount();
        CheckTiers();
        skillsHandlerScript.ActivateSkill(currentIndex);
        IncreaseSkillLevel();
        DisplayNewTexts();
    }


    private void PayWithOrbs() {
        int currentLevel = (int)skillData[currentIndex]["Level"];
        int costsForSkill = ((int[])skillData[currentIndex]["Costs"])[currentLevel+1];
        characterSheetScript.currentOrbs -= costsForSkill;
        spentOrbs += costsForSkill;
    }


    private void UpdateOrbCount() {
        
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


    private void DisplayNewTexts() {
        // Display current skill level
        skillArray[currentIndex].transform.GetChild(1).GetComponent<Text>().text = ((int)skillData[currentIndex]["Level"] + 1) + "/" + (int)skillData[currentIndex]["Cap"];

        // Update orbs needed to unlock tier TWO
        if (!tierTwoActive) {
            orbsNeededTierTwo.text = (SettingsHolder.tierTwoCosts - spentOrbs) + "";
        }
         if (!tierThreeActive) {
            orbsNeededTierThree.text = (SettingsHolder.tierThreeCosts - spentOrbs) + "";
        }
    }


    private void Update() {
        GetInput();
    }
    
    
    private void DisplayCursor() {
        cursorImage.transform.position = skillArray[currentIndex].transform.position;
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
            }
        }
        if (ReInput.players.GetPlayer(charID).GetAxis("LS Vertical") < -maxThreshold && !axisYActive) {
            if (skillArray[currentIndex].GetComponent<ButtonNav>().navDown) {
                axisYActive = true;
                currentIndex = skillArray[currentIndex].GetComponent<ButtonNav>().navDown.GetComponent<ButtonNav>().indexID;
                DisplayCursor();
            }
        }
        if (ReInput.players.GetPlayer(charID).GetAxis("LS Horizontal") > maxThreshold && !axisXActive) {
            if (skillArray[currentIndex].GetComponent<ButtonNav>().navRight) {
                axisXActive = true;
                currentIndex = skillArray[currentIndex].GetComponent<ButtonNav>().navRight.GetComponent<ButtonNav>().indexID;
                DisplayCursor();
            }
        }
        if (ReInput.players.GetPlayer(charID).GetAxis("LS Horizontal") < -maxThreshold && !axisXActive) {
            if (skillArray[currentIndex].GetComponent<ButtonNav>().navLeft) {
                axisXActive = true;
                currentIndex = skillArray[currentIndex].GetComponent<ButtonNav>().navLeft.GetComponent<ButtonNav>().indexID;
                DisplayCursor();
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
            }
        }
        if (ReInput.players.GetPlayer(charID).GetButtonDown("DPadDown")) {
            if (skillArray[currentIndex].GetComponent<ButtonNav>().navDown) {
                currentIndex = skillArray[currentIndex].GetComponent<ButtonNav>().navDown.GetComponent<ButtonNav>().indexID;
                DisplayCursor();
            }
        }
        if (ReInput.players.GetPlayer(charID).GetButtonDown("DPadLeft")) {
            if (skillArray[currentIndex].GetComponent<ButtonNav>().navLeft) {
                currentIndex = skillArray[currentIndex].GetComponent<ButtonNav>().navLeft.GetComponent<ButtonNav>().indexID;
                DisplayCursor();
            }
        }
        if (ReInput.players.GetPlayer(charID).GetButtonDown("DPadRight")) {
            if (skillArray[currentIndex].GetComponent<ButtonNav>().navRight) {
                currentIndex = skillArray[currentIndex].GetComponent<ButtonNav>().navRight.GetComponent<ButtonNav>().indexID;
                DisplayCursor();
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
