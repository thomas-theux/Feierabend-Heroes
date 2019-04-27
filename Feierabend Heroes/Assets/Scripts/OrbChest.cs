using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbChest : MonoBehaviour {

	public AudioSource spawnOrbSound;
	public AudioSource collectOrbSound;


	private void Awake() {
		if (TimeHandler.startBattle) {
			StartCoroutine(SpawnSound());
		}
	}


	private void OnTriggerEnter(Collider other) {

		if (other.tag.Contains("Character")) {
			CharacterSheet characterSheetScript = other.GetComponent<CharacterSheet>();

			Instantiate(collectOrbSound);

			// DEV STUFF – Collect data on how many times an attack has been used
			int chestsCollected = PlayerPrefs.GetInt("Chests Collected");
			chestsCollected++;
			PlayerPrefs.SetInt("Chests Collected", chestsCollected);

			characterSheetScript.currentOrbs += SettingsHolder.orbsFromChests;

			Destroy(gameObject);
		}
	}


	private IEnumerator SpawnSound() {
		yield return new WaitForSeconds(0.2f);

		Instantiate(spawnOrbSound);
		GameManager.spawnWaits.Remove(0);
	}

}
