using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

	private int getCharID;

	public float characterHealth = 100;
	public float characterAttackMin = 8;
	public float characterAttackMax = 12;
	public int characterDefense = 10;
	public float characterSpeed = 10.0f;
	public int characterLuck = 6;

	public float characterJumpHeight = 2.0f;

	public int currentStatPoints;


	void Awake()
	{
		getCharID = GetComponent<CharacterMovement>().charID;

		// characterHealth = PlayerPrefs.GetFloat("P" + getCharID + "StatHealth");
		// characterAttackMin = PlayerPrefs.GetFloat("P" + getCharID + "StatAttackMin");
		// characterAttackMax = PlayerPrefs.GetFloat("P" + getCharID + "StatAttackMax");
		// characterDefense = PlayerPrefs.GetInt("P" + getCharID + "StatDefense");
		// characterSpeed = PlayerPrefs.GetFloat("P" + getCharID + "StatSpeed");
		// characterLuck = PlayerPrefs.GetInt("P" + getCharID + "StatLuck");
		// currentStatPoints = PlayerPrefs.GetInt("P" + getCharID + "CurrentStatPoints");
	}

}