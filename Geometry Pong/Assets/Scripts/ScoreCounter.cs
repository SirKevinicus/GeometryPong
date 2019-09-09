using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

	public Text score;
	public Text finalScore;
	private int numPoints;
	public PaddleMaster pm;
	public HighScore hs;
	Animator animator;

	void Start () {
		//Initialize the score to be 0
		numPoints = 0;
		animator = score.GetComponent<Animator>();
		hs = GameObject.Find("Game Master").GetComponent<HighScore>();
		score.text = "0";
	}
	
	public void addPoint (){
		//Add a point to the score
		numPoints++;
		score.text = numPoints.ToString();
		pm.PaddleUpgrade(numPoints);
		checkScore();
		hs.PopHighScore();
	}

	public void setScore(int i){
		score.text = i.ToString();
		numPoints = i;
	}

	/*
		If the score is divisible by 5, this will trigger the emphasis animation
	 */
	public void checkScore(){
		if(numPoints % 5 == 0){
			animator.SetTrigger("importantScore");
		}
	}

	public int getScore(){
		return numPoints;
	}

	public void showScore(){
		score.text = numPoints.ToString();
	}

	public void hideScore(){
		score.text = " ";
	}

	public void ResetScore() {
		//Reset the score
		score.text = "0";
		numPoints = 0;
	}

	public void SetFinalScore(){
		finalScore.text = "Score: " + numPoints.ToString();
	}
}
