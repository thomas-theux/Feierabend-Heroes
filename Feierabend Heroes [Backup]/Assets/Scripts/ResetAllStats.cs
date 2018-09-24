using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAllStats : MonoBehaviour {

	public static bool resetAllStats;

	private int defaultHealth = 100;
	private float defaultAttackMin = 8;
	private float defaultAttackMax = 12;
	private int defaultDefense = 10;
	private float defaultSpeed = 10.0f;
	private int defaultLuck = 6;

	private float defaultJumpHeight = 2.0f;

	private int defaultStatPoints = 0;


	void Awake()
	{
		if (!resetAllStats) {
			for (int i = 0; i < 4; i++) {
				// Reset stats
				PlayerPrefs.SetInt("P" + i + "StatHealth", defaultHealth);
				PlayerPrefs.SetFloat("P" + i + "StatAttackMin", defaultAttackMin);
				PlayerPrefs.SetFloat("P" + i + "StatAttackMax", defaultAttackMax);
				PlayerPrefs.SetInt("P" + i + "StatDefense", defaultDefense);
				PlayerPrefs.SetFloat("P" + i + "StatSpeed", defaultSpeed);
				PlayerPrefs.SetInt("P" + i + "StatLuck", defaultLuck);

				// Reset jump height
				PlayerPrefs.SetFloat("P" + i + "JumpHeight", defaultJumpHeight);

				// Reset available stat points
				PlayerPrefs.SetInt("P" + i + "CurrentStatPoints", defaultStatPoints);
			}

			resetAllStats = true;
		}
	}

}
