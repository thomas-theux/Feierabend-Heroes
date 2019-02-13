using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsCard : MonoBehaviour {

    public int resultsID = 0;

    public Text charNameText;
    public Text killsStatText;
    public Text deathsStatText;
    public Text orbsSpentStatText;
    public Text rankingText;

    public Image rankingBanner;


    private void Start() {
        charNameText.text = SettingsHolder.charNames[resultsID];
        killsStatText.text = GameManager.killsStatsArr[resultsID] + "";
        deathsStatText.text = GameManager.deathsStatsArr[resultsID] + "";
        orbsSpentStatText.text = GameManager.orbsSpentStatsArr[resultsID] + "";

        switch (GameManager.orbsSpentStatsArr[resultsID]) {
            case 1:
                rankingText.text = "WINNER!";
                rankingBanner.color = new Color32(255, 109, 1, 255);
                break;
            case 2:
                rankingText.text = "2ND";
                rankingBanner.color = new Color32(255, 255, 255, 255);
                break;
            case 3:
                rankingText.text = "3RD";
                rankingBanner.color = new Color32(255, 255, 255, 255);
                break;
            case 4:
                rankingText.text = "4TH";
                rankingBanner.color = new Color32(255, 255, 255, 255);
                break;
        }
    }

}