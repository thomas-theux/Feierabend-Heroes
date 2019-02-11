using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

	private CharacterSheet characterSheetScript;
	public Image healthBarImage;
	public Image currentHealthImage;

	public Image attackOneImage;
	public Image attackTwoImage;
	public Image skillImage;
	public Image[] coolDownImage;

	public float attackOneDelayTimer;
	public float attackTwoDelayTimer;
	public float skillOneDelayTimer;
	public float skillTwoDelayTimer;
	
	public float smoothSpeed;

	private bool lowHealth = false;

	
	public void InitializeCharUI() {
		characterSheetScript = transform.root.GetComponent<CharacterSheet>();

		smoothSpeed = 8.0f;
	}


	private void Update() {
		DisplayCoolDown();

		DisplayCurrentHP();
	}


	private void DisplayCoolDown() {
		// Show cooldown for Attack One
		if (attackOneDelayTimer > 0) {
			float currentTime = attackOneDelayTimer / characterSheetScript.delayAttackOne;
			coolDownImage[0].fillAmount = currentTime;
		} else {
			coolDownImage[0].fillAmount = 0;
		}
		
		// Show cooldown for Attack Two
		if (attackTwoDelayTimer > 0) {
			float currentTime = attackTwoDelayTimer / characterSheetScript.delayAttackTwo;
			coolDownImage[1].fillAmount = currentTime;
		} else {
			coolDownImage[1].fillAmount = 0;
		}
		
		// Show cooldown for Skill One
		if (skillOneDelayTimer > 0) {
			float currentTime = skillOneDelayTimer / characterSheetScript.delaySkillOne;
			coolDownImage[2].fillAmount = currentTime;
		} else {
			coolDownImage[2].fillAmount = 0;
		}
		
		// Show cooldown for Skill Two
		if (skillTwoDelayTimer > 0) {
			float currentTime = skillTwoDelayTimer / characterSheetScript.delaySkillTwo;
			coolDownImage[2].fillAmount = currentTime;
		} else {
			coolDownImage[2].fillAmount = 0;
		}
	}


	private void DisplayCurrentHP() {
		float desiredHP = characterSheetScript.currentHealth / characterSheetScript.maxHealth;
		float smoothedHP = Mathf.Lerp(currentHealthImage.fillAmount, desiredHP, smoothSpeed * Time.deltaTime);
		currentHealthImage.fillAmount = smoothedHP;

		if (currentHealthImage.fillAmount <= 0.2f && !lowHealth) {
			lowHealth = true;
			healthBarImage.color = new Color32(255, 0, 0, 255);
			currentHealthImage.color = new Color32(255, 0, 0, 255);
		} else if (currentHealthImage.fillAmount > 0.2f && lowHealth) {
			lowHealth = false;
			healthBarImage.color = new Color32(255, 255, 255, 255);
			currentHealthImage.color = new Color32(255, 255, 255, 255);
		}
	}

}
