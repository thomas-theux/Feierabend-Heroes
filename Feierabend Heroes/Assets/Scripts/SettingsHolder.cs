using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsHolder : MonoBehaviour{

	public static int[] playerClasses = {-1, -1, -1, -1};
	// public static int[] playerClasses = {0, 0, 0, 0};
	public static int playerCount = 1;

	public static List<GameObject> characterArr = new List<GameObject>();
	public static List<Camera> camArr = new List<Camera>();
	public static List<GameObject> skillUIArr = new List<GameObject>();
	public static List<GameObject> charUIArr = new List<GameObject>();

	public static bool initialSpawn = false;
	public static bool initializeUI = false;

}