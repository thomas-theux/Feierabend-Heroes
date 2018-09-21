using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpawner : MonoBehaviour {

	public GameObject statsUI;
	public GameObject UICursor;

	// public static List<float> textYPos = new List<float>();


	public void SpawnUI()
	{
		for (int i = 0; i < GameManager.playerCount; i++) {

			// Instantiate UI text for stats
			GameObject newUI = Instantiate(statsUI, statsUI.transform.position, statsUI.transform.rotation);
			newUI.GetComponent<Canvas>().worldCamera = GameObject.Find("Camera0" + i + "(Clone)").GetComponent<Camera>();
			newUI.name = "StatsUI" + i;

			// Go through positions of the children and write them in a list
			// for (int j = 0; j < newUI.transform.childCount -1; j++) {
			// 	textYPos.Add(newUI.transform.GetChild(j).transform.localPosition.y);
			// }

			// Instantiate the cursor at the position of the first child
			GameObject newCursor = Instantiate(UICursor, UICursor.transform.position, UICursor.transform.rotation);
			newCursor.transform.SetParent(newUI.transform, false);
			newCursor.transform.SetAsFirstSibling();
			newCursor.name = "Cursor" + i;
			newCursor.GetComponent<CursorController>().cursorID = i;

		}
	}

}
