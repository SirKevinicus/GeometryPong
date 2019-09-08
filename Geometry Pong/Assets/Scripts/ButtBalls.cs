using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtBalls : MonoBehaviour {

	bool isGrounded;
	public float ballSpeed;
	public float size;

	//REFERENCES
	public ObjBoundary boundary;

	//COMPONENTS
	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		boundary = GameObject.Find("Game Master").GetComponent<ObjBoundary>();
		SetSize();
	}
	
	// Update is called once per frame
	void Update () {
		rb.AddForce(Input.acceleration * ballSpeed);
	}

	void SetSize() {
		size = 30f / (boundary.getCamSize() + 8f);
		Debug.Log(size);
		this.transform.localScale = new Vector3(size, size, size);
	}

}
