using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction
{
    GameObject map;                     //��ͼ����
    GameObject player;                  //��ҹ���
    UserGUI userGUI;                    //�û�����
    IActionManager actionManager;       //��������
    AreaController areaController;      //���޹���
    float survivetime = 0;
    // Start is called before the first frame update
    void Start()
    {
        SSDirector.GetInstance().CurrentScenceController = this;
        gameObject.AddComponent<MonsterFactory>();
        gameObject.AddComponent<AreaController>();
        areaController = Singleton<AreaController>.Instance;
        gameObject.AddComponent<CCActionManager>();
        gameObject.AddComponent<UserGUI>();
        userGUI = Singleton<UserGUI>.Instance;
        actionManager = Singleton<CCActionManager>.Instance;
        LoadResources();
        GameObject.FindWithTag("MainCamera").AddComponent<FollowManager>();
        actionManager.FollowPlayer(GameObject.FindWithTag("MainCamera"), 3.5f, 5f, 6);
    }

    //���ĸ��¼�
    private void OnEnable()
    {
        AreaController.followAction += FollowAction;
        AreaController.monsterMoveAction += MonsterMoveAction;
        PlayerManager.dealDamage += DealPlayerDamage;
        DetectPlace.setArea += SetArea;
    }

    //ȡ������
    private void OnDisable()
    {
        AreaController.followAction -= FollowAction;
        AreaController.monsterMoveAction -= MonsterMoveAction;
        PlayerManager.dealDamage -= DealPlayerDamage;
        DetectPlace.setArea -= SetArea;
    }

    //������Դ
    public void LoadResources()
    {
        if (map == null)
            map = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/map"), Vector3.zero, Quaternion.identity);
        if (player == null)
        {
            player = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/MaleFree1"), Vector3.zero, Quaternion.identity);
            player.AddComponent<PlayerManager>();
        }
        player.GetComponent<PlayerManager>().health = 3;
        userGUI.SetPlayerHealth(3);
        userGUI.SetPoints(0);
        player.transform.position = new Vector3(9, 0.5f, 9);
        actionManager.MovePlayer(player, 0, 0);
    }

    //������������¼�
    public void DealPlayerDamage(GameObject sender)
    {
        userGUI.SetPlayerHealth(sender.GetComponent<PlayerManager>().health);
        if (sender.GetComponent<PlayerManager>().health == 0)
        {
            areaController.FreeAll();
            sender.GetComponent<PlayerManager>().moveable = false;
            userGUI.gameOver = true;
        }
    }

    //����ƶ�
    public void MovePlayer(float speed, float direction)
    {
        if (player.GetComponent<PlayerManager>().moveable)
            actionManager.MovePlayer(player, speed, direction);
    }

    //�������׷���¼�
    public void FollowAction(GameObject follower, float distanceAway, float distanceUp, float speed)
    {
        actionManager.FollowPlayer(follower, distanceAway, distanceUp, speed);
    }

    //�������Ѳ���¼�
    public void MonsterMoveAction(GameObject monster, float speed)
    {
        actionManager.MoveMonster(monster, speed);
    }

    //��Ϸ�ؿ�
    public void Restart()
    {
        LoadResources();
        player.GetComponent<PlayerManager>().Revive();
        areaController.GameStart();
        userGUI.gameOver = false;
    }

    //�����������
    public void SetArea(float x, float y)
    {
        int playerArea = -1;
        if (x > 4.5f && y > 4.5f)
            playerArea = -1;
        else if (x > 4.5f && y > -4.5f && y < 4.5f)
            playerArea = 0;
        else if (x > 4.5f && y < -4.5f)
            playerArea = 1;
        else if (x < 4.5f && x > -4.5f && y < -4.5f)
            playerArea = 2;
        else if (x < 4.5f && x > -4.5f && y < 4.5f && y > -4.5f)
            playerArea = 3;
        else if (x < 4.5f && x > -4.5f && y > 4.5f)
            playerArea = 4;
        else if (x < -4.5f && y < -4.5f)
            playerArea = 5;
        else if (x < -4.5f && y > -4.5f && y < 4.5f)
            playerArea = 6;
        else if (x < -4.5f && y > 4.5f)
            playerArea = 7;
        areaController.SetArea(playerArea);
    }

    void Update()
    {
        if(areaController.GetArea() != -1)
        survivetime += Time.deltaTime;
        if(survivetime > 1)
        {
            userGUI.AddPoints(1);
            survivetime = 0;
        }
    }
}
