using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingTendrils : MonoBehaviour {

    private void OnTriggerStay(Collider other) {
        if (other.tag.Contains("Character")) {
            if (other.GetComponent<CharacterSheet>().moveSpeed > 5.0f) {
                
            }
        }
    }

}
