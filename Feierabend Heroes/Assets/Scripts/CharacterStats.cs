using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

	public int characterHealth = 100;
	public float characterAttackMin = 8;
	public float characterAttackMax = 12;
	public int characterDefense = 10;
	public float characterSpeed = 10.0f;
	public int characterLuck = 1;

	public float characterJumpHeight = 2.0f;

	public int currentStatPoints;


	void Start()
	{
		currentStatPoints = 0;
	}

}
