using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameViewManager : ViewManager
{
	private GameManager gameManager
	{
		get {return GetComponent<GameManager>();}
	}

	[SerializeField]
	private GameUIManager gameUIManager;
	private Transform uiBaseTransform
	{
		get {return gameUIManager.transform;}
	}

	protected override void BeforeShow(object parameter = null)
	{
		gameUIManager.Init(gameManager.OnDoubleTap, OnHomeButtonClick, gameManager.OnRestartButtonClick);
		gameManager.InitGame(parameter);
	}

	protected override void AfterShow()
	{
		gameManager.PrepareGame();
	}

	private T InstantiateUI<T>(GameObject prefab)
		where T : BaseUIParts
	{
		T uiParts = uiBaseTransform.GetComponentInChildren<T>();
		if (uiParts == null)
		{
			GameObject uiPartsGameObject = Instantiate(prefab);
			uiPartsGameObject.transform.SetParent(uiBaseTransform);
			uiParts = uiPartsGameObject.GetComponent<T>();
		}

		uiParts.Init();
		return uiParts;
	}

#region Event
	private void OnHomeButtonClick()
	{
		masterManager.ChangeView(MasterManager.View.Home);
	}
#endregion
}
