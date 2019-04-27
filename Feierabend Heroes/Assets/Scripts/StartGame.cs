using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class StartGame : MonoBehaviour {

    private void Awake() {
        // Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;

        SceneManager.LoadScene("1 Match Settings");

    }

}