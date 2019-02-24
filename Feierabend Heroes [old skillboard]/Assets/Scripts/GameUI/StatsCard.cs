using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsCard : MonoBehaviour {

    public int resultsID = 0;

    public Text charNameText;
    public Text orbsCountStatText;
    public Text killsStatText;
    public Text deathsStatText;
    public Text rankingText;

    public Image rankingBanner;
	public Image charBackgroundImage;

	private Color[] charColors = {
		new Color32(23, 155, 194, 255),
		new Color32(194, 66, 64, 255),
		new Color32(35, 194, 112, 255),
		new Color32(182, 194, 35, 255)
	};


    private void Start() {
        charNameText.text = SettingsHolder.charNames[resultsID];
        orbsCountStatText.text = GameManager.orbsCountStatsArr[resultsID] + "";
        killsStatText.text = GameManager.killsStatsArr[resultsID] + "";
        deathsStatText.text = GameManager.deathsStatsArr[resultsID] + "";

        charBackgroundImage.color = charColors[resultsID];

        switch (GameManager.rankingArr[resultsID]) {
            case 0:
                rankingText.text = "Bleedsau!";
                rankingBanner.color = new Color32(255, 255, 255, 255);
                break;
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