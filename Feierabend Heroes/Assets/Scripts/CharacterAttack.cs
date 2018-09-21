using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour {

	public GameObject projectile;
	public Transform shotSpawner;

	private float fireRate = 0.1f;
	private float nextFire = 0.0f;


	public void shootProjectile ()
	{
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;

			// Give damage values from character stats sheet
			projectile.GetComponent<ProjectileMover>().damageMin = GetComponent<CharacterStats>().characterAttackMin;
			projectile.GetComponent<ProjectileMover>().damageMax = GetComponent<CharacterStats>().characterAttackMax;
			projectile.GetComponent<ProjectileMover>().characterLuck = GetComponent<CharacterStats>().characterLuck;
			
			Instantiate(projectile, shotSpawner.position, shotSpawner.rotation);
		}
	}

}
