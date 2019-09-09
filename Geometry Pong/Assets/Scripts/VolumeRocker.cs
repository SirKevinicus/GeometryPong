using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeRocker : MonoBehaviour {

	public GameObject volumeNode;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.touchCount == 1){
			Touch touch = Input.GetTouch(0);
			Vector2 touchPos = touch.position;
			Ray ray = Camera.main.ScreenPointToRay(touchPos);
			RaycastHit hit;
      		if(Physics.Raycast(ray.origin, ray.direction, out hit)){
        		if(hit.transform.name == "volume"){
					//as long as the finger is down
            		while(touch.phase != TouchPhase.Ended){
						volumeNode.transform.position = touchPos;
					}
          		}
      		}
		}
	}
}
