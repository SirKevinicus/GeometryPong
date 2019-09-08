using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScore : MonoBehaviour {
	
	public int highScore = 0;
	Text highScoreText;
	public bool canGetNewHighScore;
	GameObject popText;
	GameObject message;
	

	void Start(){
		canGetNewHighScore = true;
		popText = (GameObject)Resources.Load("PopUp", typeof(GameObject));
		highScore = PlayerPrefs.GetInt("HighScore");
		if(SceneManager.GetActiveScene().name == "Greeting"){
			highScoreText = GameObject.Find("hsnum").GetComponent<Text>();
			highScoreText.text = highScore.ToString();
		}
	}	
	
	public bool isNewHighScore() {
		ScoreCounter score = gameObject.GetComponent<ScoreCounter>();
		int curHighScore = PlayerPrefs.GetInt("HighScore");

		//checks to see if the high score has been beaten, if so, then sets the new highscore
		if(curHighScore < score.getScore()){
			PlayerPrefs.SetInt("HighScore", score.getScore());
			return true;
		}
		return false;
	}

	public void resetHighScore(){
		PlayerPrefs.SetInt("HighScore",0);
		highScoreText.text = "0";
	}

	/*
		PRINTS A POPUP SAYING THAT YOU GOT A NEW HIGH SCORE THIS SESSION
	 */
	public void PopHighScore(){
		if(isNewHighScore() && canGetNewHighScore){
			canGetNewHighScore = false;
			message = Instantiate(popText);
			message.GetComponent<PopUp>().SetPopUpMessage("NEW HIGHSCORE!");
			StartCoroutine(WaitThenDestroy(1f));
		}
	}
	IEnumerator WaitThenDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(message);
    }
}
