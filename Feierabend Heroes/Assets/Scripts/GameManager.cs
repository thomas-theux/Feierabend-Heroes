using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

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
	public static void LevelEnd () {
		activePlayers = 0;

		allowMovement = false;
		enableModifier = false;

		SceneManager.LoadScene("Next Game");
	}

}
