using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public UISpawner uiSpawnerScript;

	public static int playerCount = 2;

	public static List<int> activePlayerArr = new List<int>();

	public static bool enableModifier;
	public static bool allowMovement;
	public static bool allowRespawning;

	public static int activePlayers;

	private bool startStatsTimer;
	private bool distributionStarted;
	private float startDistributionTimer;
	private float distributionTimer;

	public Text distributionDurationText;

	private int wonStatPoints;

	public static bool lastDudeStanding;
	private int winnerID;
	private Camera winnerCam;
	private bool foundCamera;

	private float camPosX;
	private float camPosY;
	private float camScaleX;
	private float camScaleY;
	private Rect defaultCamPos;

	private float camScaleSpeed = 0.02f;

	private float camTimer = 1;

	public GameObject timerBox;


	void Start()
	{
		activePlayers = playerCount;
		wonStatPoints = playerCount;
		startDistributionTimer = 0.5f;
		distributionTimer = 5.0f;

		// Write all players in an array to see who is (still) in the game
		activePlayerArr.Clear();
		for (int j = 0; j < playerCount; j++) {
			activePlayerArr.Add(j);
		}
	}


	void Update()
	{
		// 1/2 second before distribution is open
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

		if (lastDudeStanding) {

			if (!foundCamera) {
				// Find winner camera and bring it to the front
				winnerCam = GameObject.Find("Camera0" + winnerID + "(Clone)").GetComponent<Camera>();
				winnerCam.depth = 10;

				// Disable the timer box
				timerBox.SetActive(false);

				// Get camera rect data
				camPosX = winnerCam.rect.x;
				camPosY = winnerCam.rect.y;
				camScaleX = winnerCam.rect.width;
				camScaleY = winnerCam.rect.height;
				defaultCamPos = winnerCam.rect;

				foundCamera = true;
			}

			// Resize the camera until it spans across the whole screen
			camPosX = camPosX > 0 ? camPosX-camScaleSpeed : 0;
			camPosY = camPosY > 0 ? camPosY-camScaleSpeed : 0;
			camScaleX = camScaleX < 1 ? camScaleX+camScaleSpeed : 1;
			camScaleY = camScaleY < 1 ? camScaleY+camScaleSpeed : 1;

			winnerCam.rect = new Rect(camPosX, camPosY, camScaleX, camScaleY);

			if (winnerCam.rect == new Rect(0, 0, 1, 1) && camTimer > 0) {
				camTimer -= Time.deltaTime;
			} else if (winnerCam.rect == new Rect(0, 0, 1, 1) && camTimer <= 0) {
				// Reset all cameras to normal
				lastDudeStanding = false;
				winnerCam.rect = defaultCamPos;
				for (int k = 0; k < playerCount; k++) {
					GameObject.Find("Camera0" + k + "(Clone)").GetComponent<Camera>().enabled = true;
				}
				timerBox.SetActive(true);
				// Instantiate the stats sheet
				uiSpawnerScript.SpawnUI();
				startStatsTimer = true;
			}			
		}
	}


	// Level end script
	public void LevelEnd () {
		GetComponent<LevelTimer>().countdownTimerText.enabled = false;

		activePlayers = 0;

		allowMovement = false;
		enableModifier = false;

		// Winner gets stat points
		if (activePlayerArr.Count == 1) {
			winnerID = activePlayerArr[0];
			GameObject levelWinner = GameObject.Find("Character0" + winnerID + "(Clone)");
			levelWinner.GetComponent<CharacterStats>().currentStatPoints += wonStatPoints;
			lastDudeStanding = true;
		}
	}

	
	public void NextLevel () {
		// Save all stats to file
		for (int i = 0; i < playerCount; i++) {
			CharacterStats charInstance = GameObject.Find("Character0" + i + "(Clone)").GetComponent<CharacterStats>();

			PlayerPrefs.SetFloat("P" + i + "StatHealth", charInstance.characterHealth);
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