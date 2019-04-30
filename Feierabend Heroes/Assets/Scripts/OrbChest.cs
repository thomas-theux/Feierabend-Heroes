using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbChest : MonoBehaviour {

	public OrbChestSpawner orbChestSpawnerScript;

	public int orbID = 0;

	public AudioSource spawnOrbSound;
	public AudioSource collectOrbSound;


	private void OnEnable() {
		StartCoroutine(SoundDelay());
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


	private void OnDestroy() {
		orbChestSpawnerScript.currentOrbCount--;
		orbChestSpawnerScript.RespawnOrb();
	}


	private IEnumerator SoundDelay() {
		yield return new WaitForSeconds(0.01f);

		if (TimeHandler.startLevel) {
			Instantiate(spawnOrbSound);
		}
	}

}