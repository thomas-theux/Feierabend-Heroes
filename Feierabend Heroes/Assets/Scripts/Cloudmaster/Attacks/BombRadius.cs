using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRadius : MonoBehaviour {

	private Vector3 desiredScale;
	private float destroyScale;

	// These variables can be improved by advancing on the skill tree
	private float smoothSpeed = 8.0f;
	private float maxRadius = 10.0f;


	private void Start() {
		desiredScale = new Vector3(maxRadius, maxRadius, maxRadius);
		destroyScale = maxRadius - 0.1f;
	}


	private void Update() {
		
		Vector3 smoothedScale = Vector3.Lerp(transform.localScale, desiredScale, smoothSpeed * Time.deltaTime);
		
		transform.localScale = smoothedScale;

		if (transform.localScale.x >= destroyScale) {
			Destroy(gameObject);
		}
	}

}
