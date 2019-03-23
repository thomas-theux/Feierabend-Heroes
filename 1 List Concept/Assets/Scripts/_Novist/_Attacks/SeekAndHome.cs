using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekAndHome : MonoBehaviour {

	private float smoothSpeed = 0f;


	private void Awake() {
		smoothSpeed = transform.parent.GetComponent<MeteorShot>().moveSpeed;
	}

	private void OnTriggerStay(Collider other) {
		if (other.tag != "Attack" && other.tag != "Apple" && other.tag != "Orb" && other.tag != "Environment" && other.tag != this.tag) {

			Quaternion desiredRotation = Quaternion.LookRotation(other.transform.position - transform.parent.transform.position);
			transform.parent.transform.rotation = Quaternion.RotateTowards(transform.parent.transform.rotation, desiredRotation, smoothSpeed * Time.deltaTime);

		}
	}

}
