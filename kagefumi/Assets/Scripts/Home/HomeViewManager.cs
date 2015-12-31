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
	private GameObject characterPrefab;
	private HomeCharacter character;

	[SerializeField]
	private Text startButtonText;

	private int stageId = 0;

	protected override void BeforeShow(object parameter = null)
	{
		stageCreator.InstantiateBaseStage();
		InstantiateCharacter();

		stageSelecterManager.Init(SelecterClick);
	}

	private void InstantiateCharacter()
	{
		if (character == null)
		{
			character = Instantiate(characterPrefab).GetComponent<HomeCharacter>();
			character.transform.SetParent(transform);
			character.Init();
		}
	}

#region Event
	public void StartButtonClick()
	{
		if (stageId == 0) {return;}

		masterManager.ChangeView(MasterManager.View.Game, stageId);
	}

	private void SelecterClick(StageSelecter stage)
	{
		if (stageId == stage.id) {return;}

		startButtonText.text = "START\nStage " + stage.id;
		stageId = stage.id;

		character.SetTargetPosition(stage.transform.position);
	}
#endregion
}
