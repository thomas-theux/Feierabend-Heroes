﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbChest : MonoBehaviour {

	public AudioSource spawnOrbSound;
	public AudioSource collectOrbSound;

	public GameObject openChestIcon;

	private bool openedChest = false;
	private int rndDoubleOrb;


	private void Awake() {
		if (TimeHandler.startBattle) {
			StartCoroutine(SpawnSound());
		}
	}


	private void OnTriggerEnter(Collider other) {
		if (other.tag.Contains("Character0") || other.tag.Contains("Character1") || other.tag.Contains("Character2") || other.tag.Contains("Character3")) {
			openChestIcon.SetActive(true);
		}
	}


	private void OnTriggerExit(Collider other) {
		if (other.tag.Contains("Character0") || other.tag.Contains("Character1") || other.tag.Contains("Character2") || other.tag.Contains("Character3")) {
			openChestIcon.SetActive(false);
		}
	}


	private void OnTriggerStay(Collider other) {

		if (other.tag.Contains("Character")) {
			CharacterMovement characterMovementScript = other.GetComponent<CharacterMovement>();
			CharacterSheet characterSheetScript = other.GetComponent<CharacterSheet>();

			if (characterMovementScript.activationBtn && !openedChest) {
				openedChest = true;

				Instantiate(collectOrbSound);

				// DEV STUFF – Collect data on how many times an attack has been used
				SettingsHolder.chestsCollected++;
				PlayerPrefs.SetInt("Chests Collected", SettingsHolder.chestsCollected);
				
				// Check if RAGE MODE is on and on level 4
				if (characterSheetScript.rageModeOn && characterSheetScript.rageLevel >= 4) {
					rndDoubleOrb = -1;
				} else {
					rndDoubleOrb = Random.Range(1, 101);
				}

				// Check if player has activated the DOUBLE ORB skill
				if (rndDoubleOrb > characterSheetScript.doubleOrbChance) {
					characterSheetScript.currentOrbs += 1;
				} else {
					if (characterSheetScript.findThreeOrbs) {
						characterSheetScript.currentOrbs += 3;
					} else {
						characterSheetScript.currentOrbs += 2;
					}
				}

				Destroy(gameObject);
			}
		}

	}


	private IEnumerator SpawnSound() {
		yield return new WaitForSeconds(0.2f);

		Instantiate(spawnOrbSound);
	}

}
