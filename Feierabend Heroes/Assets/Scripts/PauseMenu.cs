using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public GameObject pauseMenu;
	private GameObject newPauseMenu;

	private bool enablePauseMenu;

	void Update()
	{
		if (!enablePauseMenu) {
			if (Input.GetButtonDown(InputScript.gamepadInput[0, 9])) {
				newPauseMenu = Instantiate(pauseMenu, Vector3.zero, Quaternion.identity);
				Time.timeScale = 0;
				enablePauseMenu = true;
			}
		} else {
			if (Input.GetButtonDown(InputScript.gamepadInput[0, 9])) {
				Destroy(newPauseMenu);
				Time.timeScale = 1;
				enablePauseMenu = false;
			}
		}
	}

}
