using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSurvivor : MonoBehaviour {
    
    private float levelDuration = 30.0f;

    private GameObject platformContainer;
    private List<GameObject> allPlatforms = new List<GameObject>();

    private float destroyCount;


    void Start ()
    {
        // Set level duration time
        LevelTimer.levelDurationCounter = levelDuration;

        // Attach the platform container to the gameobject
        platformContainer = GameObject.Find("Platforms");

        // Put every platform that is in the container in an Array/List
        foreach (Transform child in platformContainer.transform) {
            allPlatforms.Add(child.gameObject);
        }

        // Setting an initial destroyer countdown
        destroyCount = Random.Range(0.5f, 2.0f);
    }


    void Update ()
    {
        if (GameManager.enableModifier) {
            // Counting down to when to destroy the next platform
            destroyCount -= Time.deltaTime;

            // Destroying a random platform as long as there are still any left
            if (destroyCount <= 0 && allPlatforms.Count > 0) {
                int randomPlatform = Random.Range(0, allPlatforms.Count);
                Destroy(allPlatforms[randomPlatform]);
                allPlatforms.Remove(allPlatforms[randomPlatform]);

                // Setting a new destroyer countdown
                destroyCount = Random.Range(0.5f, 2.0f);
            }
        }
    }

}