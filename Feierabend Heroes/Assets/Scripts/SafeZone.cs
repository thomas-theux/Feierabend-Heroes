using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour {

	private Light safeZoneLight;
	public CapsuleCollider safeZoneCol;

	public float decreaseDuration;
	private float t = 0;

	private float startSizeLight = 274.0f;
	private float endSizeLight = 18.0f;

	private float startSizeCol = 110.0f;
	private float endSizeCol = 7.0f;


	private void Awake() {
		safeZoneLight = GetComponent<Light>();
		StartCoroutine(DecreaseSize());
	}


	private IEnumerator DecreaseSize() {
		while (t < decreaseDuration) {
			t += Time.deltaTime;
			safeZoneLight.cookieSize = Mathf.Lerp(startSizeLight, endSizeLight, t / decreaseDuration);
			safeZoneCol.radius = Mathf.Lerp(startSizeCol, endSizeCol, t / decreaseDuration);

			yield return null;
		}
	}

}