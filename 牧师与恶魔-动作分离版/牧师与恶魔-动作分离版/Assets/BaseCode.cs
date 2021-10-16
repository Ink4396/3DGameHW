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
		private MyActions _actionManager;
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

		public MyActions getMyActions()
		{
			return _actionManager;
		}
		internal void setMyActions(MyActions actionManager)
		{
			if (_actionManager == null)
			{
				_actionManager = actionManager;
			}
		}

		public void priestSOnB()
		{
			_actionManager.priestStartOnBoat(_gengameobj);
		}

		public void priestEOnB()
		{
			_actionManager.priestEndOnBoat(_gengameobj);
		}

		public void devilSOnB()
		{
			_actionManager.devilStartOnBoat(_gengameobj);
		}

		public void devilEOnB()
		{
			_actionManager.devilEndOnBoat(_gengameobj);
		}

		public void moveBoat()
		{
			_actionManager.moveBoat(_gengameobj);
		}

		public void offBoatL()
		{
			_actionManager.getOffTheBoat(_gengameobj,0);
		}

		public void offBoatR()
		{
			_actionManager.getOffTheBoat(_gengameobj,1);
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
	