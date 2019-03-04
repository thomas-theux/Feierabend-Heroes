// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

public class SettingsHolder {

	public static int[] playerClasses = {-1, -1, -1, -1};
	// public static int[] playerClasses = {0, 0, 0, 0};
	public static string[] charNames = {"", "", "", ""};
	public static int playerCount = 0;
	public static int registeredPlayers = 0;

	public static float exploreTime = 6.0f;
	public static float battleTime = 120.0f;

	public static int appleSpawnMax = 10;
	public static int chestSpawnMax = 10;

	public static int startingOrbs = 200;
	public static int orbsToWin = 215;
	public static int orbsEveryRound = 1;
	public static int orbsForRoundWin = 2;
	public static int orbsForKills = 2;

	public static bool matchOver = false;

	public static bool initialSpawn = false;
	public static bool nextMatch = false;

	public static int tierTwoCosts = 8;
	public static int tierThreeCosts = 16;

}