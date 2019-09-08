using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBall : BallBoi {
	
	//Components
	public GameObject ball_explode_FX;
	public ColorSwatches colorSwatch;
	protected Color ballColor;
	Animator animator;

	void Awake(){
		InitBall();
		colorSwatch = GameObject.Find("Game Master").GetComponent<ColorSwatches>();
		animator = this.GetComponent<Animator>();
		ChooseBallColor();
		
	}

	//Pick a ball color out of the available ones
    protected void ChooseBallColor(){
        ballColor = colorSwatch.PickBallColor();
        ballRend.color = ballColor;
    }

	private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("PaddleSide")){
            isActive = false;
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.GetComponent<CircleCollider2D>().enabled = false;
            animator.SetTrigger("hitPaddle");
            Destroy (gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length - .6f); 
        }
    }
	
	//return the color of the ball
    public Color GetColor(){
        return ballColor;
    }
}

