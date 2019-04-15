using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbChestDestroy : MonoBehaviour {

    // public GameObject orbChestSpawnerGO;


    private void OnTriggerStay(Collider other) {
        if (other.tag == "Orb" || other.tag == "Apple") {

            if (other.tag == "Orb") {
                OrbChestSpawner.currentChestCount--;
            } else if (other.tag == "Apple") {
                AppleSpawner.currentAppleCount--;
            }
            
            Destroy(other.gameObject);
        }
    }

}
