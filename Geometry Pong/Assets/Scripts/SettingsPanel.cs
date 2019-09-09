using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanel : MonoBehaviour {

	GameObject panel;
	bool isEnabled;

	// Use this for initialization
	void Start () {
		panel = this.GetComponent<GameObject>();
		gameObject.SetActive(false);
		isEnabled = false;
	}

	// Update is called once per frame
	void Update () {

	}

	public void ToggleSettingsPanel(){
		if(isEnabled){
			isEnabled = false;
		} else{
			isEnabled = true;
		}
	}
}
