using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour {

	private float decreaseSpeed = 0.02f;
	private float minSize = 30.0f;


	private void Update() {
		if (transform.localScale.x > minSize) {
			transform.localScale -= new Vector3(decreaseSpeed, 0, decreaseSpeed);
		}
	}

}
