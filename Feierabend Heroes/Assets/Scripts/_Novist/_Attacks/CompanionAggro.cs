using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionAggro : MonoBehaviour {

	// private float speed = 2.0f;
	public GameObject followCaster;
	private Transform enemyChar;
	private float followSpeed = 3.0f;
	private float approachEnemySpeed = 3.0f;

	// private float lifeTime = 8.0f;

	private Vector3 offset;

	private bool enemyDetected = false;
	private float distanceToChar = 1.4f;
	private float distanceToEnemy = 2.0f;
	private float distanceLimit = 30.0f;

	private bool isAttacking = false;
	private float attackDelayDefault = 0.5f;
	private float attackDelayTimer;

	public float casterDamage = 0;
	public float casterCritChance = 0;
	public float casterCritDMG = 0;
	public int damagerID;


	public void SetLifeTime(float lifeTime) {
		Destroy(transform.parent.gameObject, lifeTime);
	}


	private void OnTriggerEnter(Collider other) {
		if (other.tag != "Attack" && other.tag != "Apple" && other.tag != "Orb" && other.tag != "Environment" && other.tag != transform.parent.GetChild(0).tag) {
			enemyDetected = true;
			enemyChar = other.gameObject.transform;
			// StartCoroutine(FollowTimer());
			StartCoroutine(DistanceTooBig());
		}
	}


	private void OnTriggerExit(Collider other) {
		if (other.tag != "Attack" && other.tag != "Apple" && other.tag != "Orb" && other.tag != "Environment" && other.tag != transform.parent.GetChild(0).tag) {
			enemyDetected = false;
		}
	}


	private void FixedUpdate() {
		if (!enemyDetected) {
			if (Vector3.Distance(followCaster.transform.position, transform.parent.position) > distanceToChar) {
				Vector3 desiredPos = new Vector3(followCaster.transform.localPosition.x, followCaster.transform.localPosition.y, followCaster.transform.localPosition.z);
				Vector3 smoothedPos = Vector3.Lerp(transform.parent.position, desiredPos, followSpeed * Time.deltaTime);
				
				transform.parent.position = smoothedPos;
			}
		} else {
			// Approach enemy until its close enough
			if (Vector3.Distance(enemyChar.position, transform.parent.position) > distanceToEnemy) {
				Vector3 desiredPos = new Vector3(enemyChar.localPosition.x, enemyChar.localPosition.y, enemyChar.localPosition.z);
				Vector3 smoothedPos = Vector3.Lerp(transform.parent.position, desiredPos, approachEnemySpeed * Time.deltaTime);
				
				transform.parent.position = smoothedPos;
			} else {
				// Attack enemy when close enough
				AttackEnemy();
			}
		}
	}


	private void AttackEnemy() {
		if (!isAttacking) {
			attackDelayTimer = attackDelayDefault;
			CalculateDamage();

			int charID = enemyChar.GetComponent<CharacterMovement>().playerID;
			GameObject.Find("PlayerCamera" + charID).GetComponent<CameraShake>().enabled = true;
		}

		if (attackDelayTimer > 0) {
			isAttacking = true;

			attackDelayTimer -= Time.deltaTime;
			if (attackDelayTimer <= 0) {
				isAttacking = false;
			}
		}
	}


	private void CalculateDamage() {
		CharacterSheet characterSheetScript = enemyChar.gameObject.GetComponent<CharacterSheet>();

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
			// dealDamage = casterDamage - ((enemyDefense / 100) * casterDamage);	// my old formula – heals player if def > 100
			// dealDamage = casterDamage * casterDamage / (casterDamage + enemyDefense);
			dealDamage = (casterDamage + attackBonus) * (100 / (100 + enemyDefense + defenseBonus));

			// If character lands a critical strike then multiply damage
			if (casterCritChance <= critChance) {
				dealDamage *= casterCritDMG;
			}

			characterSheetScript.currentHealth -= dealDamage;
			enemyChar.gameObject.GetComponent<LifeDeathHandler>().lastDamagerID = damagerID;
			enemyChar.GetComponent<LifeDeathHandler>().gotHit = true;
		}
	}


	private IEnumerator DistanceTooBig() {
		while (Vector3.Distance(transform.parent.position, followCaster.transform.position) <= distanceLimit) {
			yield return null;
		}
		
		enemyDetected = false;
	}


	private IEnumerator FollowTimer() {
		yield return new WaitForSeconds(3.0f);
		enemyDetected = false;
	}

}
