﻿using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform if null.
	private Transform camTransform;
	
	// How long the object should shake for.
	private float shakeDurationDefault = 0.2f;
	private float shakeDuration;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.2f;
	private float decreaseFactor = 1.0f;
	
	private Vector3 originalPos;
	
	void Awake() {
		if (camTransform == null) {
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}
	
	void OnEnable() {
		shakeDuration = shakeDurationDefault;
		// originalPos = camTransform.localPosition;
	}

	void Update() {
		originalPos = camTransform.localPosition;
		
		if (shakeDuration > 0) {
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			
			shakeDuration -= Time.deltaTime * decreaseFactor;
		} else {
			// shakeDuration = 0f;
			camTransform.localPosition = originalPos;
			this.enabled = false;
		}
	}
}