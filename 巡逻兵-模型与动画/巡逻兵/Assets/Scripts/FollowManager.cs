using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowManager : MonoBehaviour
{
    public bool followable;         //�Ƿ�ɸ���
    public bool stop;               //�Ƿ���ͣ
    public bool lookat;             //�Ƿ������
    public float speed;             //�����ٶ�
    void Start()
    {
        followable = true;
        lookat = true;
        stop = false;
    }

}
