using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbChest : MonoBehaviour {

	private bool openedChest = false;
	private int rndDoubleOrb;

	private void OnTriggerStay(Collider other) {

		if (other.tag.Contains("Character")) {
			CharacterMovement characterMovementScript = other.GetComponent<CharacterMovement>();
			CharacterSheet characterSheetScript = other.GetComponent<CharacterSheet>();

			if (characterMovementScript.activationBtn && !openedChest) {
				openedChest = true;
				
				// CHeck if RAGE MODE is on and on level 4
				if (characterSheetScript.rageModeOn && characterSheetScript.rageLevel >= 4) {
					rndDoubleOrb = -1;
				} else {
					rndDoubleOrb = Random.Range(1, 101);
				}

				// Check if player has activated the DOUBLE ORB skill
				if (rndDoubleOrb > characterSheetScript.doubleOrbChance) {
					characterSheetScript.currentOrbs += 1;
				} else {
					if (characterSheetScript.findThreeOrbs) {
						characterSheetScript.currentOrbs += 3;
					} else {
						characterSheetScript.currentOrbs += 2;
					}
				}

				Destroy(gameObject);
			}
		}

	}

}
