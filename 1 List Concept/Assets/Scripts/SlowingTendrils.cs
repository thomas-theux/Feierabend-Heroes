﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingTendrils : MonoBehaviour {

    private float slowDownSpeed = 1.6f;


    private void OnTriggerEnter(Collider other) {
        if (other.tag.Contains("Character") && other.tag != this.gameObject.transform.parent.tag) {
            other.GetComponent<CharacterSheet>().moveSpeed /= slowDownSpeed;
        }
    }


    private void OnTriggerExit(Collider other) {
        if (other.tag.Contains("Character") && other.tag != this.gameObject.transform.parent.tag) {
            other.GetComponent<CharacterSheet>().moveSpeed *= slowDownSpeed;
        }
    }

}