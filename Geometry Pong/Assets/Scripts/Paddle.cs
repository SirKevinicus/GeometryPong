using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MoveableObject
{

    //REFERENCES
    SpriteRenderer paddle_rend;
    ColorSwatches colorSwatch;
    ObjBoundary boundary;
    GameView view;

    //ATTRIBUTES
    int numSides = 0;

    GameObject active_side;
    int active_side_num;

    int degreesToTurn;
    bool canRotate;
    bool isRotating;
    Touch movingFinger;
    Touch rotateFinger;

    //SIDES
    public GameObject[] sides;

    //PHYSICS
    float ySpawn;   // the y value that the paddle moves on
    float speed;
    float rightBound;
    float leftBound;

    /// onAwake allows for each inheritance to get all the functionality and still implement other useful functionalities
    void Awake()
    {
        //Get Outside References
        boundary = GameObject.Find("Game Master").GetComponent<ObjBoundary>();
        colorSwatch = GameObject.Find("Game Master").GetComponent<ColorSwatches>();

        //Controls  
        rightBound = (Camera.main.pixelWidth / 2.0f);
        leftBound = (Camera.main.pixelWidth / 2.0f);
        canRotate = true;

        //DO STUFF!
        this.gameObject.tag = "Paddle";
        PopulateSides();
        CanMove();
        CalcDegreeAngle();
        teleportToStartPos();
        if (numSides > 1) {
            DestroyOldPaddle();
        }
        colorSwatch.PickNewColor();

        //Get the paddle's boundaries from the Boundary class
        CalcBounds(sides[0]);
        ySpawn = boundary.getPaddleYAxis();
        speed = 25f;
    }

    //Assign sides to this paddle
    private void PopulateSides()
    {
        int numChildren = this.transform.childCount;
        //Count the number of sides
        /*for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).tag != "PaddleSide")
            {
                numChildren--;
            }
        }*/

        sides = new GameObject[numChildren];
        for (int i = 0; i < numChildren; i++)
        {
            if (this.transform.GetChild(i).tag == "PaddleSide")
            {
                sides[i] = this.transform.GetChild(i).gameObject;
                numSides++;
            }
        }
    }

    //Return the angle that we need to turn
    private void CalcDegreeAngle()
    {
        if (numSides == 1)
        {
            degreesToTurn = 0;
            canRotate = false;
            return;
        }
        else if (numSides == 2)
        {
            degreesToTurn = -(360 / numSides);
            canRotate = true;
        }
        else
        {
            degreesToTurn = 360 / numSides;
            canRotate = true;
        }
    }

    //Put the paddle at its spawn
    private void teleportToStartPos()
    {
        this.transform.position = new Vector3(0, ySpawn, 0);
    }

    //Get rid of the old paddle and go where it just was
    private void DestroyOldPaddle()
    {
        this.gameObject.SetActive(true);
        GameObject old_paddle = GameObject.FindWithTag("Paddle");
        this.transform.position = new Vector2(old_paddle.transform.position.x, old_paddle.transform.position.y);
        Destroy(old_paddle);
    }

    Vector2[] dist = new Vector2[2];
    float[] timeTouched = new float[2]{0,0};
    void Update()
    {
        CalcBounds(sides[0]);
        ySpawn = boundary.getPaddleYAxis();
        if(isMoveable){
            //COMPUTER CONTROLS
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
                this.transform.position += move * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
                this.transform.position += move * speed * Time.deltaTime;
            }
            if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.D))
            {
                if (!isRotating)
                {
                    StartCoroutine(Rotate(new Vector3(0, 0, -degreesToTurn), .1f));
                }
            }
            if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.A))
            {
                if (!isRotating)
                {
                    StartCoroutine(Rotate(new Vector3(0, 0, degreesToTurn), .1f));
                }
            }

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            //  MOBILE
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            if (Input.touchCount == 1 || Input.touchCount == 2)
            {
                //Only allow the first finger down to move the paddle
                movingFinger = Input.GetTouch(0);
                int moveID = movingFinger.fingerId;

                //Iterate through each finger
                for(int i = 0; i < Input.touchCount; i++){
                    Touch finger = Input.GetTouch(i);
                    timeTouched[i] += Time.deltaTime;

                    if(finger.phase == TouchPhase.Began){
                        dist[i] = new Vector2(0,0); //Initialize the distance for the finger
                    }

                    //When the moving finger moves
                    if(finger.phase == TouchPhase.Moved && finger.fingerId == moveID){
                        //If the touch has gone more than 20f, go ahead and move
                        if (dist[i].magnitude >= 20f)
                        {
                            Vector2 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(finger.position.x, finger.position.y));
                            transform.position = touchedPos;
                        }
                        dist[i] += finger.deltaPosition; //Add onto the distance moved
                    }

                    if (finger.phase == TouchPhase.Ended)
                    {
                        //Considered a tap if the distance it traveled is less than 20f
                        if (dist[i].magnitude < 20f && timeTouched[i] < 1f && timeTouched[i] > 0.02f)
                        {
                            //rotate right if the tap was on the right side
                            if (finger.position.x > rightBound)
                            {
                                if (canRotate && !isRotating)
                                {
                                    StartCoroutine(Rotate(new Vector3(0, 0, -degreesToTurn), .11f));
                                }
                            }
                            if (finger.position.x < leftBound)
                            {
                                if (canRotate && !isRotating)
                                {
                                    StartCoroutine(Rotate(new Vector3(0, 0, degreesToTurn), .11f));
                                }
                            }
                        }
                        dist[i] = new Vector2(0, 0);    //clear the distance out
                        Debug.Log("TIME: " + timeTouched);
                        timeTouched[i] = 0;
                    }
                }
            }
        }
        //Ensure the paddle doesn't go off the screen
        this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, xMin, xMax), ySpawn, 0);
    }

    private IEnumerator Rotate(Vector3 angles, float duration)
    {
        isRotating = true;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(angles) * startRotation;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t / duration);
            yield return null;
        }
        if(angles.z > 0){
            colorSwatch.RotateColorsRight();
        } else{
            colorSwatch.RotateColorsLeft();
        }
        transform.rotation = endRotation;
        isRotating = false;
    }
}
