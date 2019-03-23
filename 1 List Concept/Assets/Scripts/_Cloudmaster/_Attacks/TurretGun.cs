using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGun : MonoBehaviour {

	private float lifeTime = 16.0f;
	public int charID;

	public float casterDamage = 0;
	public float casterCritChance = 0;
	public float casterCritDMG = 0;
	public int damagerID;

	public GameObject projectileGO;
	public Transform projectileSpawner;

	private bool isShooting = false;
	public float shotDelayDefault = 0.5f;
	private float shotDelayTimer;


	private void Start() {
		Destroy(transform.parent.gameObject, lifeTime);
	}


	private void OnTriggerStay(Collider other) {
		if (other.tag != "Attack" && other.tag != "Apple" && other.tag != "Orb" && other.tag != "Environment" && other.tag != transform.parent.GetChild(0).tag) {
			transform.parent.transform.LookAt(other.transform);

			if (isShooting) {
				shotDelayTimer -= Time.deltaTime;
				if (shotDelayTimer <= 0) {
					isShooting = false;
				}
			} else {
				GameObject newProjectile = Instantiate(projectileGO, projectileSpawner.position, projectileSpawner.rotation);
				newProjectile.transform.GetChild(0).gameObject.tag = "Character" + charID;
				newProjectile.GetComponent<MeteorShot>().damagerID = charID;
				newProjectile.tag = "Attack";
				newProjectile.transform.GetComponent<MeteorShot>().casterDamage = casterDamage;
				newProjectile.GetComponent<MeteorShot>().casterCritChance = casterCritChance;
				newProjectile.GetComponent<MeteorShot>().casterCritDMG = casterCritDMG;

				shotDelayTimer = shotDelayDefault;
				isShooting = true;
			}
		}
	}

}
