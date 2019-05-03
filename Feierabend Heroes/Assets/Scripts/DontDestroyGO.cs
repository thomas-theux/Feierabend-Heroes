using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyGO : MonoBehaviour {

    private void Awake() {
        DontDestroyOnLoadManager.allDontDestroyOnLoadGOs.Add(this.gameObject);
    }

}
