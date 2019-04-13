// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

public class SettingsHolder {

	public static int[] playerClasses = {-1, -1, -1, -1};
	public static string[] heroNames = {"", "", "", ""};
	public static int playerCount = 0;
	public static int registeredPlayers = 0;

	// 0 = List ; 1 = Cards
	public static int skillMode = 0;

	// Stop character movement when attacking or not
	public static bool delayMovement = true;

	// "3 Aeras" ; "4 Hunted"
	public static string selectedMap = "3 Aeras";

	public static int currentRound = 0;
	public static int amountOfRounds = 10;

	public static float exploreTime = 5.0f;
	public static float battleTime = 120.0f;

	public static int appleSpawnMax = 0;
	public static int orbSpawnMax = 15;

	public static int startingOrbs = 200;
	public static int orbsToWin = 9999;

	public static int orbsEveryRound = 15;
	public static int orbsForRoundWin = 15;
	public static int orbsForKills = 20;
	public static int orbsFromChests = 10;

	public static bool matchOver = false;

	public static bool initialSpawn = false;
	public static bool nextMatch = false;

	public static int tierTwoCosts = 60;
	public static int tierThreeCosts = 180;

}