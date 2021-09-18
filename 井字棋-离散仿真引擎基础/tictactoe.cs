//tictactoe.cs

using UnityEngine;
using System.Collections;
public class tictactoe : MonoBehaviour
{
	// use 2 dimension arrary to store the game data, 1 means O, 2 means X, 0 means blank.
	private int[,] map = new int[3, 3];
	// 0 means O's turn, 1 means X's turn.
	private int turn = 0;
	// Use this for initialization
	void Start()
	{
		reset();
	}
	// OnGUI method will refresh automaticlly
	void OnGUI()
	{
		GUIStyle Ostyle = new GUIStyle();
		Ostyle.fontSize = 30;
		Ostyle.normal.textColor = new Color(0f / 256f, 205f / 256f, 0f / 256f, 256f / 256f);
		GUIStyle Xstyle = new GUIStyle();
		Xstyle.fontSize = 30;
		Xstyle.normal.textColor = new Color(65f / 256f, 105f / 256f, 225f / 256f, 256f / 256f);
		GUIStyle Tstyle = new GUIStyle();
		Tstyle.fontSize = 30;
		Tstyle.normal.textColor = new Color(255f / 256f, 255f / 256f, 48f / 256f, 256f / 256f);
		if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 + 130, 180, 60), "Restart"))
			reset();
		int result = check();
		// display the result
		if (result == 1)
		{
			GUI.Label(new Rect(Screen.width / 2 - 90, Screen.height / 2 + 90, 90, 40), "O wins", Ostyle);
		}
		else if (result == 2)
		{
			GUI.Label(new Rect(Screen.width / 2 - 90, Screen.height / 2 + 90, 90, 40), "X wins", Xstyle);
		}
		else if (result == 3)
		{
			GUI.Label(new Rect(Screen.width / 2 - 90, Screen.height / 2 + 90, 90, 40), "Draw", Tstyle);
		}
		int i = 0, j = 0;
		for (i = 0; i < 3; i++)
		{
			for (j = 0; j < 3; j++)
			{
				if (map[i, j] == 1)
				{
					GUI.Button(new Rect(Screen.width / 2 - 90 + 60 * i, Screen.height / 2 - 90 + 60 * j, 60, 60), "O");
				}
				else if (map[i, j] == 2)
				{
					GUI.Button(new Rect(Screen.width / 2 - 90 + 60 * i, Screen.height / 2 - 90 + 60 * j, 60, 60), "X");
				}
				else
				{
					if (GUI.Button(new Rect(Screen.width / 2 - 90 + 60 * i, Screen.height / 2 - 90 + 60 * j, 60, 60), ""))
					{
						if (result == 0)
						{
							if (turn == 0)
							{
								map[i, j] = 1;
								turn = 1;
							}
							else
							{
								map[i, j] = 2;
								turn = 0;
							}
						}
					}
				}
			}
		}
	}
	void reset()
	{
		// set the map blank
		int i = 0, j = 0;
		for (i = 0; i < 3; i++)
		{
			for (j = 0; j < 3; j++)
			{
				map[i, j] = 0;
			}
		}
		turn = 0;
	}
	// 1 means O wins, 2 means X wins, 3 means Draw, 0 means playing.
	int check()
	{
		int trigger = 0;
		int i = 0, j = 0;
		//check col
		for (i = 0; i < 3; i++)
		{
			if (map[i, 0] == map[i, 1] && map[i, 1] == map[i, 2])
			{
				if (map[i, 0] != 0)
					return map[i, 0];
			}
		}
		// check row
		for (j = 0; j < 3; j++)
		{
			if (map[0, j] == map[1, j] && map[1, j] == map[2, j])
			{
				if (map[0, j] != 0)
					return map[0, j];
			}
		}
		// check diagonal
		if ((map[0, 0] == map[1, 1] && map[1, 1] == map[2, 2]) ||
			(map[0, 2] == map[1, 1] && map[1, 1] == map[2, 0]))
		{
			if (map[1, 1] != 0)
				return map[1, 1];
		}
		for (i = 0; i < 3; i++)
		{
			for (j = 0; j < 3; j++)
			{
				if (map[i, j] == 0)
				{
					trigger = 1;
					break;
				}
			}
		}
		if (trigger == 0)
			return 3;
		return 0;
	}
}
