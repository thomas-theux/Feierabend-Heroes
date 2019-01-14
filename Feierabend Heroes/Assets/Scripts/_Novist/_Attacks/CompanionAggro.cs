using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionAggro : MonoBehaviour {

	// private float speed = 2.0f;
	public GameObject followCaster;
	private Transform enemyChar;
	private float followSpeed = 3.0f;
	private float approachEnemySpeed = 2.0f;

	public float lifeTime = 8.0f;

	private Vector3 offset;

	private bool enemyDetected = false;
	private float distanceToChar = 1.4f;
	private float distanceToEnemy = 2.0f;

	private bool isAttacking = false;
	private float attackDelayDefault = 0.5f;
	private float attackDelayTimer;

	public float casterDamage = 0;
	public float casterCritChance = 0;
	public float casterCritDMG = 0;


	private void Awake() {
		Destroy(transform.parent.gameObject, lifeTime);
		// StartCoroutine(LifeTime());
	}


	private void OnTriggerEnter(Collider other) {
		if (other.tag != "Attack" && other.tag != "Apple" && other.tag != "Orb" && other.tag != "Environment" && other.tag != transform.parent.GetChild(0).tag) {
			enemyDetected = true;
			enemyChar = other.gameObject.transform;
			StartCoroutine(FollowTimer());
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
		bool enemyDodgeHeal = characterSheetScript.dodgeHeal;

		int dodgeChance = Random.Range(1, 101);
		int critChance = Random.Range(1, 101);

		// Check if enemy has rage mode on
		if (characterSheetScript.rageModeOn) {
			enemyDefense *= 2;
		} else if (!characterSheetScript.rageModeOn) {
			enemyDefense *= 1;
		}

		// Check if enemy dodges attack
		if (dodgeChance > enemyDodge) {
			dealDamage = casterDamage - ((enemyDefense / 100) * casterDamage);

			// If rage mode is on and on level 2 then multiply damage
			if (characterSheetScript.rageModeOn && characterSheetScript.rageLevel >= 2) {
				dealDamage *= 2;
			} else if (!characterSheetScript.rageModeOn) {
				dealDamage *= 1;
			}

			// If character lands a critical strike then multiply damage
			if (casterCritChance <= critChance) {
				dealDamage *= casterCritDMG;
			}

			characterSheetScript.currentHealth -= dealDamage;
		} else {
			// Healing when dodging an attack
			if (enemyDodgeHeal) {
				characterSheetScript.currentHealth += characterSheetScript.currentHealth * 0.2f;
			}
		}
	}


	// private IEnumerator LifeTime() {
	// 	yield return new WaitForSeconds(lifeTime);
	// 	Destroy(transform.parent.gameObject);
	// }


	private IEnumerator FollowTimer() {
		yield return new WaitForSeconds(3.0f);
		enemyDetected = false;
	}

}
