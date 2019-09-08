using System.Collections;
using System.Collections.Generic;
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
    public GameView view;

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
        view.FocusThenZoomToRegularView();
    }

    public void RestartGame()
    {
        //SCORE
        score.ResetScore();
        score.showScore();
        gameObject.GetComponent<HighScore>().canGetNewHighScore = true;

        //CAMERA
        view.ResetCam();

        //DELETE OLD PADDLE
        Destroy(GameObject.FindWithTag("Paddle"));
        ColorSwatch.ResetColors();

        //CREATE NEW PADDLE
        pm.PaddleUpgrade(0);

        //RESTART CAMERA
        view.FocusThenZoomToRegularView();

        //SPAWN STUFF
        bm.InvokeBalls();
    }


    //When the game ends, stop spawning balls, show the GameOver Panel.
    public void GameOver()
    {
        bm.CancelInvoke();
        score.hideScore();
        gameoverman.ToggleGameOverMenu();
    }
}
