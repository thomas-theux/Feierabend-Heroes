using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsHandler : MonoBehaviour {

    private CharacterSheet characterSheetScript;

    private float increaseHP = 0.15f;
    private float increaseDMG = 0.15f;
    private float increaseDEF = 0.20f;
    private int increaseDDG = 6;
    private int increaseCRT = 6;
    private float increaseASPD = 0.1f;
    private float increaseMSPD = 0.1f;
    private int increaseDBLORB = 15;

    // private float[] clmSkillStats = {0.3f, 20.0f, -0.05f, 4.0f};
    // private float[] novSkillStats = {26.0f, 12.0f, 3.0f, 4.0f};
    // private float[] sowSkillStats = {0.0f, 0.0f, 0.0f, 0.0f};
    // private float[] shsSkillStats = {0.0f, 0.0f, 0.0f, 0.0f};


    public void Awake () {
        characterSheetScript = this.gameObject.transform.parent.GetComponent<CharacterSheet>();
    }


    public void ActivateSkill(int currentIndex, int currentLevel) {
        switch (currentIndex) {

            // Health
            case 0:
				characterSheetScript.currentHealth += characterSheetScript.currentHealth * increaseHP;
                characterSheetScript.maxHealth += characterSheetScript.maxHealth * increaseHP;
                break;

            // Damage
            case 1:
                characterSheetScript.attackOneDmg += characterSheetScript.attackOneDmg * increaseDMG;
                characterSheetScript.attackTwoDmg += characterSheetScript.attackTwoDmg * increaseDMG;
                break;

            // Defense
            case 2:
                characterSheetScript.charDefense += characterSheetScript.charDefense * increaseDEF;
                break;

            // Dodge
            case 3:
                characterSheetScript.dodgeChance += increaseDDG;
                break;

            // Crit
            case 4:
                characterSheetScript.critChance += increaseCRT;
                break;


            // Attack Speed
            case 5:
                characterSheetScript.delayAttackOne -= characterSheetScript.delayAttackOne * increaseASPD;
                characterSheetScript.delayAttackTwo -= characterSheetScript.delayAttackTwo * increaseASPD;
                break;

            // Move Speed
            case 6:
                characterSheetScript.moveSpeed += characterSheetScript.moveSpeed * increaseMSPD;
                break;

            // Double Orb
            case 7:
                characterSheetScript.doubleOrbChance += increaseDBLORB;
                break;


            // Enable / Improve Skill
            case 8:
                if (currentLevel < 0) {
                    // Enable Skill
                    characterSheetScript.skillActivated = true;
                } else {
                    // Improve Skill
                    for (int i = 0; i < 2; i++) { characterSheetScript.charSkillStats[i] += characterSheetScript.charSkillStats[i+2]; }
                }
                break;

            // Passive Skill
            case 9:
                // Passive Skill
                switch (characterSheetScript.charClass) {

                    // Cloud Master
                    case 0:
                        characterSheetScript.selfRepairActive = true;
                        break;

                    // Novist
                    case 1:
                        characterSheetScript.slowingTendrilsActive = true;
                        break;

                    // Seer of War
                    case 2:
                        break;

                    // Shape Shifter
                    case 3:
                        break;

                }
                break;

            // Find Apples
            case 10:
                // Activate APPLES layer in culling mask for the camera
                characterSheetScript.canFindApples = true;
                GameObject.Find("PlayerCamera" + transform.parent.GetComponent<CharacterMovement>().playerID).GetComponent<Camera>().cullingMask = ~ (1 << 7);
                break;
        }
    }
}