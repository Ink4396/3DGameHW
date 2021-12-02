using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerAction : SSAction
{
    PlayerManager playerManager;
    float counter;                  //������

    //��������(����ģʽ)
    public static MovePlayerAction GetSSAction()
    {
        MovePlayerAction action = ScriptableObject.CreateInstance<MovePlayerAction>();
        return action;
    }

    public override void Start()
    {
        playerManager = gameObject.GetComponent<PlayerManager>();
        counter = 0;
    }

    public override void Update()
    {
        if (playerManager.moveable)
        {
            float speed = playerManager.speed;
            //��Һ���ʱ�������ת180�Ȳ�������ͷ�����������
            if (speed < -0.1)
            {
                speed = -speed;
                if (GameObject.FindWithTag("MainCamera").GetComponent<FollowManager>().lookat)
                {
                    gameObject.transform.Rotate(0, 180, 0);
                    GameObject.FindWithTag("MainCamera").GetComponent<FollowManager>().lookat = false;
                }
            }
            else if (speed > 0.1)
            {
                if (!GameObject.FindWithTag("MainCamera").GetComponent<FollowManager>().lookat)
                {
                    gameObject.transform.Rotate(0, 180, 0);
                    GameObject.FindWithTag("MainCamera").GetComponent<FollowManager>().lookat = true;
                }
            }
            //�ٶ�>0.7ʱ���Ӽ�������1.5s��תΪ�ܲ��ٶȣ��������Ϊ0.7
            if (speed >= 0.7)
            {
                counter = (counter + Time.deltaTime) > 1.5f ? 1.5f : counter + Time.deltaTime;
                speed = counter == 1.5f ? speed : 0.7f;
            }
            else
            {
                counter = (counter - Time.deltaTime) < 0 ? 0 : counter - Time.deltaTime;
            }
            //��ǰ�ƶ�
            gameObject.transform.Translate(0, 0, speed * 3.6f * Time.deltaTime);
            playerManager.speed = playerManager.speed > 0 ? playerManager.speed - Time.deltaTime : playerManager.speed + Time.deltaTime;
            //������ת
            gameObject.transform.Rotate(0, playerManager.direction * 70 * Time.deltaTime, 0);
            playerManager.direction = 0;
            //�������״̬���ٶ�
            playerManager.SetSpeed(speed);
        }
        else
        {
            this.destroy = true;
            this.enable = false;
            this.callback.SSActionEvent(this);
        }

    }
}
