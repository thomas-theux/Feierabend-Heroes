using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeDeathHandler : MonoBehaviour {

	private CharacterSheet characterSheetScript;

	public GameObject dudeHeadGO;
	public GameObject dudeBodyGO;

	public AudioSource characterDiesSound;

	private GameObject levelGO;
	private float clearance = 5.0f;

	private float defDefense;
	private float defMoveSPD;
	private float defOneDMG;
	private float defTwoDMG;
	private float defOneSPD;
	private float defTwoSPD;

	public bool healthIsFull = true;
	public bool isOutside = false;

	private bool isWaiting = false;
	public bool charIsDead = false;

	public int lastDamagerID = -1;
	private bool isResettingID = false;
	private float safeZoneDamage = 60.0f;

	public Image redFlash;
	public bool gotHit = false;


	private void Awake() {
		characterSheetScript = GetComponent<CharacterSheet>();
		levelGO = GameObject.Find("Ground");
	}


	void OnEnable() {
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}
         
	void OnDisable() {
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}


	private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
		// Reset all bools
		healthIsFull = true;
		isOutside = false;
		isWaiting = false;
		charIsDead = false;
		isResettingID = false;

		lastDamagerID = -1;

		GameObject firstChild = transform.GetChild(transform.childCount-1).gameObject;
		GameObject secondChild = firstChild.transform.GetChild(transform.GetChild(transform.childCount-1).transform.childCount-1).gameObject;
		redFlash = secondChild.GetComponent<Image>();
	}


	private void Update() {
		// Reset damager ID after 100ms to avoid false stats
		if (lastDamagerID > -1 && !isResettingID) {
			StartCoroutine(ResetDamagerID());
		}

		if (gotHit) {
			StartCoroutine(HurtCharacter());
		}
	}


	private void LateUpdate() {
		// Continuously increase HP if SELF HEAL skill is activated
		if (characterSheetScript.selfRepairActive && !healthIsFull) {
			SelfHeal();
		}

		// Damage character when outside of safe zone
		if (isOutside && characterSheetScript.currentHealth > 0 && TimeHandler.startBattle) {
			OutsideSafeZone();
		}

		HPCapCheck();

		if (characterSheetScript.currentHealth <= 0 && !isWaiting && !charIsDead) {
			StartCoroutine(KillCharacter());
		}
	}


	private void SelfHeal() {
		characterSheetScript.currentHealth += 10.0f * Time.deltaTime;
	}


	public void OutsideSafeZone() {
		characterSheetScript.currentHealth -= safeZoneDamage * Time.deltaTime;
	}


	private void HPCapCheck() {
		// Cap the current HP if higher than max HP
		if (characterSheetScript.currentHealth > characterSheetScript.maxHealth && !healthIsFull) {
			characterSheetScript.currentHealth = characterSheetScript.maxHealth;
			healthIsFull = true;
		} else if (characterSheetScript.currentHealth < characterSheetScript.maxHealth && healthIsFull) {
			healthIsFull = false;
		}
	}


	private IEnumerator KillCharacter() {
		isWaiting = true;
		charIsDead = true;

		Instantiate(characterDiesSound);

		if (lastDamagerID > -1) {
			// Increase stats for kills
			if (GameManager.killsStatsArr[lastDamagerID] > -1) {
				GameManager.killsStatsArr[lastDamagerID]++;
			}

			// Give player who killed this character two extra orbs
			GameObject.Find("Character" + lastDamagerID).GetComponent<CharacterSheet>().currentOrbs += SettingsHolder.orbsForKills;

			// Steal orb from killed character
			for (int i = 0; i < SettingsHolder.orbsForKills; i++) {
				if (characterSheetScript.currentOrbs > 0) {
					characterSheetScript.currentOrbs--;
				}
			}
		}

		// Increase stats for deaths
		GameManager.deathsStatsArr[characterSheetScript.charID]++;
		
		// dudeHeadGO.GetComponent<Renderer>().enabled = false;
		// dudeBodyGO.GetComponent<Renderer>().enabled = false;
		// gameObject.GetComponent<CapsuleCollider>().enabled = false;

		yield return new WaitForSeconds(2.0f);

		CheckForRespawn();
	}


	private void CheckForRespawn() {
		// Check for RESPAWN skill
		if (characterSheetScript.respawnChance > 0) {
			int rndRespawn = Random.Range(1, 101);

			if (rndRespawn > characterSheetScript.respawnChance) {
				RemoveFromList();
			} else {
				float minX = (levelGO.transform.localScale.x / 2) - levelGO.transform.localScale.x + clearance;
				float maxX = (levelGO.transform.localScale.x / 2) - clearance;
				float minZ = (levelGO.transform.localScale.z / 2) - levelGO.transform.localScale.z + clearance;
				float maxZ = (levelGO.transform.localScale.z / 2) - clearance;

				float posX = Random.Range(minX, maxX);
				float posZ = Random.Range(minZ, maxZ);

				this.gameObject.transform.position = new Vector3(posX, 1, posZ);

				characterSheetScript.currentHealth = characterSheetScript.maxHealth;

				isWaiting = false;
				charIsDead = false;

				// EnableCharRenderer();
			}
		} else {
			RemoveFromList();
		}
	}


	private void RemoveFromList() {
		// Remove the just killed character from the player array
		GameManager.activePlayers.Remove(characterSheetScript.charID);

		if (GameManager.activePlayers.Count <= 1) {
			TimeHandler.lastSeconds = true;
		}
	}


	public void EnableCharRenderer() {
		dudeHeadGO.GetComponent<Renderer>().enabled = true;
		dudeBodyGO.GetComponent<Renderer>().enabled = true;
		gameObject.GetComponent<CapsuleCollider>().enabled = true;
	}


	private IEnumerator ResetDamagerID() {
		isResettingID = true;
		yield return new WaitForSeconds(0.1f);
		lastDamagerID = -1;
		isResettingID = false;
	}


	private IEnumerator HurtCharacter() {
		gotHit = false;

		redFlash.color = new Color32(152, 24, 24, 100);
		yield return new WaitForSeconds(0.1f);
		redFlash.color = new Color32(152, 24, 24, 0);
	}

}
