using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomeViewManager : ViewManager
{
	private StageCreator stageCreator
	{
		get {return GetComponent<StageCreator>();}
	}

	protected override void BeforeShow()
	{
		stageCreator.InstantiateBaseStage();
	}

	public void StartButtonClick()
	{
		masterManager.ChangeView(MasterManager.View.Game);
	}
}
