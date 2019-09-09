using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBall : BallBoi {

	// Use this for initialization
	void Awake () {
		InitBall();
		this.gameObject.tag = "Bomb";
	}

	//When the Bomb HITS
	private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("PaddleSide")){
            isActive = false;
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.GetComponent<CircleCollider2D>().enabled = false;
            //animator.SetTrigger("hitPaddle");
            //Destroy (gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length - .6f);
			Destroy(this);
        }
    }
}
