using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMaster : MonoBehaviour {

	//PADDLES
    GameObject onePaddle;
    GameObject twoPaddle;
    GameObject threePaddle;
    GameObject fourPaddle;
    GameObject fivePaddle;
    GameObject sixPaddle;

	BallMaster bm;

	public GameView view;
	GameObject PopUp;
	GameObject message;



	// Use this for initialization
	void Awake () {

		bm = GameObject.Find("Game Master").GetComponent<BallMaster>();

		onePaddle = (GameObject)Resources.Load("1SPaddle", typeof(GameObject));
    twoPaddle = (GameObject)Resources.Load("2SPaddle", typeof(GameObject));
    threePaddle = (GameObject)Resources.Load("3SPaddle", typeof(GameObject));
    fourPaddle = (GameObject)Resources.Load("4SPaddle", typeof(GameObject));
    fivePaddle = (GameObject)Resources.Load("5SPaddle", typeof(GameObject));
    sixPaddle = (GameObject)Resources.Load("6SPaddle", typeof(GameObject));

		PopUp = (GameObject)Resources.Load("PopUp", typeof(GameObject));
	}

	// Update is called once per frame
	void Update () {

	}

	public void PaddleUpgrade(int score)
    {
        //float zoomDist = 3.0f;
        float pauseTime = 2.0f;

        if (score == 0)
        {
            Instantiate(onePaddle);
            bm.SetSpeedChance(50, 40, 8, 2);
            bm.SetSpawnChance(100, 0);
        }
        else if (score == 2)
        {
            PaddleUpgrade();
            Instantiate(twoPaddle);
            bm.PauseThenStartSpawn(pauseTime);
            //view.ZoomOutLevel(zoomDist);
        }
        else if (score == 5)
        {
            PaddleUpgrade();
            Instantiate(threePaddle);
            //view.ZoomOutLevel(5f);
            bm.PauseThenStartSpawn(pauseTime);
			      bm.SetSpeedChance(30, 50, 10, 10);
            bm.SetSpawnChance(80, 20);
        }
        else if (score == 10)
        {
            PaddleUpgrade();
            Instantiate(fourPaddle);
            //view.ZoomOutLevel(5f);
            bm.PauseThenStartSpawn(pauseTime);
			      bm.SetSpeedChance(20, 50, 20, 10);
            bm.SetSpawnChance(70, 30);
        }
        else if (score == 20)
        {
            PaddleUpgrade();
            Instantiate(fivePaddle);
            //view.ZoomOutLevel(5f);
            bm.PauseThenStartSpawn(pauseTime);
			      bm.SetSpeedChance(10, 50, 30, 10);
            bm.SetSpawnChance(65, 35);
        }
        else if (score == 30){
            PaddleUpgrade();
            Instantiate(sixPaddle);
            //view.ZoomOutLevel(5f);
            bm.PauseThenStartSpawn(pauseTime);
			      bm.SetSpeedChance(0, 40, 40, 20);
            bm.SetSpawnChance(60, 40);
        }
    }

    public void PaddleUpgrade()
    {
        message = Instantiate(PopUp);
        message.GetComponent<PopUp>().SetPopUpMessage("PADDLE UP!");
        StartCoroutine(WaitThenDestroy(2.5f));
    }

    IEnumerator WaitThenDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(message);
    }
}
