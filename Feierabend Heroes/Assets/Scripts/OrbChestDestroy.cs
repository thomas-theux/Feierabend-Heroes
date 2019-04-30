using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbChestDestroy : MonoBehaviour {

    // public GameObject orbChestSpawnerGO;


    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Orb" || other.tag == "Apple") {
            Destroy(other.gameObject);
        }
    }

}
