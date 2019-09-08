using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveableObject : MonoBehaviour {

	ObjBoundary bounds;
	public float xMin;
	public float xMax;
	public bool isMoveable;

	void Awake(){
	}
	protected void SetSize(){
        bounds = GameObject.Find("Game Master").GetComponent<ObjBoundary>();
		Vector3 oldSize = this.transform.localScale;
		float s = bounds.getCamSize();
        this.transform.localScale = new Vector3(bounds.getScreenWidth()/s * oldSize.x, bounds.getScreenWidth()/s *oldSize.y , 1);
    }

	protected void CalcBounds(GameObject obj){
		bounds = GameObject.Find("Game Master").GetComponent<ObjBoundary>();
		xMin = bounds.getxMin(obj);
		xMax = bounds.getxMax(obj);
	}

	public void NoMove(){
		isMoveable = false;
	}

	public void CanMove(){
		isMoveable = true;
	}
}
