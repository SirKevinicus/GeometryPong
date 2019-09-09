using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjBoundary : MonoBehaviour {

	//REFERENCES
	Camera cam;

	//VARS
	private float xMin, xMax, yAxis;
	private float ySpawn;
	float screen_height;
 	float screen_width;

//////////////////////////////////////////////////////////////////

	//Initialize the Camera Data
	void Awake(){
		cam = Camera.main;
		screen_height = 2f * cam.orthographicSize;
		screen_width = screen_height * cam.aspect;
	}

	public void SetNewBounds(){
		cam = Camera.main;
		screen_height = 2f * cam.orthographicSize;
		screen_width = screen_height * cam.aspect;
	}
	
	//Accepts any object with a SpriteRenderer
	//Returns the lowest x value it can be at without going off the screen
	public float getxMin(GameObject obj) {
		SpriteRenderer objRend = obj.GetComponent<SpriteRenderer>();
		float objExtentX = objRend.bounds.extents.x;	//Gets the object's sprite radius
		xMin = -1 * ((screen_width / 2f) - objExtentX);
		return xMin;
	}

	//Accepts any object with a SpriteRenderer
	//Returns the highest x value it can be at without going off the screen
	public float getxMax(GameObject obj) {
		SpriteRenderer objRend = obj.GetComponent<SpriteRenderer>();
		float objExtentX = objRend.bounds.extents.x;
		xMax = ((screen_width / 2f) - objExtentX);
		return xMax;
	}

	//Returns the y value the paddle moves on
	public float getPaddleYAxis() {
		SetNewBounds();
		yAxis = (-3 * (screen_height / 9f));
		return yAxis;
	}

	public float getBallYAxis() {
		yAxis = (1 * (screen_height / 1f));
		return yAxis;
	}

	public float getCamSize(){
		return cam.orthographicSize;
	}

	public float getScreenWidth(){
		return screen_width;
	}

	public float getScreenHeight(){
		return screen_height;
	}

}
