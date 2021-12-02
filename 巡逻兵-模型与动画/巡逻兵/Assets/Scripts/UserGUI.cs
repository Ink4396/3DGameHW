using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    IUserAction userAction;             //控制器接口
    int points;                         //分数
    int playerHealth;                   //玩家血量
    public bool gameOver;               //是否游戏结束

    //增加分数
    public void AddPoints(int points)
    {
        this.points += points;
    }

    //设置分数
    public void SetPoints(int points)
    {
        this.points = points;
    }

    //设置玩家血量
    public void SetPlayerHealth(int health)
    {
        playerHealth = health;
    }

    void Start()
    {
        gameOver = false;
        points = 0;
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

        GUI.Label(new Rect(20, 0, 200, 50), "Health: " + playerHealth, style);
        GUI.Label(new Rect(20, 60, 100, 50), "Points: " + points, style);

        //显示游戏结束画面
        if (gameOver)
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 50), "You Die !", bigStyle);
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 50), "Restart"))
            {
                userAction.Restart();
            }
        }

        //获得玩家移动操作
        float speed = Input.GetAxis("Vertical");
        float direction = Input.GetAxis("Horizontal");
        userAction.MovePlayer(speed, direction);

    }
}
