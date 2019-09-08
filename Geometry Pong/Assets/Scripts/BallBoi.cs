using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BallBoi : MoveableObject{

    //*****All the magic numbers need to be constrained to the max min sizes of the screen*****//

    //References
    public ObjBoundary boundary;
    public GameObject bounceAnim;
    public GameObject gm;

    //Attributes
    protected bool isActive;

    //Components
    public SpriteRenderer ballRend;
    public Transform ballTransform;

    //Physics
    public float speedX = 0f;
    public float speedY = -1f;
    protected float ySpawn;
    protected float maxSpeed = 1f;

	protected void InitBall() {
        //Initialize References
        gm = GameObject.Find("Game Master");
        boundary = gm.GetComponent<ObjBoundary>();
        bounceAnim = Resources.Load<GameObject>("Bounce");

        //Set Attributes
        this.gameObject.tag = "Ball";

        //Set the Boundaries
        CalcBounds(this.gameObject);
		    ySpawn = boundary.getBallYAxis();

        //Components
        ballTransform = this.GetComponent<Transform>();
        ballRend = this.GetComponent<SpriteRenderer>();

        //initialize all attributes and physics values
        isActive = true;
        SetSize();
        ChooseBallStartPos();
	}

    /*
        SET BALL SPEED
        PARAM: int s, number corresponding to the speed. 0 = slow, 1 = normal, 2 = fast
        DESCR: Randomly picks a speed for this ball.
     */
    public void SetBallSpeed(int s){
        switch(s){
            case 0:
                speedY = -.7f;
                Debug.Log("Slow");
                break;
            case 1:
                speedY = -.8f;
                Debug.Log("Normal");
                break;
            case 2:
                speedY = -.9f;
                Debug.Log("Fast");
                break;
            case 3:
                //Really fast
                speedY = -1f;
                Debug.Log("NYOOM");
                break;
            default:
                Debug.Log("Oh shit");
                break;
        }
        speedY *= boundary.getCamSize();
    }

    public void SetBallAngle(int s){
        switch(s){
            case 0:
                //Straight Ball
                speedX = 0;
                break;
            case 1:
                //Angle Right
                speedX = Random.Range(.1f,.8f);
                break;
            case 2:
                speedX = Random.Range(-.1f, -.8f);
                break;
            default:
                Debug.Log("REEEE");
                break;
        }
        speedX *= boundary.getCamSize();
    }

    protected void Update() {
        CalcBounds(this.gameObject);

        if(isActive){
            //If it hits the right side
            if(this.transform.position.x >= xMax){
                speedX = -speedX; // reverse speed
                ballTransform.position = ballTransform.position + new Vector3(-.1f,0,0); //Move over slightly so bounce bug doesn't happen
                Vector3 pos = ballTransform.position + new Vector3(1,-2,0);
                Instantiate(bounceAnim, pos, Quaternion.Euler(new Vector3(0,180,0)));
            }
            //If it hits the left side
            else if (this.transform.position.x <= xMin){
                speedX = -speedX; // reverse speed
                ballTransform.position = ballTransform.position + new Vector3(.1f,0,0);
                Vector3 pos = ballTransform.position + new Vector3(-1,-2,0);
                Instantiate(bounceAnim, pos, Quaternion.identity);
            }

            //Update the Ball's Position
            Vector2 newPos = new Vector2(ballTransform.position.x + (speedX * Time.deltaTime), ballTransform.position.y + (speedY * Time.deltaTime));
            ballTransform.position = newPos;
        }
    }


    //Picks a random ball spawn location
    protected void ChooseBallStartPos(){
        this.transform.position = new Vector2((float)UnityEngine.Random.Range(xMin+1f,xMax-1f), ySpawn);
    }
}
