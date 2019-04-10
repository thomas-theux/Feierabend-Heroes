using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour {

	private Light safeZoneLight;
	public CapsuleCollider safeZoneCol;

	private float decreaseDuration;
	private float t = 0;

	private float startSizeLight = 320.0f;
	private float endSizeLight = 18.0f;

	private float startSizeCol = 128.0f;
	private float endSizeCol = 7.0f;

	private bool startDecreasing = false;


	private void Awake() {
		safeZoneLight = GetComponent<Light>();
		startDecreasing = false;
		decreaseDuration = SettingsHolder.battleTime;
	}


	private void Update() {
		if (TimeHandler.startBattle && !startDecreasing) {
			StartCoroutine(DecreaseSize());
		}
	}


	private IEnumerator DecreaseSize() {
		startDecreasing = true;
		
		while (t < decreaseDuration) {
			t += Time.deltaTime;
			safeZoneLight.cookieSize = Mathf.Lerp(startSizeLight, endSizeLight, t / decreaseDuration);
			safeZoneCol.radius = Mathf.Lerp(startSizeCol, endSizeCol, t / decreaseDuration);

			if (TimeHandler.startBattle) {
				yield return null;
			} else {
				yield break;
			}
		}
	}

}