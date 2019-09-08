using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMaster : MonoBehaviour {

	public GameObject ColorBall;
	public GameObject Bomb;

	//VARS
	float repeatTime = 2f;
	public bool canSpawn = true;
	public float[] oddsToSpeed = new float[]{0.5f, 0.9f, 0.98f};
  public float[] oddsToSpawn = new float[]{1.0f, 1.0f};

	// Use this for initialization
	void Start () {
		Bomb = (GameObject)Resources.Load("Bomb", typeof(GameObject));
		ColorBall =(GameObject)Resources.Load("ColorBall", typeof(GameObject));
	}

	public void InvokeBalls(){
        InvokeRepeating("Spawn", 0, repeatTime);
    }

	public void SetSpawnChance(float colorBall, float bombBall){
		if(colorBall + bombBall != 100){
			Debug.Log("The odds are against us");
		}
		colorBall = colorBall / 100f;
		oddsToSpawn = new float[]{colorBall, 1f - colorBall};
	}

    //Spawns balls
    void Spawn()
    {
        if(canSpawn){

            //DEPRECATED: Picks a random speed
            float r = Random.Range(0.0f, 1.0f);
            int ballSpeed;
            if(r < oddsToSpeed[0]){
                ballSpeed = 0;  //SLOW BALL
            } else if(r > oddsToSpeed[0] && r < oddsToSpeed[1]){
                ballSpeed = 1; //NORMAL BALL
            } else if(r > oddsToSpeed[1] && r < oddsToSpeed[2]){
                ballSpeed = 2;
            } else{
                ballSpeed = 3;
            }

            //Picks an angle
            int ballAngle = Random.Range(0,3);

            float projType = Random.Range(0.0f,1.0f);
            if(projType <= oddsToSpawn[0]){
                SpawnBall(1, ballAngle);
            }
            else{
                SpawnBomb(ballSpeed, ballAngle);
            }
        }
    }

    public void PauseThenStartSpawn(float time){
        StartCoroutine("WaitThenRestartSpawn", time);
    }

    IEnumerator WaitThenRestartSpawn(float time){
        CancelInvoke();
        Debug.Log("WAAAAIT");
        yield return new WaitForSeconds(time);
        Debug.Log("LETSGO");
        InvokeBalls();
    }

    public void SpawnBall(int i, int q){
        GameObject ball = Instantiate(ColorBall);
        ball.GetComponent<BallBoi>().SetBallSpeed(i);
        ball.GetComponent<BallBoi>().SetBallAngle(q);
    }

    public void SpawnBomb(int i, int q){
        GameObject bomb = Instantiate(Bomb);
        bomb.GetComponent<BombBall>().SetBallSpeed(i);
        bomb.GetComponent<BombBall>().SetBallAngle(q);
    }

    //DEPRECATED CODE
    public void SetSpeedChance(float slowBall, float normBall, float fastBall, float nyoomBall){
		if(slowBall + normBall + fastBall + nyoomBall != 100){
			Debug.Log("The odds are against us");
		}
		slowBall = slowBall / 100f;
		normBall = normBall / 100f;
		fastBall = fastBall / 100f;
		nyoomBall = 1f - (nyoomBall / 100f);
		oddsToSpeed = new float[]{slowBall, slowBall+normBall, slowBall + normBall + fastBall};
	}
}
