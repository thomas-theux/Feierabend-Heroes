using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class SkillBoardNav : MonoBehaviour {

    private int charID = 0;
    private int currentIndex = 0;

    public List<GameObject> skillArray;
    public Image cursorImage;

	private float minThreshold = 0.5f;
	private float maxThreshold = 0.5f;
	private bool axisXActive;
	private bool axisYActive;

    private List<Hashtable> skillData = new List<Hashtable>();
    private Hashtable healthSkillDic = new Hashtable();
    private Hashtable damageSkillDic = new Hashtable();


    private void Awake() {
        // charID = this.gameObject.transform.parent.GetComponent<CharacterMovement>().playerID;

        healthSkillDic.Add("Title", "Health");
        healthSkillDic.Add("Text", "+10%");
        healthSkillDic.Add("Cost", new int[] {1, 1, 2, 2, 3});
        healthSkillDic.Add("Cap", 5);

        damageSkillDic.Add("Title", "Damage");
        damageSkillDic.Add("Text", "+14");
        damageSkillDic.Add("Cost", new int[] {1, 1, 2, 2, 3});
        damageSkillDic.Add("Cap", 5);

        skillData.Add(healthSkillDic);
        skillData.Add(damageSkillDic);

        // print((string)skillData[0]["Title"]);
        // print((string)skillData[0]["Text"]);
        // print((int)skillData[0]["Cost"]);
        // print((int)skillData[0]["Cap"]);

    }


    private void Start() {
        print((int)healthSkillDic["Cap"]);
    }


    private void Update() {
        GetInput();
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
	}


    private void DisplayCursor() {
        cursorImage.transform.position = skillArray[currentIndex].transform.position;
    }

}
