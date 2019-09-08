using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomSensor : MonoBehaviour {

	public GameMaster gm;
	public ScoreCounter score;
	public ObjBoundary bounds;

	void Start(){
		
	}

	void Update(){
		SetSize();
	}

	void SetSize(){
		this.transform.localScale = new Vector3(bounds.getScreenWidth(), bounds.getScreenHeight(), 1);
		this.transform.position = new Vector3(0, 0, 1);
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Ball"){
			StartCoroutine(WaitThenDeleteBall(other.gameObject));
			other.enabled = false;
		}
		if (other.gameObject.tag == "Bomb"){
			StartCoroutine(WaitThenDeleteBomb(other.gameObject));
			Debug.Log("Bomb");
		}
	}

	IEnumerator WaitThenDeleteBall(GameObject other){
		yield return new WaitForSeconds(.5f);
		gm.GameOver();
		yield return new WaitForSeconds(2f);
		Destroy(other.gameObject);
	}

	IEnumerator WaitThenDeleteBomb(GameObject other){
		Debug.Log("Wait then delete");
		yield return new WaitForSeconds(.5f);
		score.addPoint();
		yield return new WaitForSeconds(2f);
		Destroy(other.gameObject);
	}
	
}
