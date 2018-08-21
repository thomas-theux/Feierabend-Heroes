using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

	public GameObject projectile;
	public Transform shotSpawner;

	private float fireRate = 0.3f;
	private float nextFire = 0.0f;


	public void shootProjectile ()
	{
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(projectile, shotSpawner.position, shotSpawner.rotation);
		}
	}

}
