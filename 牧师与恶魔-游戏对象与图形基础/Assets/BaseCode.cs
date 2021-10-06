using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using GSCsettings;

namespace GSCsettings
{
	public enum State { BSTART, BSEMOVING, BESMOVING, BEND, WIN, LOSE };
	/* 
	 * BSTART:  boat stops on start shore 
	 * BEND:    boat stops on end shore 
	 * BSEMOVING:   boat is moving from start shore to end shore 
	 * BESMOVING:   boat is moving from end shore to start shore 
	 * WIN:     win 
	 * LOSE:    lose 
	 */

	public interface IUserActions
	{
		void priestSOnB();
		void priestEOnB();
		void devilSOnB();
		void devilEOnB();
		void moveBoat();
		void offBoatL();
		void offBoatR();
		void restart();
	}

	public class GameSceneController : System.Object, IUserActions
	{
		private static GameSceneController _instance;
		private BaseCode _basecode;
		private GenGameObject _gengameobj;
		public State state = State.BSTART;

		public static GameSceneController GetInstance()
		{
			if (_instance == null)
			{
				_instance = new GameSceneController();
				return _instance;
			}
			else
				return _instance;
		}
		public BaseCode getBaseCode()
		{
			return _basecode;
		}
		internal void setBaseCode(BaseCode basecode)
		{
			if (_basecode == null)
			{
				_basecode = basecode;
			}
		}
		public GenGameObject getGenGameObject()
		{
			return _gengameobj;
		}
		internal void setGenGameObject(GenGameObject gengameobj)
		{
			if (_gengameobj == null)
			{
				_gengameobj = gengameobj;
			}
		}

		public void priestSOnB()
		{
			_gengameobj.priestStartOnBoat();
		}

		public void priestEOnB()
		{
			_gengameobj.priestEndOnBoat();
		}

		public void devilSOnB()
		{
			_gengameobj.devilStartOnBoat();
		}

		public void devilEOnB()
		{
			_gengameobj.devilEndOnBoat();
		}

		public void moveBoat()
		{
			_gengameobj.moveBoat();
		}

		public void offBoatL()
		{
			_gengameobj.getOffTheBoat(0);
		}

		public void offBoatR()
		{
			_gengameobj.getOffTheBoat(1);
		}

		public void restart()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			state = State.BSTART;
		}
	}
}

public class BaseCode : MonoBehaviour
{
	void Start()
	{
		GameSceneController my = GameSceneController.GetInstance();
		my.setBaseCode(this);
	}
}
	