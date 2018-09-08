using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class GameSelector : MonoBehaviour {

	private List<string> gameMaps = new List<string>();
	private List<string> gameModes = new List<string>();
	private List<string> gameModifiers = new List<string>();

	public Text displayedMap;
	public Text displayedMode;
	public Text displayedModifier;

	public static string selectedMap;
	public static string selectedMode;
	public static string selectedModifier;

	private int randomMap;
	private int randomMode;
	private int randomModifier;

	private float sceneSwitchTimer = 2.0f;


	void Start ()
	{
		// Get every Map located inside the Maps folder and push it into a List
		string mapsFolder = Application.dataPath + "/Scenes/Maps";
		var mapsDirInfo = new DirectoryInfo(mapsFolder);
		var allMapsFileInfos = mapsDirInfo.GetFiles("*.unity", SearchOption.AllDirectories);
		foreach (var fileInfo in allMapsFileInfos) {
			gameMaps.Add(Path.GetFileNameWithoutExtension(@fileInfo.Name));
		}

		// Get every Game Mode located inside the Modes folder and push it into a List
		string modesFolder = Application.dataPath + "/Scripts/Modes";
		var modesDirInfo = new DirectoryInfo(modesFolder);
		var allModesFileInfos = modesDirInfo.GetFiles("*.cs", SearchOption.AllDirectories);
		foreach (var fileInfo in allModesFileInfos) {
			gameModes.Add(Path.GetFileNameWithoutExtension(@fileInfo.Name));
		}

		// Get every Modifier located inside the Modifiers folder and push it into a List
		string modifiersFolder = Application.dataPath + "/Scripts/Modifiers";
		var modifiersDirInfo = new DirectoryInfo(modifiersFolder);
		var allModifiersFileInfos = modifiersDirInfo.GetFiles("*.cs", SearchOption.AllDirectories);
		foreach (var fileInfo in allModifiersFileInfos) {
			gameModifiers.Add(Path.GetFileNameWithoutExtension(@fileInfo.Name));
		}

		// Randomly select one of the existing Maps, Modes and Modifiers
		randomMap = Random.Range(0, gameMaps.Count);
		randomMode = Random.Range(0, gameModes.Count);
		randomModifier = Random.Range(0, gameModifiers.Count);

		// Save selected Map, Mode and Modifier in public variable
		selectedMap = gameMaps[randomMap];
		selectedMode = gameModes[randomMode];
		selectedModifier = gameModifiers[randomModifier];

		// Display the selected Map, Mode and Modifier in the UI
		displayedMap.text = selectedMap.ToUpper();
		displayedMode.text = selectedMode;
		displayedModifier.text = selectedModifier;
	}


	void Update()
	{
		sceneSwitchTimer -= Time.deltaTime;

		if (sceneSwitchTimer <= 0) {
			sceneSwitchTimer = 0;
			SceneManager.LoadScene(selectedMap);
		}
	}

}
