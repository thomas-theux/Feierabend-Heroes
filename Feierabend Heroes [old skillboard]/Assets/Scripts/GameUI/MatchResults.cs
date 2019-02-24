using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Rewired;

public class MatchResults : MonoBehaviour {
    
    public GameObject statsCardGO;
    public GameObject cardsParentGO;

    private bool quitButton;
    private bool restartButton;
    private bool nextButton;


    private void Awake() {
        for (int i = 0; i < SettingsHolder.playerCount; i++) {
            GameObject newCard = Instantiate(statsCardGO);
            newCard.transform.SetParent(cardsParentGO.transform);
            newCard.transform.localPosition = new Vector3(283 * i, newCard.transform.position.y, newCard.transform.position.z);

            newCard.GetComponent<StatsCard>().resultsID = i;
        }
    }


    private void Update() {
        if (ReInput.players.GetPlayer(0).GetButtonDown("Circle")) {
            QuitMatch();
        }

        if (ReInput.players.GetPlayer(0).GetButtonDown("Square")) {
            NextMatch();
        }

        if (ReInput.players.GetPlayer(0).GetButtonDown("X")) {
            RestartMatch();
        }
    }


    private void QuitMatch() {
        // Kill DontDestroyOnLOad game objetcts
		for (int i = 0; i < SettingsHolder.playerCount; i++) {
			Destroy(GameObject.Find("Character" + i));
			Destroy(GameObject.Find("PlayerCamera" + i));
        }
        
        SettingsHolder.matchOver = false;

        SceneManager.LoadScene("1 Character Selection");
    }


    private void NextMatch() {
        SettingsHolder.nextMatch = true;
        SettingsHolder.matchOver = false;

        SceneManager.LoadScene("3 Aeras");
    }


    private void RestartMatch() {
        // Kill DontDestroyOnLOad game objetcts
		for (int i = 0; i < SettingsHolder.playerCount; i++) {
			Destroy(GameObject.Find("Character" + i));
			Destroy(GameObject.Find("PlayerCamera" + i));
        }

        SettingsHolder.initialSpawn = false;
        SettingsHolder.matchOver = false;

        SceneManager.LoadScene("3 Aeras");
    }

}
