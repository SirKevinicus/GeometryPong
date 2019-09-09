using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : MonoBehaviour {
    
	private GameMaster gm;
	private BallMaster bm;
	public float targetOrtho;
	Camera cam;
	ObjBoundary bounds;
	public Transform targetTransform;
    
	public float smoothSpeed = 3.0f;
	public float zoomSpeed = 1.0f;
	
	//Zoomed In On Paddle
	public float paddleFocusOrtho = 5.0f;
	public Vector3 paddleFocusPos = new Vector3(0,-15,-10);
	
	//Standard View for 1Paddle
	public float stdOrtho = 30.0f;
	public Vector3 stdPos = new Vector3(0,0,-10);

	public float currOrtho;

	//BOUNDS
	public float minOrtho = 10.0f;
    public float maxOrtho = 50.0f;

	// Use this for initialization
	void Awake () {
		gm = GameObject.Find("Game Master").GetComponent<GameMaster>();
		bm = GameObject.Find("Game Master").GetComponent<BallMaster>();
		bounds = gm.GetComponent<ObjBoundary>();
		cam = this.GetComponent<Camera>(); //reference to camera
		targetTransform = cam.GetComponent<Transform>();
		
		//SET CAM TO STD POS&ORTHO
		targetTransform.position = stdPos;
		cam.orthographicSize = stdOrtho;
		bounds.SetNewBounds();
	}

	public void FocusThenZoomToRegularView(){
		//SET START POSITION AND SCALE
		cam.orthographicSize = paddleFocusOrtho;
		targetTransform.position = paddleFocusPos;

		StartCoroutine(ZoomCamera(paddleFocusOrtho, stdOrtho, smoothSpeed));
		StartCoroutine(MoveCamera(paddleFocusPos, stdPos, smoothSpeed));
	}

	public void ZoomOutLevel(float zoom){
		StartCoroutine(ZoomCamera(currOrtho, currOrtho + zoom, zoomSpeed));
	}

	IEnumerator ZoomCamera(float startOrtho, float endOrtho, float time){
		bm.canSpawn = false;
		float t = 0.0f;
		while(t < time){
			cam.orthographicSize = Mathf.Lerp (startOrtho, endOrtho, (t/time));
			cam.orthographicSize = Mathf.Clamp (cam.orthographicSize, minOrtho, maxOrtho);
			t += Time.deltaTime;
			yield return null;
		}
		currOrtho = endOrtho;
		bm.canSpawn = true;
	}

	IEnumerator MoveCamera(Vector3 startPos, Vector3 endPos, float time){
		float t = 0.0f;
		while(t < time){
			targetTransform.position = Vector3.Lerp (startPos, endPos, (t/time));
			t += Time.deltaTime;
			yield return null;
		}
	}
	public void ResetCam(){
		cam.orthographicSize = stdOrtho;
		targetTransform.position = stdPos;
	}
}
