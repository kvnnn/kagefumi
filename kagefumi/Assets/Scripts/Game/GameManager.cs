using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : GameMonoBehaviour
{
	private StageManager stageManager
	{
		get {return GetComponent<StageManager>();}
	}

	private LightManager lightManager
	{
		get {return GetComponent<LightManager>();}
	}

	[SerializeField]
	private GameObject mainCharacterPrefab;
	private MainCharacter mainCharacter;

	private BaseObject diveTarget = null;
	private int stageId;

	public System.Action<bool> onClear;
	public System.Action onGameOver;

#region Init
	public void InitGame(object parameter = null)
	{
		if (parameter != null)
		{
			stageId = (int)parameter;
		}

		stageManager.Init(stageId);
		InitMainCharacter(stageManager.characterDefaultPosition);
	}

	private void InitMainCharacter(Vector3 characterDefaultPosition)
	{
		if (mainCharacter == null || mainCharacter.isDead)
		{
			Transform characterTransform = Instantiate(mainCharacterPrefab).transform;
			characterTransform.SetParent(transform);
			mainCharacter = characterTransform.GetComponent<MainCharacter>();
			mainCharacter.Init(CharacterOnUpdate, OnKeyGet, OnClear, OnGameOver);
		}

		mainCharacter.gameObject.SetActive(false);
		mainCharacter.transform.position = characterDefaultPosition;
		mainCharacter.Reset();
		mainCharacter.gameObject.SetActive(true);
	}

	public void PrepareGame()
	{
		lightManager.Init(stageManager.stageObjects);
	}

	private void Restart()
	{
		InitGame();
		PrepareGame();
	}
#endregion

#region Action
	private void DiveToTarget()
	{
		if (!mainCharacter.Dive()) {return;}
		diveTarget.Dive();
	}

	private void GetOutFromTarget()
	{
		if (!mainCharacter.GetOut(diveTarget.GetOutPosition())) {return;}
		diveTarget.GetOut();

		diveTarget = null;
	}
#endregion

#region Event
	private void CharacterOnUpdate(Vector3 characterPosition)
	{
		BaseObject shadowObject = lightManager.GetShadowObject(characterPosition);

		if (shadowObject != null && diveTarget != shadowObject)
		{
			shadowObject.StartBlink();
		}

		if (diveTarget != null && diveTarget != shadowObject)
		{
			diveTarget.StopBlink();
		}

		diveTarget = shadowObject;
	}

	public void OnDoubleTap()
	{
		if (diveTarget == null) {return;}

		if (!mainCharacter.isDive)
		{
			DiveToTarget();
		}
		else
		{
			GetOutFromTarget();
		}
	}

	public void OnRestartButtonClick()
	{
		Restart();
	}

	public void OnNextStageButtonClick()
	{
		stageId = Stage.NextStageId(stageId);
		Restart();
	}

	private void OnKeyGet()
	{
		stageManager.goalLight.LightOn();
	}

	private void OnClear()
	{
		stageManager.SetSignOff();
		onClear(Stage.LastStage(stageId));

		User.stageId = Stage.NextStageId(stageId);
	}

	private void OnGameOver()
	{
		onGameOver();
	}
#endregion
}
