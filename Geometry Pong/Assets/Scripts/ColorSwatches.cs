using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwatches : MonoBehaviour {
	public static Color red_color = new Color(0.804f, 0.353f, 0.502f);
	public static Color blue_color = new Color(0.424f, 0.592f, 0.812f);
	public static Color orange_color = new Color(0.937f, 0.624f, 0.243f);
	public static Color green_color = new Color(0.678f, 0.847f, 0.639f);
	public static Color purple_color = new Color(0.608f, 0.451f, 0.698f);
	public static Color yellow_color = new Color(0.867f, 0.843f, 0.373f);

	public Dictionary<string, Color> color_dict = new Dictionary<string, Color>(){
		{"red", red_color},
		{"blue", blue_color},
		{"orange", orange_color},
		{"green", green_color},
		{"purple", purple_color},
		{"yellow", yellow_color}
	};

	public Dictionary<Color, string> reverse_color_dict = new Dictionary<Color, string>(){
		{red_color, "red"},
		{blue_color, "blue"},
		{orange_color, "orange"},
		{green_color, "green"},
		{purple_color, "purple"},
		{yellow_color, "yellow"}
	};

	public static Dictionary<string,Color> color_bank = new Dictionary<string, Color>(){};
	public static List<Color> active_colors = new List<Color>();

	void Awake(){
		ResetColors();
	}

	public void RotateColorsRight(){
		Color active = active_colors[0];
		active_colors.RemoveAt(0);
		List<Color> new_list = new List<Color>();
		for(int i = 0; i < active_colors.Count; i++){
			new_list.Add(active_colors[i]);
		}
		new_list.Add(active);
		active_colors = new_list;
	}

	public void RotateColorsLeft(){
		Color new_active = active_colors[active_colors.Count-1]; //Get the last element
		active_colors.RemoveAt(active_colors.Count-1); // Remove last element from active
		List<Color> new_list = new List<Color>();
		new_list.Add(new_active);	//Last is the new front
		for (int i = 0; i < active_colors.Count; i++){
			new_list.Add(active_colors[i]);
		}
		active_colors = new_list;
	}

	public void PickNewColor(){
		/*
			Selects a random color and adds it to the active dictionary.
		*/
		List<string> colorKeyList = new List<string>(color_bank.Keys);	//Get a list of the colors
		System.Random randIndex = new System.Random();	//Pick a random Index from the dictionary
		string randomKey = colorKeyList[randIndex.Next(colorKeyList.Count)];	//Get that Color's Key

		//Removes the color from the available colors and adds it to the active colors dictionary
		active_colors.Add(color_bank[randomKey]);
		color_bank.Remove(randomKey);
	}

	public Color PickBallColor(){
		/*
			RETURN: A random color currently on the paddle
		*/
		System.Random rand = new System.Random();	//Pick a random Index from the dictionary
		int randIndex = rand.Next(active_colors.Count);	//Get that Color's Key

		return active_colors[randIndex];	//This is the RGB Value
	}

	public Color GetSideColor(int sidenum){
		return active_colors[sidenum];
	}

	public void ResetColors(){
		foreach(KeyValuePair<string,Color> e in color_dict){
			color_bank[e.Key] = e.Value;
		}
		active_colors = new List<Color>();
	}
}
