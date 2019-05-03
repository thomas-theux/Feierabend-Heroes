using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class MatchEnd : MonoBehaviour {

    public GameObject buttonsUIInteraction;
    public GameObject dontDestroyManager;

    public Camera winnerCam;
    public int winnerID = 0;


    private void OnEnable() {
        if (SettingsHolder.matchOver) {
            buttonsUIInteraction.SetActive(true);
        }
    }


    private void Update() {
        if (SettingsHolder.matchOver) {
            GetInput();
        }
    }


    private void GetInput() {
        if (ReInput.players.GetPlayer(0).GetButtonDown("Circle")) {
            QuitMatch();
        }

        // if (ReInput.players.GetPlayer(0).GetButtonDown("Square")) {
        //     NextMatch();
        // }

        if (ReInput.players.GetPlayer(0).GetButtonDown("X")) {
            RestartMatch();
        }
    }


    private void QuitMatch() {
        // Reset every variable
        ResetAllStats();
        SettingsHolder.selectedMap = "";

        // Kill DontDestroyOnLOad game objects
		Destroy(dontDestroyManager);

        // Load the main menu screen
        SceneManager.LoadScene("0 Main Screen");
    }


    private void NextMatch() {
        SettingsHolder.nextMatch = true;

        // Reset some of the variables
        ResetSomeStats();

        // Load the same level again
        SceneManager.LoadScene(SettingsHolder.selectedMap);
    }


    private void RestartMatch() {
        // Reset every variable
        ResetAllStats();

        // Kill DontDestroyOnLOad game objects
		Destroy(dontDestroyManager);

        // Load the same level again
        SceneManager.LoadScene(SettingsHolder.selectedMap);
    }


    private void ResetSomeStats() {
        for (int i = 0; i < SettingsHolder.playerCount; i++) {
            GameManager.orbsCountStatsArr[i] = 0;
            GameManager.killsStatsArr[i] = 0;
            GameManager.deathsStatsArr[i] = 0;
            GameManager.orbsSpentStatsArr[i] = 0;

        }

        ///////////////////////
        // Reset player cameras
        float camPosX = 0;
        float camPosY = 0;
        float camWidth = 0.5f;
        float camHeight = 0.5f;

        switch (winnerID) {
            case 0:
                camPosX = 0.0f;
                camPosY = 0.5f;
                break;
            case 1:
                camPosX = 0.5f;
                camPosY = 0.5f;
                break;
            case 2:
                camPosX = 0.0f;
                camPosY = 0.0f;
                break;
            case 3:
                camPosX = 0.5f;
                camPosY = 0.0f;
                break;
        }

        if (SettingsHolder.playerCount == 2) {
            camPosY = 0.0f;
            camHeight = 1.0f;
        }
        
        winnerCam.rect = new Rect(camPosX, camPosY, camWidth, camHeight);
        ///////////////////////
        
        GameManager.activePlayers.Clear();
        GameManager.rankingsArr.Clear();

        for (int i = 0; i < SettingsHolder.playerCount; i++) {
			GameManager.activePlayers.Add(i);
		}

        GameManager.pauseMenuOpen = false;
        GameManager.whoPaused = -1;

        SettingsHolder.publicEnemy = -1;

        SettingsHolder.currentRound = 0;

        SettingsHolder.orbsForBounty = 50;

        SettingsHolder.matchOver = false;
        SettingsHolder.playedFirstRound = false;
    }


    private void ResetAllStats() {
        for (int i = 0; i < SettingsHolder.playerCount; i++) {
            GameManager.orbsCountStatsArr[i] = 0;
            GameManager.killsStatsArr[i] = 0;
            GameManager.deathsStatsArr[i] = 0;
            GameManager.orbsSpentStatsArr[i] = 0;

            SettingsHolder.playerClasses[i] = -1;
            SettingsHolder.heroNames[i] = "";
        }

        GameManager.activePlayers.Clear();
        GameManager.rankingsArr.Clear();

        GameManager.pauseMenuOpen = false;
        GameManager.whoPaused = -1;

        SettingsHolder.playerCount = 0;
        SettingsHolder.registeredPlayers = 0;
        SettingsHolder.publicEnemy = -1;

        SettingsHolder.battleType = -1;
        SettingsHolder.gameType = -1;
        SettingsHolder.bountyType = -1;

        SettingsHolder.currentRound = 0;
        SettingsHolder.amountOfRounds = 10;

        SettingsHolder.startingOrbs = 0;
        SettingsHolder.orbsForBounty = 50;

        SettingsHolder.matchOver = false;
        SettingsHolder.initialSpawn = false;
        SettingsHolder.playedFirstRound = false;
    }

}
