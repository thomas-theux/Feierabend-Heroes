using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadManager : MonoBehaviour {

    // All dontDestroyOnLoad game objects in one array
    public static List<GameObject> allDontDestroyOnLoadGOs = new List<GameObject>();


    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }


    // If this game object is being destroyed, it destroys all other dontDestroyOnLoad game objects
    private void OnDestroy() {
        for (int i = 0; i < allDontDestroyOnLoadGOs.Count; i++) {
            Destroy(allDontDestroyOnLoadGOs[i]);
        }
    }

}
