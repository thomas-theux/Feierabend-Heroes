using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRoyale : MonoBehaviour {

	void Start ()
	{
		// Respawning disabled in this game mode
		HealthManager.respawningAllowed = false;
	}
}
