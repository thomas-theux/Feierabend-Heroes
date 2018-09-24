using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggering : MonoBehaviour {

	void Update()
	{
		transform.localScale -= new Vector3(0.03f, 0, 0.03f);
	}

	void OnTriggerExit(Collider other)
	{
		print("exited");
		Destroy(other.gameObject);
	}

}
