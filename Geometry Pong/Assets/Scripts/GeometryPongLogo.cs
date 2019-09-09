using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeometryPongLogo : MonoBehaviour {

	Dictionary<string,Color> colors;
	List<Color> colorList;
	int colorIndex;
	bool colorChange;

	// Use this for initialization
	void Start () {
		colors = GameObject.Find("Game Master").GetComponent<ColorSwatches>().color_dict;
		colorIndex = 0;
		colorList = new List<Color> ();
		colorChange = false;
		foreach(KeyValuePair<string,Color> e in colors){
			colorList.Add(e.Value);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!colorChange){
			StartCoroutine("WaitThenChangeColor");
		}
	}

	IEnumerator WaitThenChangeColor(){
		colorChange = true;
		if(colorIndex == colorList.Count){
			colorIndex = 0;
		}
		yield return new WaitForSeconds(1f);
		Debug.Log(colorList[colorIndex]);
		this.GetComponent<Text>().color = colorList[colorIndex];
		colorIndex++;
		colorChange = false;
	}
}
