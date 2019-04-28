using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlock : MonoBehaviour {

	public float casterDamage = 0;
	public float casterCritChance = 0;
	public float casterCritDMG = 0;
	public int damagerID;

	public GameObject fireBlockActiveSound;

	private void Awake() {
		Destroy(gameObject, 7.0f);
		Instantiate(fireBlockActiveSound);
	}


	private void OnTriggerEnter(Collider other) {
		if (other.transform.childCount > 0) {
			if (other.tag == "Attack" && other.transform.GetChild(0).tag != this.gameObject.transform.GetChild(0).tag) {
				Destroy(other.gameObject);
			}
		}
	}


	private void OnTriggerStay(Collider other) {
		if (other.transform.childCount > 0) {
			if (other.tag != "Attack" && other.tag != "Apple" && other.tag != "Orb" && other.tag != "Environment" && other.transform.GetChild(0).tag != transform.GetChild(0).tag) {
				CalculateDamage(other);
			}
		}
	}


	private void CalculateDamage(Collider other) {
		CharacterSheet characterSheetScript = other.gameObject.GetComponent<CharacterSheet>();

		float dealDamage = 0;
		float enemyDefense = characterSheetScript.charDefense;
		int enemyDodge = characterSheetScript.dodgeChance;

		int dodgeChance = Random.Range(1, 101);
		int critChance = Random.Range(1, 101);

		// Adding a bonus to attack and defense depending on the player's ranking
		int bonusIndexSelf = 0;
		int bonusIndexEnemy = 0;
		float attackBonus = 0;
		float defenseBonus = 0;

		if (SettingsHolder.playedFirstRound) {
			bonusIndexSelf = GameManager.rankingsArr.IndexOf(damagerID);
			bonusIndexEnemy = GameManager.rankingsArr.IndexOf(characterSheetScript.charID);

			attackBonus = casterDamage * SettingsHolder.rankingBonus[bonusIndexSelf];
			defenseBonus = enemyDefense * SettingsHolder.rankingBonus[bonusIndexEnemy];
		}

		// Check if enemy dodges attack
		if (dodgeChance > enemyDodge) {
			// dealDamage = casterDamage - ((enemyDefense / 100) * casterDamage);	// my old formula – heals enemy if def > 100
			// dealDamage = casterDamage * casterDamage / (casterDamage + enemyDefense);
			dealDamage = (casterDamage + attackBonus) * (100 / (100 + enemyDefense + defenseBonus));

			// If character lands a critical strike then multiply damage
			if (casterCritChance <= critChance) {
				dealDamage *= casterCritDMG;
			}

			characterSheetScript.currentHealth -= dealDamage;
			other.gameObject.GetComponent<LifeDeathHandler>().lastDamagerID = damagerID;
			other.GetComponent<LifeDeathHandler>().gotHit = true;
		}
	}

}
