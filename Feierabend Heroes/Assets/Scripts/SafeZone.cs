using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour {

	public SphereCollider safeZoneCol;
	private Light safeZoneLight;

	private float decreaseDuration = 10.0f;

	private float startSizeCol = 110.0f;
	private float endSizeCol = 7.0f;

	private float startSizeLight = 140.0f;
	private float endSizeLight = 20.0f;


	private void Update() {
		safeZoneCol.radius = Mathf.MoveTowards(startSizeCol, endSizeCol, decreaseDuration);
		safeZoneLight.spotAngle = Mathf.MoveTowards(startSizeLight, endSizeLight, decreaseDuration);

		// if (transform.localScale.x > minSizeCol) {
		// 	colliderGO.transform.localScale -= new Vector3(decreaseSpeedCol, 0, decreaseSpeedCol);
		// 	safeZoneLight.spotAngle -= decreaseSpeedLight;
		// }
	}

}
