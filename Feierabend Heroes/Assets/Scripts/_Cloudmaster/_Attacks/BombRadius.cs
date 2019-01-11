using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRadius : MonoBehaviour {

	public float bombDamage = 0;

	private Vector3 desiredScale;
	private float destroyScale;

	private bool didDamage;

	// These variables can be improved by advancing on the skill tree
	private float smoothSpeed = 8.0f;
	private float maxRadius = 10.0f;


	private void Start() {
		desiredScale = new Vector3(maxRadius, maxRadius, maxRadius);
		destroyScale = maxRadius - 0.1f;
	}


	private void OnTriggerEnter(Collider other) {
		if (!didDamage) {
			if (other.tag != "Attack" && other.tag != "Environment" && other.tag != transform.GetChild(0).tag) {
				CalculateDamage(other);
			}
		}
	}


	private void CalculateDamage(Collider other) {
		didDamage = true;

		float dealDamage = 0;
		float victimDefense = other.gameObject.GetComponent<CharacterSheet>().charDefense;

		dealDamage = bombDamage - ((victimDefense / 100) * bombDamage);
		other.gameObject.GetComponent<CharacterSheet>().currentHealth -= dealDamage;
	}


	private void Update() {
		Vector3 smoothedScale = Vector3.Lerp(transform.localScale, desiredScale, smoothSpeed * Time.deltaTime);
		
		transform.localScale = smoothedScale;

		if (transform.localScale.x >= destroyScale) {
			Destroy(gameObject);
		}
	}

}
