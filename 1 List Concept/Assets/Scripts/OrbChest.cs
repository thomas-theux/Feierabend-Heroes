using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbChest : MonoBehaviour {

	public AudioSource spawnOrbSound;
	public AudioSource collectOrbSound;

	// public GameObject openChestIcon;

	// private bool openedChest = false;
	private int rndDoubleOrb;


	private void Awake() {
		if (TimeHandler.startBattle) {
			StartCoroutine(SpawnSound());
		}
	}


	private void OnTriggerEnter(Collider other) {
		// if (other.tag.Contains("Character0") || other.tag.Contains("Character1") || other.tag.Contains("Character2") || other.tag.Contains("Character3")) {
		// 	openChestIcon.SetActive(true);
		// }

		if (other.tag.Contains("Character")) {
			CharacterSheet characterSheetScript = other.GetComponent<CharacterSheet>();

			Instantiate(collectOrbSound);

			// DEV STUFF – Collect data on how many times an attack has been used
			int chestsCollected = PlayerPrefs.GetInt("Chests Collected");
			chestsCollected++;
			PlayerPrefs.SetInt("Chests Collected", chestsCollected);
			
			rndDoubleOrb = Random.Range(1, 101);

			// Check if player has activated the DOUBLE ORB skill
			if (rndDoubleOrb > characterSheetScript.doubleOrbChance) {
				characterSheetScript.currentOrbs += 1;
			} else {
				characterSheetScript.currentOrbs += 2;
			}

			Destroy(gameObject);
		}
	}


	// private void OnTriggerExit(Collider other) {
	// 	if (other.tag.Contains("Character0") || other.tag.Contains("Character1") || other.tag.Contains("Character2") || other.tag.Contains("Character3")) {
	// 		openChestIcon.SetActive(false);
	// 	}
	// }


	// private void OnTriggerStay(Collider other) {

	// 	if (other.tag.Contains("Character")) {
	// 		CharacterMovement characterMovementScript = other.GetComponent<CharacterMovement>();
	// 		CharacterSheet characterSheetScript = other.GetComponent<CharacterSheet>();

	// 		if (characterMovementScript.interactBtn && !openedChest) {
	// 			openedChest = true;

	// 			Instantiate(collectOrbSound);

	// 			// DEV STUFF – Collect data on how many times an attack has been used
	// 			int chestsCollected = PlayerPrefs.GetInt("Chests Collected");
	// 			chestsCollected++;
	// 			PlayerPrefs.SetInt("Chests Collected", chestsCollected);
				
	// 			rndDoubleOrb = Random.Range(1, 101);

	// 			// Check if player has activated the DOUBLE ORB skill
	// 			if (rndDoubleOrb > characterSheetScript.doubleOrbChance) {
	// 				characterSheetScript.currentOrbs += 1;
	// 			} else {
	// 				characterSheetScript.currentOrbs += 2;
	// 			}

	// 			Destroy(gameObject);
	// 		}
	// 	}

	// }


	private IEnumerator SpawnSound() {
		yield return new WaitForSeconds(0.2f);

		Instantiate(spawnOrbSound);
	}

}
