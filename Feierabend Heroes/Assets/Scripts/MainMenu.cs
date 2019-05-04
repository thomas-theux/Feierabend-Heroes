using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Rewired;

public class MainMenu : MonoBehaviour {

    public Image menuCursor;
    public RectTransform cursorSize;
    public GameObject[] menuItemsArr;

	public AudioSource navigateSound;
	public AudioSource selectSound;
	public AudioSource unavailableSound;

	private float minThreshold = 0.5f;
	private float maxThreshold = 0.5f;
	private bool axisYActive;

    private int currentIndex = 0;

    private Color32 transparentBlue = new Color32(
        Colors.blue20.r,
        Colors.blue20.g,
        Colors.blue20.b,
        128
    );
    private Color32 transparentPaper = new Color32(
        Colors.keyPaper.r,
        Colors.keyPaper.g,
        Colors.keyPaper.b,
        128
    );


    private void Awake() {
        // DisplayCursor();
        DisplayTexts();
    }


    private void DisplayCursor() {
        Instantiate(navigateSound);

        menuCursor.transform.position = new Vector2(
            menuCursor.transform.position.x,
            menuItemsArr[currentIndex].transform.position.y - 2
        );

        switch (currentIndex) {
            case 0:
            case 1:
                cursorSize.sizeDelta = new Vector2(600, 70);
                break;
            case 2:
            case 3:
                cursorSize.sizeDelta = new Vector2(600, 56);
                break;
        }
    }


    private void DisplayTexts() {
        for (int i = 0; i < menuItemsArr.Length; i++) {
            if (i == currentIndex) {
                if (i == 1 || i == 2 || i == 3) {
                    menuItemsArr[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = transparentPaper;
                } else {
                    menuItemsArr[currentIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Colors.keyPaper;
                }
            } else {
                if (i == 1 || i == 2 || i == 3) {
                    menuItemsArr[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = transparentBlue;
                } else {
                    menuItemsArr[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Colors.blue20;
                }
            }
        }
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
            }
        }
        if (ReInput.players.GetPlayer(0).GetButtonDown("DPadDown")) {
            if (currentIndex < menuItemsArr.Length - 1) {
                currentIndex++;

                DisplayCursor();
                DisplayTexts();
            }
        }

        // UI navigation with the analog sticks
        // UP
        if (ReInput.players.GetPlayer(0).GetAxis("LS Vertical") > maxThreshold && !axisYActive) {
            axisYActive = true;

            if (currentIndex > 0) {
                currentIndex--;

                DisplayCursor();
                DisplayTexts();
            }
        }
        // DOWN
        if (ReInput.players.GetPlayer(0).GetAxis("LS Vertical") < -maxThreshold && !axisYActive) {
            axisYActive = true;

            if (currentIndex < menuItemsArr.Length - 1) {
                currentIndex++;

                DisplayCursor();
                DisplayTexts();
            }
        }

        if (ReInput.players.GetPlayer(0).GetAxis("LS Vertical") <= minThreshold && ReInput.players.GetPlayer(0).GetAxis("LS Vertical") >= -minThreshold) {
            axisYActive = false;
        }

        if (ReInput.players.GetPlayer(0).GetButtonDown("X")) {
            switch (currentIndex) {
                case 0:
                    SceneManager.LoadScene("1 Match Settings");
                    Instantiate(selectSound);
                    break;
                case 1:
                    Instantiate(unavailableSound);
                    break;
                case 2:
                    Instantiate(unavailableSound);
                    break;
                case 3:
                    Instantiate(unavailableSound);
                    break;
            }
        }
    }

}
