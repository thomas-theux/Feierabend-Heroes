// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

public class SettingsHolder {

	public static int[] playerClasses = {-1, -1, -1, -1};
	// public static int[] playerClasses = {0, 0, 0, 0};
	public static string[] charNames = {"", "", "", ""};
	public static int playerCount = 0;
	public static int registeredPlayers = 0;

	public static int appleMax = 20;
	public static int chestMax = 20;

	public static int startingOrbs = 2;
	public static int orbsEveryRound = 1;
	public static int orbsForRoundWin = 2;
	public static int orbsToWin = 15;

	public static bool matchOver = false;

	public static bool initialSpawn = false;
	public static bool nextMatch = false;


	// DEV STUFF – Delete for production
	public static int orbsSpawned = 0;
	public static int chestsCollected = 0;
	public static int spentOrbs = 0;

	public static int meteorShot = 0;
	public static int fireBlock = 0;
	public static int doubleHP = 0;
	public static int spawnCompanion = 0;

	public static int wrenchPunch = 0;
	public static int bombThrow = 0;
	public static int healingBeacon = 0;
	public static int turretGun = 0;

}