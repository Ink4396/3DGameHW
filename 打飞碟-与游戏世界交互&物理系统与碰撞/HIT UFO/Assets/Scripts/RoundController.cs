using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    FirstController controller;
    IActionManager actionManager;                   //动作管理者
    DiskFactory diskFactory;                         //飞碟工厂
    ScoreRecorder scoreRecorder;
    UserGUI userGUI;
    int n;                     //轮次数
    int roundTrial;           //每轮的trial数
    bool isInfinite;            //游戏当前模式
    int round;                  //游戏当前轮次
    int sendCnt;                //当前已发送的飞碟数量
    float sendTime;             //发送时间

    void Start()
    {
        controller = (FirstController)SSDirector.GetInstance().CurrentScenceController;
        actionManager = Singleton<CCActionManager>.Instance;
        diskFactory = Singleton<DiskFactory>.Instance;
        scoreRecorder = new ScoreRecorder();
        userGUI = Singleton<UserGUI>.Instance;
        sendCnt = 0;
        round = 1;
        sendTime = 0;
        isInfinite = false;
        roundTrial= 10;
        n = 5;
    }

    public void Reset()
    {
        sendCnt = 0;
        round = 1;
        sendTime = 0;
        scoreRecorder.Reset();
    }

    public void Record(DiskData disk)
    {
        scoreRecorder.Record(disk);
    }

    public int GetPoints()
    {
        return scoreRecorder.GetPoints();
    }

    public void SetMode(bool isInfinite)
    {
        this.isInfinite = isInfinite;
        userGUI.SetcurInfinite(isInfinite ? "on" : "off");
    }

    public void SetFlyMode(bool isPhysis)
    {
        actionManager = isPhysis ? Singleton<PhysisActionManager>.Instance : Singleton<CCActionManager>.Instance as IActionManager;
        userGUI.SetcurMode(isPhysis ? "on" : "off");
    }

    public void SendDisk()
    {
        //从工厂生成一个飞碟
        GameObject disk = diskFactory.GetDisk(round);
        //设置飞碟的随机位置
        disk.transform.position = new Vector3(-disk.GetComponent<DiskData>().direction.x * 7, UnityEngine.Random.Range(0f, 8f), 0);
        disk.SetActive(true);
        //设置飞碟的飞行动作
        actionManager.Fly(disk, disk.GetComponent<DiskData>().speed, disk.GetComponent<DiskData>().direction);
    }

    // Update is called once per frame
    void Update()
    {
        sendTime += Time.deltaTime;
        //每隔1s发送一次飞碟
        if (sendTime > 1)
        {
            sendTime = 0;
            //每次发送至多3个飞碟
            for (int i = 0; i < UnityEngine.Random.Range(1, 3) && sendCnt < roundTrial && round <= n ; i++)
            {
                SendDisk();
            }
            if (sendCnt < roundTrial && round <= n)
            {
                sendCnt++;
                userGUI.SetroundMessage("Round " + round);
                userGUI.SetMessage("Trial " + sendCnt);
            }
            else
                userGUI.SetroundMessage("Hit UFO");
            //判断是否需要重置轮次，不需要则输出游戏结束
            if (sendCnt == roundTrial && round == n)
            {
                if (isInfinite)
                {
                    round++;
                    n++;
                    sendCnt = 0;
                    userGUI.SetMessage("");
                }
                else
                {
                    userGUI.SetMessage("Game Over!");
                }
            }
            //更新轮次
            if (sendCnt == roundTrial && round < n)
            {
                sendCnt = 0;
                round++;
            }
        }
    }
}
