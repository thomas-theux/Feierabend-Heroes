using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

	private int maxHealth = 100;
	private int currentHealth;


	void Start ()
	{
		currentHealth = maxHealth;
	}

	public void getHit (int damage)
	{
		currentHealth -= damage;
	}

	void Update ()
	{
		if (currentHealth <= 0) {
			gameObject.SetActive(false);
		}
	}

}
