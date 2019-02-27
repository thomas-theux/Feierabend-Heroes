using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHandler : MonoBehaviour {

	// private CharacterSheet characterSheetScript;

	// private float charHealth;
	// private float charDamage;
	// private float charDefense;

	// private float increaseHP = 0.1f;
	// private float increaseDMG = 0.20f;
	// private float increaseDEF = 8.0f;
	// private float increaseMSPD = 0.1f;
	// private float increaseASPD = 0.1f;
	// private int increaseDodge = 6;
	// private int setCrit = 5;
	// private int increaseCrit = 3;
	// private int critMax = 3;
	// private float increaseAppleHeal = 0.1f;
	// private int increaseDoubleOrbChance = 20;
	// private int increaseRespawnChance = 2;

	// public string[] skillTitles = {
	// 	"HEALTH",
	// 	"DAMAGE",
	// 	"DEFENSE",

	// 	"???",
	// 	"???",
	// 	"???",

	// 	"MOVE SPEED",
	// 	"JUMP",
		
	// 	"UPGRADE",
		
	// 	"ATTACK SPEED",
	// 	"DOUBLE JUMP",
		
	// 	"ENABLE CRITICAL HITS",
	// 	"ENABLE DODGING",
	// 	"ENABLE RESPAWNING",
		
	// 	"LEARN SKILL X",
	// 	"CLASS",
	// 	"LEARN SKILL Y",
		
	// 	"ENABLE DOUBLE ORBS",
	// 	"ENABLE APPLE FINDING",
		
	// 	"IMPROVE CRITICAL HITS",
	// 	"IMPROVE DODGING",
	// 	"IMPROVE RESPAWNING",
		
	// 	"IMPROVE SKILL X",
	// 	"ENABLE RAGE MODE",
	// 	"IMPROVE SKILL Y",
		
	// 	"DOUBLE ORB CHANCE",
	// 	"APPLE HEALING",
		
	// 	"PERFECT CRITICAL HITS",
	// 	"PERFECT DODGING",
	// 	"PERFECT RESPAWNING",
		
	// 	"IMPROVE RAGE",
		
	// 	"PERFECT ORB FINDING",
	// 	"PERFECT APPLE HEALING",
	// };

	// public string[] skillTexts = {
	// 	"Increases your maximum health.",
	// 	"Increases your weapons damage.",
	// 	"Increases your Defense.",
		
	// 	"???",
	// 	"???",
	// 	"???",
		
	// 	"Increases your movement speed.",
	// 	"Your character learns to jump.",
		
	// 	"???",
		
	// 	"Increases your weapons speed.",
	// 	"Your character learns how to double jump.",
		
	// 	"Your character learns to perform Critical Hits (200% damage).",
	// 	"Your character learns how to dodge atacks.",
	// 	"Your character learns how to respawn after death.",
		
	// 	"???",
	// 	"???",
	// 	"???",
		
	// 	"Enables the chance to get 2 orbs from chests instead of 1.",
	// 	"Your character learns how to find Apples (Apples heal 20%).",
		
	// 	"Increases your critical hit chance.",
	// 	"Increases the chance to dodge attacks.",
	// 	"Increases the chance to respawn after death.",
		
	// 	"???",
	// 	"Your character goes into Rage Mode when health is below 20%.",
	// 	"???",
		
	// 	"Increases the chance to get 2 orbs from chests.",
	// 	"Increases the amount your character gets healed from Apples.",
		
	// 	"Every Critical Hit deals more damage.",
	// 	"Everytime your character dodges an attack they get healed.",
	// 	"Every Respawn gives your character one additional Orb.",
		
	// 	"Improves your characters Rage Mode by one level.",
		
	// 	"Enables the chance to get 3 orbs from chests instead of 2.",
	// 	"Your character learns how to heal automatically over time."
	// };

	// public string[] skillPerks = {
	// 	"HP +10%",
	// 	"Damage +20%",
	// 	"Defense +8",
		
	// 	"???",
	// 	"???",
	// 	"???",
		
	// 	"Move Speed +10%",
	// 	"Enable Jumping",
		
	// 	"???",
		
	// 	"Attack Speed +10%",
	// 	"Enable Double Jump",
		
	// 	"Enables Critical Hits (5% chance)",
	// 	"Enables Dodging (6% chance)",
	// 	"Enables Respawning (2% chance)",
		
	// 	"???",
	// 	"???",
	// 	"???",
		
	// 	"Enables Double Orb Chests (20% chance)",
	// 	"Enables Apple Finding",
		
	// 	"Crit Chance +3%",
	// 	"Dodge Chance +6%",
	// 	"Respawn Chance +2%",
		
	// 	"???",
	// 	"Defense x2.0",
	// 	"???",
		
	// 	"Double Orb Chance +20%",
	// 	"Apples heal +10%",
		
	// 	"Crit Damage 300%",
	// 	"Dodging heals 20%",
	// 	"1 Orb per Respawn",
		
	// 	"One additional perk",
		
	// 	"3 Orbs from Chests",
	// 	"Auto heal"
	// };


	// private void Awake() {
	// 	characterSheetScript = this.gameObject.transform.parent.GetComponent<CharacterSheet>();

	// 	// Getting class type and description from character sheet
	// 	skillTitles[15] = characterSheetScript.classType;
	// 	skillTexts[15] = characterSheetScript.classText;
	// 	skillPerks[15] = characterSheetScript.classPerk;

	// 	// Setting the skill title, text and perks for Skill One
	// 	skillTitles[14] = characterSheetScript.skillOneTitle;
	// 	skillTitles[22] = characterSheetScript.skillOneTitle;

	// 	skillTexts[14] = characterSheetScript.skillOneText;
	// 	skillTexts[22] = characterSheetScript.skillOneUpgradeText;

	// 	skillPerks[14] = characterSheetScript.skillOnePerk;
	// 	skillPerks[22] = characterSheetScript.skillOneUpgradePerk;

	// 	// Setting the skill title, text and perks for Skill Two
	// 	skillTitles[16] = characterSheetScript.skillTwoTitle;
	// 	skillTitles[24] = characterSheetScript.skillTwoTitle;

	// 	skillTexts[16] = characterSheetScript.skillTwoText;
	// 	skillTexts[24] = characterSheetScript.skillTwoUpgradeText;

	// 	skillPerks[16] = characterSheetScript.skillTwoPerk;
	// 	skillPerks[24] = characterSheetScript.skillTwoUpgradePerk;
	// }


	// public void ActivateSkill(List<int> skillUpgradeCurrent, int currentIndex) {

	// 	switch(currentIndex) {

	// 		// Skill 00 – HP
	// 		case 0:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					characterSheetScript.maxHealth += characterSheetScript.maxHealth * increaseHP;
	// 					characterSheetScript.currentHealth += characterSheetScript.currentHealth * increaseHP;
	// 					break;
	// 				case 2:
	// 					characterSheetScript.maxHealth += characterSheetScript.maxHealth * increaseHP;
	// 					characterSheetScript.currentHealth += characterSheetScript.currentHealth * increaseHP;
	// 					break;
	// 				case 3:
	// 					characterSheetScript.maxHealth += characterSheetScript.maxHealth * increaseHP;
	// 					characterSheetScript.currentHealth += characterSheetScript.currentHealth * increaseHP;
	// 					break;
	// 				case 4:
	// 					characterSheetScript.maxHealth += characterSheetScript.maxHealth * increaseHP;
	// 					characterSheetScript.currentHealth += characterSheetScript.currentHealth * increaseHP;
	// 					break;
	// 				case 5:
	// 					characterSheetScript.maxHealth += characterSheetScript.maxHealth * increaseHP;
	// 					characterSheetScript.currentHealth += characterSheetScript.currentHealth * increaseHP;
	// 					break;
	// 			}
	// 			break;
			
	// 		// Skill 01 – Damage
	// 		case 01:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					characterSheetScript.attackOneDmg += characterSheetScript.attackOneDmg * increaseDMG;
	// 					characterSheetScript.attackTwoDmg += characterSheetScript.attackTwoDmg * increaseDMG;
	// 					break;
	// 				case 2:
	// 					characterSheetScript.attackOneDmg += characterSheetScript.attackOneDmg * increaseDMG;
	// 					characterSheetScript.attackTwoDmg += characterSheetScript.attackTwoDmg * increaseDMG;
	// 					break;
	// 				case 3:
	// 					characterSheetScript.attackOneDmg += characterSheetScript.attackOneDmg * increaseDMG;
	// 					characterSheetScript.attackTwoDmg += characterSheetScript.attackTwoDmg * increaseDMG;
	// 					break;
	// 				case 4:
	// 					characterSheetScript.attackOneDmg += characterSheetScript.attackOneDmg * increaseDMG;
	// 					characterSheetScript.attackTwoDmg += characterSheetScript.attackTwoDmg * increaseDMG;
	// 					break;
	// 				case 5:
	// 					characterSheetScript.attackOneDmg += characterSheetScript.attackOneDmg * increaseDMG;
	// 					characterSheetScript.attackTwoDmg += characterSheetScript.attackTwoDmg * increaseDMG;
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 02 – Defense
	// 		case 02:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					characterSheetScript.charDefense += increaseDEF;
	// 					break;
	// 				case 2:
	// 					characterSheetScript.charDefense += increaseDEF;
	// 					break;
	// 				case 3:
	// 					characterSheetScript.charDefense += increaseDEF;
	// 					break;
	// 				case 4:
	// 					characterSheetScript.charDefense += increaseDEF;
	// 					break;
	// 				case 5:
	// 					characterSheetScript.charDefense += increaseDEF;
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 03 – Time Upgrade
	// 		case 03:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 04 – Chaos Upgrade
	// 		case 04:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 05 – Venom Upgrade
	// 		case 05:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 06 – Move Speed
	// 		case 06:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					characterSheetScript.moveSpeed += characterSheetScript.moveSpeed * increaseMSPD;
	// 					break;
	// 				case 2:
	// 					characterSheetScript.moveSpeed += characterSheetScript.moveSpeed * increaseMSPD;
	// 					break;
	// 				case 3:
	// 					characterSheetScript.moveSpeed += characterSheetScript.moveSpeed * increaseMSPD;
	// 					break;
	// 				case 4:
	// 					characterSheetScript.moveSpeed += characterSheetScript.moveSpeed * increaseMSPD;
	// 					break;
	// 				case 5:
	// 					characterSheetScript.moveSpeed += characterSheetScript.moveSpeed * increaseMSPD;
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 07 – Single Jump
	// 		case 07:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 08 – Weapon Upgrades
	// 		case 08:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					break;
	// 				case 2:
	// 					break;
	// 				case 3:
	// 					break;
	// 				case 4:
	// 					break;
	// 				case 5:
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 09 – Attack Speed
	// 		case 09:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					characterSheetScript.delayAttackOne -= characterSheetScript.delayAttackOne * increaseASPD;
	// 					characterSheetScript.delayAttackTwo -= characterSheetScript.delayAttackTwo * increaseASPD;
	// 					break;
	// 				case 2:
	// 					characterSheetScript.delayAttackOne -= characterSheetScript.delayAttackOne * increaseASPD;
	// 					characterSheetScript.delayAttackTwo -= characterSheetScript.delayAttackTwo * increaseASPD;
	// 					break;
	// 				case 3:
	// 					characterSheetScript.delayAttackOne -= characterSheetScript.delayAttackOne * increaseASPD;
	// 					characterSheetScript.delayAttackTwo -= characterSheetScript.delayAttackTwo * increaseASPD;
	// 					break;
	// 				case 4:
	// 					characterSheetScript.delayAttackOne -= characterSheetScript.delayAttackOne * increaseASPD;
	// 					characterSheetScript.delayAttackTwo -= characterSheetScript.delayAttackTwo * increaseASPD;
	// 					break;
	// 				case 5:
	// 					characterSheetScript.delayAttackOne -= characterSheetScript.delayAttackOne * increaseASPD;
	// 					characterSheetScript.delayAttackTwo -= characterSheetScript.delayAttackTwo * increaseASPD;
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 10 – Double Jump
	// 		case 10:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 11 – Enable Crit
	// 		case 11:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					characterSheetScript.critChance = setCrit;
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 12 – Enable Dodge
	// 		case 12:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					characterSheetScript.dodgeChance += increaseDodge;
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 13 – Enable Respawn
	// 		case 13:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					characterSheetScript.respawnChance += increaseRespawnChance;
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 14 – Skill One
	// 		case 14:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					// characterSheetScript.skillActivated = 1;
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 15 – Character Class – NOT NEEDED

	// 		// Skill 16 – Skill Two
	// 		case 16:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					// characterSheetScript.skillActivated = 2;
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 17 – Enable Orb Finding
	// 		case 17:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					characterSheetScript.doubleOrbChance += increaseDoubleOrbChance;
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 18 – Enable Apple Finding
	// 		case 18:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					characterSheetScript.canFindApples = true;
	// 					GameObject.Find("PlayerCamera" + transform.parent.GetComponent<CharacterMovement>().playerID).GetComponent<Camera>().cullingMask = ~ (1 << 7);	// Activate APPLES layer in culling mask for the camera
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 19 – Improve Crit
	// 		case 19:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					characterSheetScript.critChance += increaseCrit;
	// 					break;
	// 				case 2:
	// 					characterSheetScript.critChance += increaseCrit;
	// 					break;
	// 				case 3:
	// 					characterSheetScript.critChance += increaseCrit;
	// 					break;
	// 				case 4:
	// 					characterSheetScript.critChance += increaseCrit;
	// 					break;
	// 				case 5:
	// 					characterSheetScript.critChance += increaseCrit;
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 20 – Improve Dodge
	// 		case 20:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					characterSheetScript.dodgeChance += increaseDodge;
	// 					break;
	// 				case 2:
	// 					characterSheetScript.dodgeChance += increaseDodge;
	// 					break;
	// 				case 3:
	// 					characterSheetScript.dodgeChance += increaseDodge;
	// 					break;
	// 				case 4:
	// 					characterSheetScript.dodgeChance += increaseDodge;
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 21 – Improve Respawn
	// 		case 21:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					characterSheetScript.respawnChance += increaseRespawnChance;
	// 					break;
	// 				case 2:
	// 					characterSheetScript.respawnChance += increaseRespawnChance;
	// 					break;
	// 				case 3:
	// 					characterSheetScript.respawnChance += increaseRespawnChance;
	// 					break;
	// 				case 4:
	// 					characterSheetScript.respawnChance += increaseRespawnChance;
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 22 – Improve Skill One
	// 		case 22:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					for (int i = 0; i< 2; i++) { characterSheetScript.skillOneStats[i] += characterSheetScript.skillOneStats[i+2]; }
	// 					break;
	// 				case 2:
	// 					for (int i = 0; i< 2; i++) { characterSheetScript.skillOneStats[i] += characterSheetScript.skillOneStats[i+2]; }
	// 					break;
	// 				case 3:
	// 					for (int i = 0; i< 2; i++) { characterSheetScript.skillOneStats[i] += characterSheetScript.skillOneStats[i+2]; }
	// 					break;
	// 				case 4:
	// 					for (int i = 0; i< 2; i++) { characterSheetScript.skillOneStats[i] += characterSheetScript.skillOneStats[i+2]; }
	// 					break;
	// 				case 5:
	// 					for (int i = 0; i< 2; i++) { characterSheetScript.skillOneStats[i] += characterSheetScript.skillOneStats[i+2]; }
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 23 – Enable Rage Mode
	// 		case 23:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					characterSheetScript.rageSkillActivated = true;
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 24 – Improve Skill Two
	// 		case 24:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					for (int i = 0; i< 2; i++) { characterSheetScript.skillTwoStats[i] += characterSheetScript.skillTwoStats[i+2]; }
	// 					break;
	// 				case 2:
	// 					for (int i = 0; i< 2; i++) { characterSheetScript.skillTwoStats[i] += characterSheetScript.skillTwoStats[i+2]; }
	// 					break;
	// 				case 3:
	// 					for (int i = 0; i< 2; i++) { characterSheetScript.skillTwoStats[i] += characterSheetScript.skillTwoStats[i+2]; }
	// 					break;
	// 				case 4:
	// 					for (int i = 0; i< 2; i++) { characterSheetScript.skillTwoStats[i] += characterSheetScript.skillTwoStats[i+2]; }
	// 					break;
	// 				case 5:
	// 					for (int i = 0; i< 2; i++) { characterSheetScript.skillTwoStats[i] += characterSheetScript.skillTwoStats[i+2]; }
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 25 – Improve Orb Finding
	// 		case 25:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					characterSheetScript.doubleOrbChance += increaseDoubleOrbChance;
	// 					break;
	// 				case 2:
	// 					characterSheetScript.doubleOrbChance += increaseDoubleOrbChance;
	// 					break;
	// 				case 3:
	// 					characterSheetScript.doubleOrbChance += increaseDoubleOrbChance;
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 26 – Improve Apple Finding
	// 		case 26:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					characterSheetScript.healPercentage += increaseAppleHeal;
	// 					break;
	// 				case 2:
	// 					characterSheetScript.healPercentage += increaseAppleHeal;
	// 					break;
	// 				case 3:
	// 					characterSheetScript.healPercentage += increaseAppleHeal;
	// 					break;
	// 				case 4:
	// 					characterSheetScript.healPercentage += increaseAppleHeal;
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 27 – Perfect Crit
	// 		case 27:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					characterSheetScript.critDMG = critMax;
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 28 – Perfect Dodge
	// 		case 28:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					characterSheetScript.dodgeHeal = true;
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 29 – Perfect Respawn
	// 		case 29:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					characterSheetScript.respawnOrb = true;
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 30 – Improve Rage Mode
	// 		case 30:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					characterSheetScript.rageLevel += 1;
	// 					break;
	// 				case 2:
	// 					characterSheetScript.rageLevel += 1;
	// 					break;
	// 				case 3:
	// 					characterSheetScript.rageLevel += 1;
	// 					break;
	// 				case 4:
	// 					characterSheetScript.rageLevel += 1;
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 31 – Perfect Orb Finding
	// 		case 31:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					characterSheetScript.findThreeOrbs = true;
	// 					break;
	// 			}
	// 			break;

	// 		// Skill 32 – Perfect Apple Finding
	// 		case 32:
	// 			switch(skillUpgradeCurrent[currentIndex]) {
	// 				case 1:
	// 					characterSheetScript.selfHealActive = true;
	// 					break;
	// 			}
	// 			break;

	// 	}

	// }

}
