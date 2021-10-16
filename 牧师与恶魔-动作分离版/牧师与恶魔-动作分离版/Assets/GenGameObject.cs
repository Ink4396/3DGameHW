using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GSCsettings;

public class GenGameObject : MonoBehaviour
{
	// 使用Stack来存储游戏对象,start代表开始岸，end代表目标岸
	// priests_start代表在开始岸上的牧师，devils_start代表在开始岸上的恶魔
	public Stack<GameObject> priests_start = new Stack<GameObject>();
	public Stack<GameObject> priests_end = new Stack<GameObject>();
	public Stack<GameObject> devils_start = new Stack<GameObject>();
	public Stack<GameObject> devils_end = new Stack<GameObject>();
	// 使用数组来存储在船上的游戏对象
	public GameObject[] boat = new GameObject[2];
	// 船的实体
	public GameObject boat_obj;
	// 船的速度
	public float speed = 10f;
	GameSceneController my;
	// 坐标
	Vector3 shoreStartPos = new Vector3(0, 0, -12);
	Vector3 shoreEndPos = new Vector3(0, 0, 12);
	Vector3 riverPos = new Vector3(0, -1, 0);
	Vector3 boatStartPos = new Vector3(0, 0, -5);
	Vector3 boatEndPos = new Vector3(0, 0, 5);
	float gap = 1.5f;
	Vector3 priestStartPos = new Vector3(0, 2f, -11f);
	Vector3 priestEndPos = new Vector3(0, 2f, 8f);
	Vector3 devilStartPos = new Vector3(0, 2f, -16f);
	Vector3 devilEndPos = new Vector3(0, 2f, 13f);
	void Start()
	{
		// 将 GenGameObject 对象注入了 GameSceneController 单实例对象中
		my = GameSceneController.GetInstance();
		my.setGenGameObject(this);
		loadSrc();
	}
	// 用来实例化游戏对象
	void loadSrc()
	{
		// shore  
		Instantiate(Resources.Load("Prefabs/Shore"), shoreStartPos, Quaternion.identity);
		Instantiate(Resources.Load("Prefabs/Shore"), shoreEndPos, Quaternion.identity);
		// river  
		Instantiate(Resources.Load("Prefabs/River"), riverPos, Quaternion.identity);
		// boat  
		boat_obj = Instantiate(Resources.Load("Prefabs/Boat"), boatStartPos, Quaternion.identity) as GameObject;
		// priests & devils  
		for (int i = 0; i < 3; ++i)
		{
			priests_start.Push(Instantiate(Resources.Load("Prefabs/Priest")) as GameObject);
			devils_start.Push(Instantiate(Resources.Load("Prefabs/Devil")) as GameObject);
		}
	}
	void setCharacterPositions(Stack<GameObject> stack, Vector3 pos)
	{
		GameObject[] array = stack.ToArray();
		for (int i = 0; i < stack.Count; ++i)
		{
			array[i].transform.position = new Vector3(pos.x, pos.y, pos.z + gap * i);
		}
	}
	public int boatCapacity()
	{
		int capacity = 0;
		for (int i = 0; i < 2; ++i)
		{
			if (boat[i] == null) capacity++;
		}
		return capacity;
	}
	void check()
	{
		int pOnb = 0, dOnb = 0;
		int priests_s = 0, devils_s = 0, priests_e = 0, devils_e = 0;

		if (priests_end.Count == 3 && devils_end.Count == 3)
		{
			my.state = State.WIN;
			return;
		}

		for (int i = 0; i < 2; ++i)
		{
			if (boat[i] != null && boat[i].tag == "Priest") pOnb++;
			else if (boat[i] != null && boat[i].tag == "Devil") dOnb++;
		}
		if (my.state == State.BSTART)
		{
			priests_s = priests_start.Count + pOnb;
			devils_s = devils_start.Count + dOnb;
			priests_e = priests_end.Count;
			devils_e = devils_end.Count;
		}
		else if (my.state == State.BEND)
		{
			priests_s = priests_start.Count;
			devils_s = devils_start.Count;
			priests_e = priests_end.Count + pOnb;
			devils_e = devils_end.Count + dOnb;
		}
		if ((priests_s != 0 && priests_s < devils_s) || (priests_e != 0 && priests_e < devils_e))
		{
			my.state = State.LOSE;
		}
	}
	void Update()
	{
		setCharacterPositions(priests_start, priestStartPos);
		setCharacterPositions(priests_end, priestEndPos);
		setCharacterPositions(devils_start, devilStartPos);
		setCharacterPositions(devils_end, devilEndPos);

		if (my.state == State.BSEMOVING)
		{
			boat_obj.transform.position = Vector3.MoveTowards(boat_obj.transform.position, boatEndPos, speed * Time.deltaTime);
			if (boat_obj.transform.position == boatEndPos)
			{
				my.state = State.BEND;
			}
		}
		else if (my.state == State.BESMOVING)
		{
			boat_obj.transform.position = Vector3.MoveTowards(boat_obj.transform.position, boatStartPos, speed * Time.deltaTime);
			if (boat_obj.transform.position == boatStartPos)
			{
				my.state = State.BSTART;
			}
		}
		else check();
	}
}