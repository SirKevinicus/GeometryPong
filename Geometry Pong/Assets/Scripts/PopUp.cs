using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    Text message;
    ObjBoundary bounds;
    Camera cam;

    // Use this for initialization
    void Awake()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        this.GetComponent<Canvas>().worldCamera = cam;
        bounds = GameObject.Find("Game Master").GetComponent<ObjBoundary>();
        message = this.transform.GetChild(0).GetComponent<Text>();
        
        this.transform.position = new Vector3(0, 0 + (bounds.getScreenHeight()/3), 1);
    }

    public void SetPopUpMessage(string s)
    {
        message.text = s;
    }
}
