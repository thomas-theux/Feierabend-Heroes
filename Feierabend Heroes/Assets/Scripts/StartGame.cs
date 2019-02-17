using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class StartGame : MonoBehaviour {

    private List<int> testList = new List<int>();
    private int highestValue = 0;
    private int lowestValue = 1000;

    private void Awake() {
        print("Laden");

        SceneManager.LoadScene("1 Character Selection");

        // testList.Add(11);
        // testList.Add(15);
        // testList.Add(14);
        // testList.Add(9);

        // int lowestValue = testList.Min();
        // int highestValue = testList.Max();

        // int highestIndex = testList.IndexOf(highestValue);
        // int lowestIndex = testList.IndexOf(lowestValue);

        // print(highestIndex);
        // print(lowestIndex);
    }

}
