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
				MeteorShot newMeteorShotScript = newProjectile.GetComponent<MeteorShot>();

				newProjectile.transform.GetChild(0).gameObject.tag = "Character" + charID;
				newMeteorShotScript.damagerID = charID;
				newProjectile.tag = "Attack";
				newMeteorShotScript.casterDamage = casterDamage;
				newMeteorShotScript.casterCritChance = casterCritChance;
				newMeteorShotScript.casterCritDMG = casterCritDMG;

				shotDelayTimer = shotDelayDefault;
				isShooting = true;
			}
		}
	}

}
