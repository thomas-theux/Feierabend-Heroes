using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

	private void Awake() {
		StartCoroutine(LoadLevel());
	}


	IEnumerator LoadLevel() {
		yield return new WaitForSeconds(1.0f);
		SceneManager.LoadScene("2 TestLevel");
	}

}
