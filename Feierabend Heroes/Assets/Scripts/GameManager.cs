using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject statsCharacter;
	public GameObject statsParent;

	public static int playerCount = 2;

	public static bool enableModifier;
	public static bool allowMovement;
	public static bool allowRespawning;

	public static int activePlayers;


	void Start()
	{
		activePlayers = playerCount;
	}


	// Level end script
	public void LevelEnd () {
		GetComponent<LevelTimer>().countdownTimerText.enabled = false;
		GetComponent<LevelTimer>().levelDurationText.enabled = false;

		activePlayers = 0;

		allowMovement = false;
		enableModifier = false;

		// Instantiate the stats sheet
		GameObject statsSheet = Instantiate(statsCharacter, statsCharacter.transform.position, statsCharacter.transform.rotation);
		statsSheet.transform.parent = statsParent.transform;

		// Position the stats sheet
		statsSheet.transform.position = new Vector3(456, 222, 0);

		for (int i = 0; i < playerCount; i++) {
			// PlayerPrefs.SetInt("char0" + i + "Health", );
		}

		// SceneManager.LoadScene("1 Next Game");
	}

}
