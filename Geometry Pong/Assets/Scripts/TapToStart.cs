using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TapToStart : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0){
			CastRay();
		}
		if(Input.GetMouseButtonDown(0)){
			CastRay();
		}
		
	}

	void CastRay(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);

			if (hit) {
				Debug.Log(hit.transform.name);
				if (hit.transform.name == this.name){
					SceneManager.LoadScene("Game");
				}
			}
	}
}
