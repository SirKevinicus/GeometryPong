using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour {
	public GameMaster gm;
	public GameObject[] ballList;
	public ScoreCounter score;

	void Start(){
		//Hide the GameOver Panel
		gameObject.SetActive(false);
	}

	//Called when the player loses
	public void ShowGameOverMenu(){
    	//Show the Game Over Panel
		gameObject.SetActive(true);
		score.SetFinalScore();
		//Finds all active balls and destroys them
		ballList = GameObject.FindGameObjectsWithTag("Ball");
    	for(int i = 0 ; i < ballList.Length ; i ++){
        	Destroy(ballList[i]);
		}
		ballList = GameObject.FindGameObjectsWithTag("Bomb");
    	for(int i = 0 ; i < ballList.Length ; i ++){
        	Destroy(ballList[i]);
		}
	}

	public void HideGameOverMenu(){
		gameObject.SetActive(false);
	}
	
}
