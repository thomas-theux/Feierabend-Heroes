using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSurvivor : MonoBehaviour {

	public GameObject platform;

	public int platformCount;
	private int maxPlatformsZ;
	private int[] disappearArr;
	private Vector3 randomPos;

	private int posX;
	private int posZ;


	void Start ()
	{
		maxPlatformsZ = Random.Range(5, 8);

		for (int j = 0; j < maxPlatformsZ; j++) {

			platformCount = Random.Range(5, 8);

			for (int i = 0; i < platformCount; i++) {
				posX = Random.Range(posX+6, posX+14);
				posZ = 6;
				randomPos = new Vector3(posX, 0f, posZ*j);
				Instantiate(platform, randomPos, Quaternion.identity);

				if (i == platformCount-1) {
					posX = 0;
				}
			}

		}	
	}


}
