using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    IUserAction userAction;
    string roundMessage;
    string gameMessage;
    string curMode;
    string curInfinite;
    bool isfly;
    bool isInfinite;
    int points;

    public void SetroundMessage(string roundMessage)
    {
        this.roundMessage = roundMessage;
    }
    public void SetMessage(string gameMessage)
    {
        this.gameMessage = gameMessage;
    }
    public void SetcurMode(string curMode)
    {
        this.curMode = curMode;
    }
    public void SetcurInfinite(string curInfinite)
    {
        this.curInfinite = curInfinite;
    }

    public void SetPoints(int points)
    {
        this.points = points;
    }

    void Start()
    {
        points = 0;
        roundMessage = "Hit UFO";
        gameMessage = "";
        curMode = "off";
        curInfinite = "off";
        isfly = false;
        userAction = SSDirector.GetInstance().CurrentScenceController as IUserAction;
    }

    void OnGUI()
    {
        //小字体初始化
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontSize = 30;

        //大字体初始化
        GUIStyle bigStyle = new GUIStyle();
        bigStyle.normal.textColor = Color.white;
        bigStyle.fontSize = 50;

        GUI.Label(new Rect(300, 30, 50, 200), roundMessage, bigStyle);
        GUI.Label(new Rect(20, 0, 100, 50), "Points: " + points, style);
        GUI.Label(new Rect(310, 100, 50, 200), gameMessage, style);
        if (GUI.Button(new Rect(20, 50, 100, 40), "Restart"))
        {
            userAction.Restart();
        }
        if (GUI.Button(new Rect(20, 100, 100, 40), "Infinite "+curInfinite))
        {
            isInfinite = !isInfinite;
            userAction.SetMode(isInfinite);
        }
        if (GUI.Button(new Rect(20, 150, 100, 40), "Physis "+curMode))
        {
            isfly = !isfly;
            userAction.SetFlyMode(isfly);
        }
    }
	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
        {
            userAction.Hit(Input.mousePosition);
        }
	}
}
