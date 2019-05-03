// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

public class SettingsHolder {

	public static int[] playerClasses = {-1, -1, -1, -1};
	public static string[] heroNames = {"", "", "", ""};
	public static float[] rankingBonus = {0f, 0.05f, 0.1f, 0.2f};
	public static int playerCount = 0;
	public static int registeredPlayers = 0;

	// 0 = isometric camera ; 1 = 3rd person camera
	public static int perspectiveMode = 0;

	public static int publicEnemy = -1;
	public static float sizeIncrease = 2.0f;
	public static float speedDecrease = 0.8f;

	// 0 = List ; 1 = Cards
	public static int skillMode = 0;

	public static int battleType = -1;
	public static int gameType = -1;
	public static int bountyType = -1;

	// Stop character movement when attacking or not
	public static bool delayMovement = true;

	// "4 Aeras" ; "5 Hunted"
	public static string selectedMap;
	// public static string selectedMap = "4 Aeras";
	// public static string selectedMap = "5 Hunted";

	public static int currentRound = 0;
	public static int amountOfRounds = 10;

	public static float exploreTime = 5.0f;
	public static float battleTime = 1200.0f;

	public static int appleSpawnMax = 0;
	public static int orbSpawnMax = 17;
	// public static float timeMultiplier = 0;
	// public static float spawnDelayTime = 0;

	// Need 4785 orbs to complete full skillset
	public static int startingOrbs = 0;
	public static int orbsToWin = 9999;

	public static int orbsEveryRound = 15;
	public static int orbsForRoundWin = 15;
	public static int orbsForKills = 20;
	public static int orbsFromChests = 10;

	public static bool matchOver = false;

	public static bool initialSpawn = false;
	public static bool playedFirstRound = false;
	public static bool nextMatch = false;

	public static int tierTwoCosts = 100;
	public static int tierThreeCosts = 200;

	public static int orbsForBountyDef = 50;
	public static int orbsForBounty = 50;
	public static int increaseBountyBy = 10;

}