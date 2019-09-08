using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScript : MonoBehaviour {

	GameObject theBall;

	// Use this for initialization
	void Awake(){
		StartCoroutine("WaitThenDestroy");
	}
	
	IEnumerator WaitThenDestroy(){
		yield return new WaitForSeconds(.2f);
		Destroy(this.gameObject);
	}
}
