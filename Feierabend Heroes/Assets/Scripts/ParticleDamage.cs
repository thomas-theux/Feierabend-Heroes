using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDamage : MonoBehaviour {

	void OnParticleCollision(GameObject other) {

		if (other.tag != "Attack" && other.tag != "Apple" && other.tag != "Orb" && other.tag != "Environment") {
       		other.GetComponent<CharacterSheet>().currentHealth -= other.GetComponent<CharacterSheet>().currentHealth * 0.1f;
		}

    }

}
