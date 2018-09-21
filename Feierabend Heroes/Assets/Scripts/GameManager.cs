using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public UISpawner uiSpawnerScript;

	public static int playerCount = 3;

	public static bool enableModifier;
	public static bool allowMovement;
	public static bool allowRespawning;

	public static int activePlayers;

	private bool startStatsTimer;
	private bool distributionStarted;
	private float startDistributionTimer;
	private float distributionTimer;

	public Text distributionDurationText;


	void Start()
	{
		activePlayers = playerCount;
		startDistributionTimer = 1.0f;
		distributionTimer = 10.0f;
	}


	void Update()
	{
		// 1 second before distribution is open
		if (startStatsTimer) {
			startDistributionTimer -= Time.deltaTime;
			if (startDistributionTimer <= 0) {
				CursorController.enableDistribution = true;
				distributionStarted = true;
				startStatsTimer = false;
			}
		}

		// 10 seconds distribution time window is open
		if (distributionStarted) {
			distributionTimer -= Time.deltaTime;
			distributionDurationText.text = Mathf.Ceil(distributionTimer) + "";
			if (distributionTimer <= 0.3f) {
				CursorController.enableDistribution = false;
				distributionStarted = false;
				NextLevel();
			}
		}
	}


	// Level end script
	public void LevelEnd () {
		GetComponent<LevelTimer>().countdownTimerText.enabled = false;
		GetComponent<LevelTimer>().levelDurationText.enabled = false;

		activePlayers = 0;

		allowMovement = false;
		enableModifier = false;

		// Instantiate the stats sheet
		uiSpawnerScript.SpawnUI();
		startStatsTimer = true;
	}

	
	public void NextLevel () {
		// Save all stats to file
		for (int i = 0; i < playerCount; i++) {
			CharacterStats charInstance = GameObject.Find("Character0" + i + "(Clone)").GetComponent<CharacterStats>();
			
			PlayerPrefs.SetInt("P" + i + "StatHealth", charInstance.characterHealth);
			PlayerPrefs.SetFloat("P" + i + "StatAttackMin", charInstance.characterAttackMin);
			PlayerPrefs.SetFloat("P" + i + "StatAttackMax", charInstance.characterAttackMax);
			PlayerPrefs.SetInt("P" + i + "StatDefense", charInstance.characterDefense);
			PlayerPrefs.SetFloat("P" + i + "StatSpeed", charInstance.characterSpeed);
			PlayerPrefs.SetInt("P" + i + "StatLuck", charInstance.characterLuck);
			PlayerPrefs.SetInt("P" + i + "CurrentStatPoints", charInstance.currentStatPoints);
		}

		SceneManager.LoadScene("1 Next Game");
	}

}