using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    IUserAction userAction;             //�������ӿ�
    int points;                         //����
    int playerHealth;                   //���Ѫ��
    public bool gameOver;               //�Ƿ���Ϸ����

    //���ӷ���
    public void AddPoints(int points)
    {
        this.points += points;
    }

    //���÷���
    public void SetPoints(int points)
    {
        this.points = points;
    }

    //�������Ѫ��
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
        //С�����ʼ��
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontSize = 30;

        //�������ʼ��
        GUIStyle bigStyle = new GUIStyle();
        bigStyle.normal.textColor = Color.white;
        bigStyle.fontSize = 50;

        GUI.Label(new Rect(20, 0, 200, 50), "Health: " + playerHealth, style);
        GUI.Label(new Rect(20, 60, 100, 50), "Points: " + points, style);

        //��ʾ��Ϸ��������
        if (gameOver)
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 50), "You Die !", bigStyle);
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 50), "Restart"))
            {
                userAction.Restart();
            }
        }

        //�������ƶ�����
        float speed = Input.GetAxis("Vertical");
        float direction = Input.GetAxis("Horizontal");
        userAction.MovePlayer(speed, direction);

    }
}
