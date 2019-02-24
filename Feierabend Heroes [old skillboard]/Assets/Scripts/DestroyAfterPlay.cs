using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterPlay : MonoBehaviour {

	private float clipLength;


	private void Awake() {
		clipLength = GetComponent<AudioSource>().clip.length;
		Destroy(this.gameObject, clipLength + 0.5f);
	}

}
