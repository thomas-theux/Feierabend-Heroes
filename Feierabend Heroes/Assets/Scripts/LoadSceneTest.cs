using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneTest : MonoBehaviour {

    private bool isReloading = false;
    private int testValue;


    private void Update() {
        this.gameObject.transform.position += new Vector3(0, 0, 0.1f);

        testValue++;
        // print(testValue);

        if (this.gameObject.transform.position.z > 10.0f && !isReloading) {
            isReloading = true;

            SceneManager.LoadScene("ReloadSceneTest");
        }
    }
}
