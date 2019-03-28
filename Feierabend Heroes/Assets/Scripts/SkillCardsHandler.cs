using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class SkillCardsHandler : MonoBehaviour {

    private CharacterSheet characterSheetScript;
    private CharacterMovement characterMovementScript;
    private SkillsHandler skillsHandlerScript;

    private int charID = 0;
    private int currentIndex = 0;
    private bool cardsAlreadyDrawn = false;

    public GameObject[] skillCardsArray;
    public Sprite[] skillIconsArray;
    public GameObject[] levelDisplayerParent;
    public GameObject skillLevelDisplayer;
    public Image cursorImage;
    public Text currentOrbCount;

    public AudioSource cursorMoveSound;
	public AudioSource activateSkillSound;
	public AudioSource skillCompleteSound;
	public AudioSource skillLockedSound;
	public AudioSource drawCardsSound;

    private float minThreshold = 0.5f;
	private float maxThreshold = 0.5f;
	private bool axisXActive;

    private List<Hashtable> skillData = new List<Hashtable>();
    private Hashtable healthSkillDict = new Hashtable();
    private Hashtable damageSkillDict = new Hashtable();
    private Hashtable defenseSkillDict = new Hashtable();
    private Hashtable critSkillDict = new Hashtable();
    private Hashtable ASPDSkillDict = new Hashtable();
    private Hashtable MSPDSkillDict = new Hashtable();
    private Hashtable classSkillDict = new Hashtable();
    private Hashtable passiveSkillDict = new Hashtable();

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

    private List<int> allAvailableBasicsArr = new List<int>();
    private List<int> allAvailableSpecialsArr = new List<int>();
    private List<int> currentBasicsArr = new List<int>();
    private List<int> currentSpecialsArr = new List<int>();
    private List<int> drawnSkillsArr = new List<int>();

    private int basicSkillsCount = 6;
    private int specialSkillsCount = 2;
    private int maxCardDraw = 3;
    private int currentSpecialSkillChance = 0;
    private int specialSkillChanceIncrease = 1;
    private int maxSpecialSkillChance = 10;
    private bool specialCardDrawn = false;

    private int minLevel;
    private int maxLevel;
    private float levelDisplayerDistance = 26.0f;

    private bool canPickCard = false;
    private bool initialDrawAnimation = false;
    private float moveInDuration = 1.5f;
	private float t = 0;
    

    public void InitializeSkillUI() {
        characterSheetScript = this.gameObject.transform.parent.GetComponent<CharacterSheet>();
        characterMovementScript = this.gameObject.transform.parent.GetComponent<CharacterMovement>();
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
        healthSkillDict.Add("Info", "+12%");
        healthSkillDict.Add("Costs", 1);
        healthSkillDict.Add("Level", 0);
        healthSkillDict.Add("Cap", 9);
        
        damageSkillDict.Add("Title", "Damage");
        damageSkillDict.Add("Info", "+12%");
        damageSkillDict.Add("Costs", 1);
        damageSkillDict.Add("Level", 0);
        damageSkillDict.Add("Cap", 9);
        
        defenseSkillDict.Add("Title", "Defense");
        defenseSkillDict.Add("Info", "+16%");
        defenseSkillDict.Add("Costs", 1);
        defenseSkillDict.Add("Level", 0);
        defenseSkillDict.Add("Cap", 9);
        
        critSkillDict.Add("Title", "Critical Hit");
        critSkillDict.Add("Info", "+5%");
        critSkillDict.Add("Costs", 1);
        critSkillDict.Add("Level", 0);
        critSkillDict.Add("Cap", 9);
        
        // Tier TWO skills and stats
        ASPDSkillDict.Add("Title", "Attack Speed");
        ASPDSkillDict.Add("Info", "+8%");
        ASPDSkillDict.Add("Costs", 1);
        ASPDSkillDict.Add("Level", 0);
        ASPDSkillDict.Add("Cap", 9);

        MSPDSkillDict.Add("Title", "Move Speed");
        MSPDSkillDict.Add("Info", "+5%");
        MSPDSkillDict.Add("Costs", 1);
        MSPDSkillDict.Add("Level", 0);
        MSPDSkillDict.Add("Cap", 9);
        
        // Tier THREE skills and stats
        classSkillDict.Add("Title", newSkillTitle);
        classSkillDict.Add("Info", "???");
        classSkillDict.Add("Costs", 3);
        classSkillDict.Add("Level", 0);
        classSkillDict.Add("Cap", 5);

        passiveSkillDict.Add("Title", newPassiveTitle);
        passiveSkillDict.Add("Info", newPassiveInfo);
        passiveSkillDict.Add("Costs", 3);
        passiveSkillDict.Add("Level", 0);
        passiveSkillDict.Add("Cap", 1);

        // Add dictionaries to a data list        
        skillData.Add(healthSkillDict);
        skillData.Add(damageSkillDict);
        skillData.Add(defenseSkillDict);
        skillData.Add(critSkillDict);
        skillData.Add(ASPDSkillDict);
        skillData.Add(MSPDSkillDict);
        skillData.Add(classSkillDict);
        skillData.Add(passiveSkillDict);

        // Set all basic skills
        for (int i = 0; i < basicSkillsCount; i++) {
            allAvailableBasicsArr.Add(i);
        }

        // Set all special skills
        for (int m = basicSkillsCount; m < basicSkillsCount + specialSkillsCount; m++) {
            allAvailableSpecialsArr.Add(m);
        }
    }


    private void OnEnable() {
        currentIndex = 1;

        if (!cardsAlreadyDrawn) {
            ResetCardsPos();
            DrawCards();
        }
        CheckForLiquidity();
        DisplayCards();
        DisplayCursor();
        UpdateOrbCount();

        if (!initialDrawAnimation) {
            initialDrawAnimation = true;

            for (int p = 0; p < maxCardDraw; p++) {
                StartCoroutine(AnimateCards(p));
                StartCoroutine(DrawCardSound(p));
            }
        }
    }


    private void Update() {
        if (this.gameObject.transform.parent.GetComponent<CharacterMovement>().skillBoardOn) {
            GetInput();
        }
    }


    private void GetInput() {
        // DPAD navigation
        if (currentIndex < maxCardDraw - 1) {
            // DPAD Right
            if (ReInput.players.GetPlayer(charID).GetButtonDown("DPadRight")) {
                currentIndex++;
                DisplayCursor();
            }
            // Analog Stick Right
            if (ReInput.players.GetPlayer(charID).GetAxis("LS Horizontal") > maxThreshold && !axisXActive) {
                axisXActive = true;
                currentIndex++;
                DisplayCursor();
            }
        }
        if (currentIndex > 0) {
            // DPAD Left
            if (ReInput.players.GetPlayer(charID).GetButtonDown("DPadLeft")) {
                currentIndex--;
                DisplayCursor();
            }
            // Analog Stick Left
            if (ReInput.players.GetPlayer(charID).GetAxis("LS Horizontal") < -maxThreshold && !axisXActive) {
                axisXActive = true;
                currentIndex--;
                DisplayCursor();
            }
        }        

        // Reset analog stick navigation
        if (ReInput.players.GetPlayer(charID).GetAxis("LS Horizontal") <= minThreshold && ReInput.players.GetPlayer(charID).GetAxis("LS Horizontal") >= -minThreshold) {
            axisXActive = false;
        }

        // Purchase skill
        if (canPickCard) {
            if (ReInput.players.GetPlayer(charID).GetButtonDown("X")) {
                int skillArrIndex = drawnSkillsArr[currentIndex];
                if (characterSheetScript.currentOrbs >= (int)skillData[skillArrIndex]["Costs"]) {
                    PurchaseSkill();
                } else {
                    // print("not enough orbs");
                    Instantiate(skillLockedSound);
                }
            }
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private IEnumerator CardHighlightDelay() {
        yield return new WaitForSeconds(0.4f);

        ResetCardsPos();
        DrawCards();
        CheckForLiquidity();
        DisplayCards();

        for (int p = 0; p < maxCardDraw; p++) {
            StartCoroutine(AnimateCards(p));
            StartCoroutine(DrawCardSound(p));
        }
    }


    // Set all cards outside the viewport
    private void ResetCardsPos() {
        for (int s = 0; s < maxCardDraw; s++) {
            // Disable outline from selected Card
            skillCardsArray[s].GetComponent<Outline>().enabled = false;

            skillCardsArray[s].transform.localPosition = new Vector3(
                skillCardsArray[s].transform.localPosition.x,
                1500.0f + s * 1000.0f,
                skillCardsArray[s].transform.localPosition.z
            );
        }
    }


    private void DrawCards() {
        // Cards have been drawn and need to be purchased before new cards will be drawn
        cardsAlreadyDrawn = true;
        specialCardDrawn = false;

        // Clear basics array and drawn cards array and add skills from overall basics array
        currentBasicsArr.Clear();
        currentBasicsArr.AddRange(allAvailableBasicsArr);

        currentSpecialsArr.Clear();
        currentSpecialsArr.AddRange(allAvailableSpecialsArr);

        drawnSkillsArr.Clear();

        // Draw random number, add to drawn cards and remove that number from the basics/specials array
        for (int j = 0; j < maxCardDraw; j++) {
            // Draw a random number to see if a special skill will be drawn
            int specialSkillChance = Random.Range(0, 100);

            int drawnSkill = -1;

            if (specialSkillChance < currentSpecialSkillChance && !specialCardDrawn && currentSpecialsArr.Count > 0) {
                specialCardDrawn = true;
                int randomSkill = Random.Range(0, currentSpecialsArr.Count);
                drawnSkill = currentSpecialsArr[randomSkill];
            } else if (specialSkillChance >= currentSpecialSkillChance || specialCardDrawn || currentSpecialsArr.Count <= 0) {
                int randomSkill = Random.Range(0, currentBasicsArr.Count);
                drawnSkill = currentBasicsArr[randomSkill];
            }

            drawnSkillsArr.Add(drawnSkill);
            currentBasicsArr.Remove(drawnSkill);
        }
    }


    private void DisplayCards() {
        for (int l = 0; l < drawnSkillsArr.Count; l++) {
            int skillArrIndex = drawnSkillsArr[l];

            string skillInfoText = "";

            switch (skillArrIndex) {
                // HEALTH
                case 0:
                    skillInfoText =
                    characterSheetScript.maxHealth.ToString("F0") +
                    " → " +
                    (characterSheetScript.maxHealth + (characterSheetScript.maxHealth * SkillsHandler.increaseHP)).ToString("F0");
                    break;
                // DAMAGE
                case 1:
                    float averageDmg = (characterSheetScript.attackOneDmg + characterSheetScript.attackTwoDmg) / 2;
                    skillInfoText =
                    averageDmg.ToString("F0") +
                    " → " +
                    (averageDmg + (averageDmg * SkillsHandler.increaseDMG)).ToString("F0");
                    break;
                // DEFENSE
                case 2:
                    skillInfoText = 
                    characterSheetScript.charDefense.ToString("F0") +
                    " → " +
                    (characterSheetScript.charDefense + (characterSheetScript.charDefense * SkillsHandler.increaseDEF)).ToString("F0");
                    break;
                // CRITICAL HIT CHANCE
                case 3:
                    skillInfoText = 
                    characterSheetScript.critChance.ToString("F0") + "%" +
                    " → " +
                    (characterSheetScript.critChance + SkillsHandler.increaseCRT).ToString("F0") + "%";
                    break;
                // ATTACK SPEED
                case 4:
                // WEISS IMMER NOCH NICH WIE ICH DEN SCHEISS DARSTELLEN SOLL..
                    skillInfoText = "+10%";
                    break;
                // MOVE SPEED
                case 5:
                    skillInfoText = 
                    characterSheetScript.moveSpeed.ToString("F1") +
                    " → " +
                    (characterSheetScript.moveSpeed + (characterSheetScript.moveSpeed * SkillsHandler.increaseMSPD)).ToString("F1");
                    break;
                // CLASS SKILL
                case 6:
                    if ((int)skillData[skillArrIndex]["Level"] == 0) {
                        skillInfoText = newEnableInfo;
                    } else {
                        skillInfoText = newImproveInfo;
                    }
                    break;
                // PASSIVE SKILL
                case 7:
                    skillInfoText = (string)skillData[skillArrIndex]["Info"];
                    break;
            }
            
            skillCardsArray[l].transform.GetChild(1).GetComponent<Text>().text = (string)skillData[skillArrIndex]["Title"];
            skillCardsArray[l].transform.GetChild(2).GetComponent<Text>().text = skillInfoText;
            skillCardsArray[l].transform.GetChild(3).GetComponent<Text>().text = (int)skillData[skillArrIndex]["Costs"] + "";
            skillCardsArray[l].transform.GetChild(5).GetComponent<Image>().sprite = skillIconsArray[skillArrIndex];

            DisplayLevel(l);

            if (drawnSkillsArr[l] < basicSkillsCount) {
                skillCardsArray[l].GetComponent<Image>().color = Colors.blue20;
                skillCardsArray[l].transform.GetChild(1).GetComponent<Text>().color = Colors.keyOrange30;
            } else {
                skillCardsArray[l].GetComponent<Image>().color = Colors.keyGold30;
                skillCardsArray[l].transform.GetChild(1).GetComponent<Text>().color = Colors.white50;
            }
        }
    }


    private void CheckForLiquidity() {
        for (int r = 0; r < drawnSkillsArr.Count; r++) {
            int skillArrIndex = drawnSkillsArr[r];

            if (characterSheetScript.currentOrbs >= (int)skillData[skillArrIndex]["Costs"]) {
                skillCardsArray[r].transform.GetChild(7).GetComponent<Image>().color = new Color32(255, 255, 255, 0);
            } else {
                skillCardsArray[r].transform.GetChild(7).GetComponent<Image>().color = new Color32(255, 255, 255, 180);
            }
        }
    }


    private void DisplayLevel(int cardIndex) {
        GameObject levelDisplayParent = skillCardsArray[cardIndex].transform.GetChild(6).gameObject;
        int currentCount = 0;

        foreach (Transform child in levelDisplayParent.transform) {
            Destroy(child.gameObject);
        }

        if (drawnSkillsArr[cardIndex] < basicSkillsCount) {
            minLevel = 0;
            maxLevel = 9;
        } else if (drawnSkillsArr[cardIndex] == basicSkillsCount) {
            minLevel = 2;
            maxLevel = 7;
        } else if (drawnSkillsArr[cardIndex] > basicSkillsCount) {
            minLevel = 4;
            maxLevel = 5;
        }

        for (int n = minLevel; n < maxLevel; n++) {
            GameObject newLevelDisplayer = Instantiate(skillLevelDisplayer);
            newLevelDisplayer.transform.SetParent(levelDisplayParent.transform);

            newLevelDisplayer.transform.localPosition = new Vector3(-104 + levelDisplayerDistance * n, 0, 0);
            newLevelDisplayer.transform.localRotation = Quaternion.Euler(0, 0, 0);
            newLevelDisplayer.transform.localScale = new Vector3(1, 1, 1);

            // HIGHLIGHT LevelDisplayer
            if (currentCount == (int)skillData[drawnSkillsArr[cardIndex]]["Level"]) {
                if (drawnSkillsArr[cardIndex] < basicSkillsCount) {
                    newLevelDisplayer.GetComponent<Image>().color = new Color32(Colors.blue50.r, Colors.blue50.g, Colors.blue50.b, 100);
                } else {
                    newLevelDisplayer.GetComponent<Image>().color = new Color32(Colors.keyGold60.r, Colors.keyGold60.g, Colors.keyGold60.b, 100);
                }
            }

            // DEACTIVATE LevelDisplayer
            if (currentCount > (int)skillData[drawnSkillsArr[cardIndex]]["Level"]) {
                newLevelDisplayer.GetComponent<Image>().color = Colors.black40p;
            }
            
            // ACTIVATE LevelDisplayer
            if (currentCount < (int)skillData[drawnSkillsArr[cardIndex]]["Level"]) {
                if (drawnSkillsArr[cardIndex] < basicSkillsCount) {
                    newLevelDisplayer.GetComponent<Image>().color = Colors.blue50;
                } else {
                    newLevelDisplayer.GetComponent<Image>().color = Colors.keyGold60;
                }
            }

            currentCount++;
        }
    }


    private void DisplayCursor() {
        // cursorImage.transform.localPosition = skillCardsArray[currentIndex].transform.localPosition + new Vector3(0, -268, 0);
        cursorImage.transform.localPosition = new Vector3(
            skillCardsArray[currentIndex].transform.localPosition.x,
            -268,
            skillCardsArray[currentIndex].transform.localPosition.z
        );
        Instantiate(cursorMoveSound);
    }


    private void UpdateOrbCount() {
        currentOrbCount.text = characterSheetScript.currentOrbs + "";
    }


    private void PurchaseSkill() {
        characterMovementScript.toggleSkillsDisabled = true;

        PlayActivationSounds();
        PayWithOrbs();
        UpdateOrbCount();
        skillsHandlerScript.ActivateSkill(drawnSkillsArr[currentIndex], (int)skillData[drawnSkillsArr[currentIndex]]["Level"]);
        IncreaseSkillLevel();
        IncreaseSpecialSkillChance();

        StartCoroutine(CardHighlightDelay());
    }


    private void PlayActivationSounds() {
        int skillArrIndex = drawnSkillsArr[currentIndex];
        if ((int)skillData[skillArrIndex]["Level"] < (int)skillData[skillArrIndex]["Cap"] - 1) {
            Instantiate(activateSkillSound);
        } else {
            Instantiate(skillCompleteSound);
        }

        // Enable outline of selected Card
        skillCardsArray[currentIndex].GetComponent<Outline>().enabled = true;
    }


    private void PayWithOrbs() {
        int skillArrIndex = drawnSkillsArr[currentIndex];
        characterSheetScript.currentOrbs -= (int)skillData[skillArrIndex]["Costs"];

        cardsAlreadyDrawn = false;
    }


    private void IncreaseSkillLevel() {
        int skillArrIndex = drawnSkillsArr[currentIndex];
        skillData[skillArrIndex]["Level"] = (int)skillData[skillArrIndex]["Level"] + 1;
        
        // Remove the skill from the overall array when it reaches its level max
        if ((int)skillData[skillArrIndex]["Level"] >= (int)skillData[skillArrIndex]["Cap"]) {
            if (skillArrIndex < basicSkillsCount) {
                allAvailableBasicsArr.Remove(skillArrIndex);
            } else {
                allAvailableSpecialsArr.Remove(skillArrIndex);
            }
        }
    }


    private void IncreaseSpecialSkillChance() {
        if (currentSpecialSkillChance < maxSpecialSkillChance) {
            currentSpecialSkillChance += specialSkillChanceIncrease;

            // Limit the current special skill chance if it's bigger than the max
            if (currentSpecialSkillChance > maxSpecialSkillChance) {
                currentSpecialSkillChance = maxSpecialSkillChance;
            }
        }
    }


    private IEnumerator AnimateCards(int p) {

        canPickCard = false;

        // Reset all position values
        float desiredPos = 0.0f;
        float smoothedPos = 0.0f;
        t = 0;

        // LERP ANIMATION
        while (t < (moveInDuration + p * 1.0f)) {
            t += Time.deltaTime;
            smoothedPos = Mathf.Lerp(skillCardsArray[p].transform.localPosition.y, desiredPos, t / (moveInDuration + p * 0.2f));
            skillCardsArray[p].transform.localPosition = new Vector3(
                skillCardsArray[p].transform.localPosition.x,
                smoothedPos,
                skillCardsArray[p].transform.localPosition.z
            );

            yield return null;
        }

        StopAllCoroutines();
        canPickCard = true;
        characterMovementScript.toggleSkillsDisabled = false;

        // Set all cards to their destined position
        for (int q = 0; q < maxCardDraw; q++) {
            skillCardsArray[q].transform.localPosition = new Vector3(skillCardsArray[q].transform.localPosition.x, 0, skillCardsArray[p].transform.localPosition.z);
        }

    }

    private IEnumerator DrawCardSound(int p) {
        yield return new WaitForSeconds(p * 0.1f);
        Instantiate(drawCardsSound);
    }

}