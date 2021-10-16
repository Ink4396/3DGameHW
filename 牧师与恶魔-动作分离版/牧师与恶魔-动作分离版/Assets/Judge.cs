using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GSCsettings;

public class Judge : MonoBehaviour
{
	GameSceneController my;
	void Start()
	{
		my = GameSceneController.GetInstance();
	}
    // Update is called once per frame
    void Update()
	{
		int pOnb = 0, dOnb = 0;
		int priests_s = 0, devils_s = 0, priests_e = 0, devils_e = 0;
		GenGameObject gobj = my.getGenGameObject();
		if (my.state == State.BSEMOVING)
		{
			gobj.boat_obj.transform.position = Vector3.MoveTowards(gobj.boat_obj.transform.position, gobj.boatEndPos, gobj.speed * Time.deltaTime);
			if (gobj.boat_obj.transform.position == gobj.boatEndPos)
			{
				my.state = State.BEND;
			}
		}
		else if (my.state == State.BESMOVING)
		{
			gobj.boat_obj.transform.position = Vector3.MoveTowards(gobj.boat_obj.transform.position, gobj.boatStartPos, gobj.speed * Time.deltaTime);
			if (gobj.boat_obj.transform.position == gobj.boatStartPos)
			{
				my.state = State.BSTART;
			}
		}
		else
		{
			if (gobj.priests_end.Count == 3 && gobj.devils_end.Count == 3)
			{
				my.state = State.WIN;
				return;
			}

			for (int i = 0; i < 2; ++i)
			{
				if (gobj.boat[i] != null && gobj.boat[i].tag == "Priest") pOnb++;
				else if (gobj.boat[i] != null && gobj.boat[i].tag == "Devil") dOnb++;
			}
			if (my.state == State.BSTART)
			{
				priests_s = gobj.priests_start.Count + pOnb;
				devils_s = gobj.devils_start.Count + dOnb;
				priests_e = gobj.priests_end.Count;
				devils_e = gobj.devils_end.Count;
			}
			else if (my.state == State.BEND)
			{
				priests_s = gobj.priests_start.Count;
				devils_s = gobj.devils_start.Count;
				priests_e = gobj.priests_end.Count + pOnb;
				devils_e = gobj.devils_end.Count + dOnb;
			}
			if ((priests_s != 0 && priests_s < devils_s) || (priests_e != 0 && priests_e < devils_e))
			{
				my.state = State.LOSE;
			}
		}
	}
}
