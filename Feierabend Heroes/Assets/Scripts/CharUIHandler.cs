using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharUIHandler : MonoBehaviour {

	private Slider HPGauge;
	private CharacterSheet characterSheetScript;


	public void InitializeCharUI() {
		HPGauge = transform.GetChild(transform.childCount-1).GetComponent<Slider>();
		characterSheetScript = transform.parent.GetComponent<CharacterSheet>();
	}


	private void Update() {
		HPGauge.maxValue = characterSheetScript.maxHealth;
		HPGauge.value = characterSheetScript.currentHealth;
	}

}
