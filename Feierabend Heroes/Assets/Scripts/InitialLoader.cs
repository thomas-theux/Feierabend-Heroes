using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InitialLoader : MonoBehaviour {

	void Start()
	{
		// Get the selected Mode and Modifier
		string selectedMode = GameSelector.selectedMode;
		string selectedModifier = GameSelector.selectedModifier;
		// Remove whitespaces so Unity can find the scripts
		string attachMode = selectedMode.Replace(" ", "");
		string attachModifier = selectedModifier.Replace(" ", "");
		// Attach them to the LevelManager
		gameObject.AddComponent(Type.GetType(attachMode));
		gameObject.AddComponent(Type.GetType(attachModifier));

		// Attach Timer script to the LevelManager
		gameObject.AddComponent<LevelTimer>();
	}
}


