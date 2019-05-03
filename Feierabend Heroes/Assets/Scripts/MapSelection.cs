using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Rewired;
using UnityEngine.SceneManagement;

public class MapSelection : MonoBehaviour {

    public Image settingsCursor;
    public GameObject mapListParent;

    public Image selectedMapImage;

	public AudioSource navigateSound;
    public AudioSource cancelSound;
	public AudioSource selectSound;

    private List<GameObject> mapListArr = new List<GameObject>();
    public List<string> mapNamesArr = new List<string>();

    private int currentIndex = 0;


    private void Awake() {
        // Add all maps to the array
        for (int i = 0; i < mapListParent.transform.childCount; i++) {
            mapListArr.Add(mapListParent.transform.GetChild(i).gameObject);
            mapNamesArr.Add((i + 1) + " " + mapListArr[i].transform.GetChild(0).GetComponent<TMP_Text>().text);
        }

        // for (int j = 0; j < mapListArr.Count; j++) {
        //     mapNamesArr.Add(j + " " + mapListArr[j].GetComponent<TMP_Text>().text);
        // }

        DisplayCursor();
        DisplayTexts();
        DisplayMapImage();
    }


    private void DisplayCursor() {
        Instantiate(navigateSound);

        settingsCursor.transform.position = new Vector2(
            settingsCursor.transform.position.x,
            mapListArr[currentIndex].transform.position.y - 2
        );
    }


    private void DisplayTexts() {
        for (int i = 0; i < mapListArr.Count; i++) {
            if (i == currentIndex) {
                mapListArr[currentIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Colors.keyPaper;
            } else {
                mapListArr[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Colors.blue20;
            }
        }
    }


    private void DisplayMapImage() {
        selectedMapImage.sprite = mapListArr[currentIndex].transform.GetChild(1).GetComponent<Image>().sprite;
    }


    private void Update() {
        GetInput();
    }


    private void GetInput() {
        // UI navigation with the D-Pad buttons
        if (ReInput.players.GetPlayer(0).GetButtonDown("DPadUp")) {
            if (currentIndex > 0) {
                currentIndex--;

                DisplayCursor();
                DisplayTexts();
                DisplayMapImage();
            }
        }
        if (ReInput.players.GetPlayer(0).GetButtonDown("DPadDown")) {
            if (currentIndex < mapListArr.Count - 1) {
                currentIndex++;

                DisplayCursor();
                DisplayTexts();
                DisplayMapImage();
            }
        }

        if (ReInput.players.GetPlayer(0).GetButtonDown("X")) {
            Instantiate(selectSound);
            SettingsHolder.selectedMap = mapNamesArr[currentIndex];
            SceneManager.LoadScene("3 Character Selection");
        }

        if (ReInput.players.GetPlayer(0).GetButtonDown("Circle")) {
            Instantiate(cancelSound);
            SceneManager.LoadScene("1 Match Settings");
        }
    }

}
