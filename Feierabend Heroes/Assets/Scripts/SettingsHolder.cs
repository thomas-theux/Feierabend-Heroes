﻿// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

public class SettingsHolder {

	public static int[] playerClasses = {-1, -1, -1, -1};
	public static string[] charNames = {"", "", "", ""};
	public static int playerCount = 0;
	public static int registeredPlayers = 0;

	// 0 = List ; 1 = Cards
	public static int skillMode = 0;

	public static int amountOfRounds = 2;
	public static int currentRound = 0;

	public static float exploreTime = 5.0f;
	public static float battleTime = 120.0f;

	public static int appleSpawnMax = 0;
	public static int orbSpawnMax = 15;

	public static int startingOrbs = 2;
	public static int orbsToWin = 20;
	public static int orbsEveryRound = 2;
	public static int orbsForRoundWin = 2;
	public static int orbsForKills = 2;

	public static bool matchOver = false;

	public static bool initialSpawn = false;
	public static bool nextMatch = false;

	public static int tierTwoCosts = 6;
	public static int tierThreeCosts = 12;

	// Stats for character classes – Health, Defense, Move Speed
	// public static float[] classHP = {260.0f, 380.0f, 300.0f, 220.0f};
	// public static float[] classDEF = {8.0f, 18.0f, 10.0f, 6.0f};
	// public static float[] classMSPD = {10.0f, 8.0f, 9.0f, 11.0f};

}