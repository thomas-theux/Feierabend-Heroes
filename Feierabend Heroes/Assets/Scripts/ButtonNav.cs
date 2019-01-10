using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonNav : MonoBehaviour {

	public int indexID;

	public Image navUp;
	public Image navDown;
	public Image navLeft;
	public Image navRight;


	private void Awake() {
		string fullName = this.gameObject.name;
		string newName = fullName.Replace("Skill", "");
		indexID = int.Parse(newName);
	}

}
