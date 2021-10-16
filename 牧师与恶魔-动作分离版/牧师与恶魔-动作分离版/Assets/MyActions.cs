using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GSCsettings;

public class MyActions : MonoBehaviour
{
	GameSceneController my;
	// 将 MyActions 对象注入了 GameSceneController 单实例对象中
	void Start()
	{
		my = GameSceneController.GetInstance();
		my.setMyActions(this);
	}
	//动作部分（与上次作业基本一致）
	public void getOnTheBoat(GenGameObject gobj, GameObject obj)
	{
		if (gobj.boatCapacity() != 0)
		{
			obj.transform.parent = gobj.boat_obj.transform;
			if (gobj.boat[0] == null)
			{
				gobj.boat[0] = obj;
				obj.transform.localPosition = new Vector3(0, 1.2f, -0.3f);
			}
			else
			{
				gobj.boat[1] = obj;
				obj.transform.localPosition = new Vector3(0, 1.2f, 0.3f);
			}
		}
	}
	public void moveBoat(GenGameObject gobj)
	{
		if (gobj.boatCapacity() != 2)
		{
			if (my.state == State.BSTART)
			{
				my.state = State.BSEMOVING;
			}
			else if (my.state == State.BEND)
			{
				my.state = State.BESMOVING;
			}
		}
	}
	public void getOffTheBoat(GenGameObject gobj, int side)
	{
		if (gobj.boat[side] != null)
		{
			gobj.boat[side].transform.parent = null;
			if (my.state == State.BEND)
			{
				if (gobj.boat[side].tag == "Priest")
				{
					gobj.priests_end.Push(gobj.boat[side]);
				}
				else if (gobj.boat[side].tag == "Devil")
				{
					gobj.devils_end.Push(gobj.boat[side]);
				}
			}
			else if (my.state == State.BSTART)
			{
				if (gobj.boat[side].tag == "Priest")
				{
					gobj.priests_start.Push(gobj.boat[side]);
				}
				else if (gobj.boat[side].tag == "Devil")
				{
					gobj.devils_start.Push(gobj.boat[side]);
				}
			}
			gobj.boat[side] = null;
		}
	}
	public void priestStartOnBoat(GenGameObject gobj)
	{
		if (gobj.priests_start.Count != 0 && gobj.boatCapacity() != 0 && my.state == State.BSTART)
			getOnTheBoat(gobj, gobj.priests_start.Pop());
	}

	public void priestEndOnBoat(GenGameObject gobj)
	{
		if (gobj.priests_end.Count != 0 && gobj.boatCapacity() != 0 && my.state == State.BEND)
			getOnTheBoat(gobj, gobj.priests_end.Pop());
	}

	public void devilStartOnBoat(GenGameObject gobj)
	{
		if (gobj.devils_start.Count != 0 && gobj.boatCapacity() != 0 && my.state == State.BSTART)
			getOnTheBoat(gobj, gobj.devils_start.Pop());
	}

	public void devilEndOnBoat(GenGameObject gobj)
	{
		if (gobj.devils_end.Count != 0 && gobj.boatCapacity() != 0 && my.state == State.BEND)
			getOnTheBoat(gobj, gobj.devils_end.Pop());
	}
}
