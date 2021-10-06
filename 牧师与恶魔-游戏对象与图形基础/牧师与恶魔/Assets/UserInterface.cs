
using UnityEngine;
using System.Collections;
using GSCsettings;

public class UserInterface : MonoBehaviour
{

	GameSceneController my;
	IUserActions action;

	float width, height;

	float castw(float scale)
	{
		return (Screen.width - width) / scale;
	}

	float casth(float scale)
	{
		return (Screen.height - height) / scale;
	}

	void Start()
	{
		my = GameSceneController.GetInstance();
		action = GameSceneController.GetInstance() as IUserActions;
	}

	void OnGUI()
	{
		width = Screen.width / 10;
		height = Screen.height / 10;
		print(my.state);
		if (my.state == State.WIN)
		{
			if (GUI.Button(new Rect(castw(2f), casth(6f), width, height), "Win!"))
			{
				action.restart();
			}
		}
		else if (my.state == State.LOSE)
		{
			if (GUI.Button(new Rect(castw(2f), casth(6f), width, height), "Lose!"))
			{
				action.restart();
			}
		}
		else if (my.state == State.BSTART || my.state == State.BEND)
		{
			if (GUI.Button(new Rect(castw(2.0f), casth(1.3f), width, height), "Boat Go"))
			{
				action.moveBoat();
			}
			if (GUI.Button(new Rect(castw(10.5f), casth(4f), width, height), "Devil On"))
			{
				action.devilSOnB();
			}
			if (GUI.Button(new Rect(castw(4.3f), casth(4f), width, height), "Priest On"))
			{
				action.priestSOnB();
			}
			if (GUI.Button(new Rect(castw(1.1f), casth(4f), width, height), "Deivl On"))
			{
				action.devilEOnB();
			}
			if (GUI.Button(new Rect(castw(1.3f), casth(4f), width, height), "Priest On"))
			{
				action.priestEOnB();
			}
			if (GUI.Button(new Rect(castw(2.9f), casth(1.3f), width, height), "Left Off"))
			{
				action.offBoatL();
			}
			if (GUI.Button(new Rect(castw(1.55f), casth(1.3f), width, height), "Right Off"))
			{
				action.offBoatR();
			}
		}
	}
}