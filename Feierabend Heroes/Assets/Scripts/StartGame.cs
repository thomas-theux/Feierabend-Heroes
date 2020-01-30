using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class StartGame : MonoBehaviour {

    private void Awake() {
        Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;

        Application.targetFrameRate = 60;

        SceneManager.LoadScene("0 Main Screen");
    }

}