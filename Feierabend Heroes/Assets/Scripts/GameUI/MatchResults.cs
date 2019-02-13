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


    // private void Update() {
    //     if (ReInput.players.GetPlayer(0).GetButtonDown("Circle")) {
    //         QuitMatch();
    //     }

    //     if (ReInput.players.GetPlayer(0).GetButtonDown("X")) {
    //         RestartMatch();
    //     }

    //     if (ReInput.players.GetPlayer(0).GetButtonDown("Square")) {
    //         NextMatch();
    //     }
    // }


    // private void QuitMatch() {
    //     SceneManager.LoadScene("1 Character Selection");
    // }


    // private void RestartMatch() {
    //     // Reset all stats
    //     for (int i = 0; i < SettingsHolder.playerCount; i++) {
    //         GameManager.killsStatsArr[i] = 0;
    //         GameManager.deathsStatsArr[i] = 0;
    //         GameManager.orbsSpentStatsArr[i] = 0;
    //         GameManager.rankingArr[i] = 0;

    //         GameObject.Find("Character" + i).GetComponent<CharacterSheet>().currentOrbs = 0;
    //         GameObject.Find("Character" + i).GetComponent<SkillTreeUIHandler>().currentOrbs = 0;
    //     }

    //     SceneManager.LoadScene("3 Aeras");
    // }


    // private void NextMatch() {
    //     SceneManager.LoadScene("3 Aeras");
    // }

}
