using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : GameMonoBehaviour
{
	private StageManager stageManager
	{
		get {return gameObject.GetComponent<StageManager>();}
	}

#region Init
	public void InitGame()
	{
		stageManager.Init();

		PrepareGame();
	}
#endregion

	public void PrepareGame()
	{

	}
}
