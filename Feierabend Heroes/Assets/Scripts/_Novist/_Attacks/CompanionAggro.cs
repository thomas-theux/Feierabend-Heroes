using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionAggro : MonoBehaviour {

	// private float speed = 2.0f;
	public Transform followCaster;
	private Transform enemyChar;
	private float followSpeed = 3.0f;
	private float approachEnemySpeed = 2.0f;

	private float lifeTime = 10.0f;

	private Vector3 offset;

	private bool enemyDetected = false;
	private float distanceToChar = 1.4f;


	private void Awake() {
		StartCoroutine(LifeTime());
	}


	private void OnTriggerEnter(Collider other) {
		if (other.tag != "Attack" && other.tag != "Apple" && other.tag != "Orb" && other.tag != "Environment" && other.tag != transform.parent.GetChild(0).tag) {
			enemyDetected = true;
			enemyChar = other.gameObject.transform;
			StartCoroutine(FollowTimer());
		}
	}


	private void OnTriggerExit(Collider other) {
		if (other.tag != "Attack" && other.tag != "Apple" && other.tag != "Orb" && other.tag != "Environment" && other.tag != transform.parent.GetChild(0).tag) {
			enemyDetected = false;
		}
	}


	private void FixedUpdate() {
		if (!enemyDetected) {
			if (Vector3.Distance(followCaster.position, transform.parent.position) > distanceToChar) {
				Vector3 desiredPos = new Vector3(followCaster.localPosition.x, followCaster.localPosition.y, followCaster.localPosition.z);
				Vector3 smoothedPos = Vector3.Lerp(transform.parent.position, desiredPos, followSpeed * Time.deltaTime);
				
				transform.parent.position = smoothedPos;
			}
		} else {
			if (Vector3.Distance(enemyChar.position, transform.parent.position) > distanceToChar) {
				Vector3 desiredPos = new Vector3(enemyChar.localPosition.x, enemyChar.localPosition.y, enemyChar.localPosition.z);
				Vector3 smoothedPos = Vector3.Lerp(transform.parent.position, desiredPos, approachEnemySpeed * Time.deltaTime);
				
				transform.parent.position = smoothedPos;
			}
		}
	}


	private IEnumerator LifeTime() {
		yield return new WaitForSeconds(lifeTime);
		Destroy(transform.parent.gameObject);
	}


	private IEnumerator FollowTimer() {
		yield return new WaitForSeconds(3.0f);
		enemyDetected = false;
	}

}
