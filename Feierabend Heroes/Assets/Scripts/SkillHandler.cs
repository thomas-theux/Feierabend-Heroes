using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHandler : MonoBehaviour {

	private float charHealth;
	private float charDamage;
	private float charDefense;


	private void Awake() {
	}


	public void ActivateSkill(List<int> skillUpgradeCurrent, int currentIndex) {

		switch(currentIndex) {

			// Skill 00 – HP
			case 0:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("HP +10%");
						this.gameObject.transform.parent.GetComponent<HealthHandler>().healthPoints += this.gameObject.transform.parent.GetComponent<HealthHandler>().healthPoints * 0.1f;
						break;
					case 2:
						print("HP +10%");
						this.gameObject.transform.parent.GetComponent<HealthHandler>().healthPoints += this.gameObject.transform.parent.GetComponent<HealthHandler>().healthPoints * 0.1f;
						break;
					case 3:
						print("HP +10%");
						this.gameObject.transform.parent.GetComponent<HealthHandler>().healthPoints += this.gameObject.transform.parent.GetComponent<HealthHandler>().healthPoints * 0.1f;
						break;
					case 4:
						print("HP +10%");
						this.gameObject.transform.parent.GetComponent<HealthHandler>().healthPoints += this.gameObject.transform.parent.GetComponent<HealthHandler>().healthPoints * 0.1f;
						break;
					case 5:
						print("HP +10%");
						this.gameObject.transform.parent.GetComponent<HealthHandler>().healthPoints += this.gameObject.transform.parent.GetComponent<HealthHandler>().healthPoints * 0.1f;
						break;
				}
				break;
			
			// Skill 01 – Damage
			case 01:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Damage +14%");
						break;
					case 2:
						print("Damage +14%");
						break;
					case 3:
						print("Damage +14%");
						break;
					case 4:
						print("Damage +14%");
						break;
					case 5:
						print("Damage +14%");
						break;
				}
				break;

			// Skill 02 – Defense
			case 02:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Damage +8");
						break;
					case 2:
						print("Damage +8");
						break;
					case 3:
						print("Damage +8");
						break;
					case 4:
						print("Damage +8");
						break;
					case 5:
						print("Damage +8");
						break;
				}
				break;

			// Skill 03 – Time Upgrade
			case 03:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Enable Time");
						break;
				}
				break;

			// Skill 04 – Chaos Upgrade
			case 04:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Enable Chaos");
						break;
				}
				break;

			// Skill 05 – Venom Upgrade
			case 05:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Enable Venom");
						break;
				}
				break;

			// Skill 06 – Move Speed
			case 06:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Move Speed +10%");
						break;
					case 2:
						print("Move Speed +10%");
						break;
					case 3:
						print("Move Speed +10%");
						break;
					case 4:
						print("Move Speed +10%");
						break;
					case 5:
						print("Move Speed +10%");
						break;
				}
				break;

			// Skill 07 – Single Jump
			case 07:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Enable Single Jump");
						break;
				}
				break;

			// Skill 08 – Weapon Upgrades
			case 08:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Weapon Upgrade 1");
						break;
					case 2:
						print("Weapon Upgrade 2");
						break;
					case 3:
						print("Weapon Upgrade 3");
						break;
					case 4:
						print("Weapon Upgrade 4");
						break;
					case 5:
						print("Weapon Upgrade 5");
						break;
				}
				break;

			// Skill 09 – Attack Speed
			case 09:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Attack Speed +10%");
						break;
					case 2:
						print("Attack Speed +10%");
						break;
					case 3:
						print("Attack Speed +10%");
						break;
					case 4:
						print("Attack Speed +10%");
						break;
					case 5:
						print("Attack Speed +10%");
						break;
				}
				break;

			// Skill 10 – Double Jump
			case 10:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Enable Double Jump");
						break;
				}
				break;

			// Skill 11 – Enable Crit
			case 11:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Enable Crit");
						break;
				}
				break;

			// Skill 12 – Enable Dodge
			case 12:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Enable Dodge");
						break;
				}
				break;

			// Skill 13 – Enable Respawn
			case 13:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Enable Respawn");
						break;
				}
				break;

			// Skill 14 – Skill One
			case 14:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Enable Skill One");
						break;
				}
				break;

			// Skill 15 – Character Class – NOT NEEDED

			// Skill 16 – Skill Two
			case 16:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Enable Skill Two");
						break;
				}
				break;

			// Skill 17 – Enable Orb Finding
			case 17:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Enable Orb Finding");
						break;
				}
				break;

			// Skill 18 – Enable Apple Finding
			case 18:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Enable Apple Finding");
						break;
				}
				break;

			// Skill 19 – Improve Crit
			case 19:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Crit +3%");
						break;
					case 2:
						print("Crit +3%");
						break;
					case 3:
						print("Crit +3%");
						break;
					case 4:
						print("Crit +3%");
						break;
					case 5:
						print("Crit +3%");
						break;
				}
				break;

			// Skill 20 – Improve Dodge
			case 20:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Dodge +6%");
						break;
					case 2:
						print("Dodge +6%");
						break;
					case 3:
						print("Dodge +6%");
						break;
					case 4:
						print("Dodge +6%");
						break;
				}
				break;

			// Skill 21 – Improve Respawn
			case 21:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Respawn +2%");
						break;
					case 2:
						print("Respawn +2%");
						break;
					case 3:
						print("Respawn +2%");
						break;
					case 4:
						print("Respawn +2%");
						break;
				}
				break;

			// Skill 22 – Improve Skill One
			case 22:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Skill One 1");
						break;
					case 2:
						print("Skill One 2");
						break;
					case 3:
						print("Skill One 3");
						break;
					case 4:
						print("Skill One 4");
						break;
					case 5:
						print("Skill One 5");
						break;
				}
				break;

			// Skill 23 – Enable Rage Mode
			case 23:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Enable Rage Mode");
						break;
				}
				break;

			// Skill 24 – Improve Skill Two
			case 24:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Skill Two 1");
						break;
					case 2:
						print("Skill Two 2");
						break;
					case 3:
						print("Skill Two 3");
						break;
					case 4:
						print("Skill Two 4");
						break;
					case 5:
						print("Skill Two 5");
						break;
				}
				break;

			// Skill 25 – Improve Orb Finding
			case 25:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Orb +20%");
						break;
					case 2:
						print("Orb +20%");
						break;
					case 3:
						print("Orb +20%");
						break;
				}
				break;

			// Skill 26 – Improve Apple Finding
			case 26:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Apple +50%");
						break;
					case 2:
						print("Apple +50%");
						break;
					case 3:
						print("Apple +50%");
						break;
					case 4:
						print("Apple +50%");
						break;
				}
				break;

			// Skill 27 – Perfect Crit
			case 27:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Perfect Crit");
						break;
				}
				break;

			// Skill 28 – Perfect Dodge
			case 28:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Perfect Dodge");
						break;
				}
				break;

			// Skill 29 – Perfect Respawn
			case 29:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Perfect Respawn");
						break;
				}
				break;

			// Skill 30 – Improve Rage Mode
			case 30:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Rage 1");
						break;
					case 2:
						print("Rage 2");
						break;
					case 3:
						print("Rage 3");
						break;
					case 4:
						print("Rage 4");
						break;
				}
				break;

			// Skill 31 – Perfect Orb Finding
			case 31:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Perfect Orb Finding");
						break;
				}
				break;

			// Skill 32 – Perfect Apple Finding
			case 32:
				switch(skillUpgradeCurrent[currentIndex]) {
					case 1:
						print("Perfect Apple Finding");
						break;
				}
				break;

		}

	}

}
