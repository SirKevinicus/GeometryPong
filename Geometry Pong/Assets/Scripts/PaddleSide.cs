using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleSide : MonoBehaviour {
	//REFERENCES
	ScoreCounter score;
	ColorSwatches colorSwatch;

	GameMaster gm;
	//ATTRIBUTES
	Color pcolor;

	/////////////////////////////////////////////////////////////////////////////////
	void Awake () {
		//REFERENCES
		score = GameObject.Find("Game Master").GetComponent<ScoreCounter>();
		colorSwatch = GameObject.Find("Game Master").GetComponent<ColorSwatches>();
		gm = GameObject.Find("Game Master").GetComponent<GameMaster>();
		
		this.gameObject.tag = "PaddleSide";

		//SET COLOR
		pcolor = colorSwatch.GetSideColor(transform.GetSiblingIndex());	//The sibling index is what number side this is.
		GetComponent<SpriteRenderer>().color = pcolor;
	}

	//When ball hits this side
	void OnTriggerEnter2D(Collider2D ball){
		if (ball.gameObject.tag == "Ball"){
			if(ball.gameObject.GetComponent<ColorBall>().GetColor() == this.pcolor){
				score.addPoint();
			}
			else{
				gm.GameOver();
			}
		}
		if (ball.gameObject.tag == "Bomb"){
			gm.GameOver();
		}
	}

	//Returns this side's color
	public Color getSideColor(){
		return pcolor;
	}

	public void setSideColor(Color c){
		pcolor = c;
	}
}
