using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject statsSheet;
	// public GameObject statsParent;

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
		// CODE

		// Save all stats to file
		// CODE

		// SceneManager.LoadScene("1 Next Game");
	}

}
