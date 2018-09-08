using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRoyale : MonoBehaviour {

	void Start ()
	{
		// Respawning disabled in this game mode
		GameManager.allowRespawning = false;
	}

	void Update()
	{
		if (GameManager.activePlayers == 1) {
			GameManager.LevelEnd();
		}
	}

}
