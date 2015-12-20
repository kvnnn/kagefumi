using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HomeViewManager : ViewManager
{
	private StageCreator stageCreator
	{
		get {return GetComponent<StageCreator>();}
	}

	[SerializeField]
	private StageSelecterManager stageSelecterManager;

	[SerializeField]
	private Text startButtonText;

	private int stageId = 0;

	protected override void BeforeShow(object parameter = null)
	{
		stageCreator.InstantiateBaseStage();
		stageSelecterManager.Init(SelecterClick);
	}

#region Event
	public void StartButtonClick()
	{
		if (stageId == 0) {return;}

		masterManager.ChangeView(MasterManager.View.Game, stageId);
	}

	private void SelecterClick(StageSelecter stage)
	{
		startButtonText.text = "START Stage " + stage.id;
		stageId = stage.id;
	}
#endregion
}
