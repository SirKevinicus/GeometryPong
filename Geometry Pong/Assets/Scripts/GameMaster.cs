using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    //OBJECT REFERENCES

    //SCRIPT REFERENCES
    ColorSwatches ColorSwatch;
    public GameOverMenu gameoverman;
    BallMaster bm;
    PaddleMaster pm;
    public ScoreCounter score;

    //VARS

    public int paddleNum = 1;

    //////////////////////////////////////////////////////////////////

    // Use this for initialization
    public void Start()
    {
        Application.targetFrameRate = 60;

        pm = GetComponent<PaddleMaster>();
        bm = GetComponent<BallMaster>();

        ColorSwatch = this.gameObject.GetComponent<ColorSwatches>();

        pm.PaddleUpgrade(score.getScore());
        bm.InvokeBalls();
    }

    public void RestartGame()
    {
        //SCORE
        score.ResetScore();
        score.showScore();
        gameObject.GetComponent<HighScore>().canGetNewHighScore = true;

        //DELETE OLD PADDLE
        Destroy(GameObject.FindWithTag("Paddle"));
        ColorSwatch.ResetColors();

        //CREATE NEW PADDLE
        pm.PaddleUpgrade(0);

        //SPAWN STUFF
        bm.InvokeBalls();

        gameoverman.HideGameOverMenu();
    }


    //When the game ends, stop spawning balls, show the GameOver Panel.
    public void GameOver()
    {
        bm.CancelInvoke();
        score.hideScore();
        gameoverman.ShowGameOverMenu();
    }

    public void GoToMenu(){
      SceneManager.LoadScene("Greeting");
    }
}
