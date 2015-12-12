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

	private CharacterCamera characterCamera
	{
		get {return Camera.main.gameObject.GetComponent<CharacterCamera>();}
	}

	[SerializeField]
	private GameObject mainCharacterPrefab;
	private MainCharacter mainCharacter;

	private BaseObject diveTarget = null;

#region Init
	public void InitGame()
	{
		stageManager.Init();
		InitMainCharacter();

		characterCamera.Init(mainCharacter.transform);
	}

	private void InitMainCharacter()
	{
		if (mainCharacter == null || mainCharacter.isDead)
		{
			Transform characterTransform = Instantiate(mainCharacterPrefab).transform;
			characterTransform.SetParent(transform);
			characterTransform.MoveY(characterTransform.localScale.y);
			mainCharacter = characterTransform.GetComponent<MainCharacter>();
			mainCharacter.Init(CharacterOnUpdate);
		}
	}

	public void PrepareGame()
	{
		characterCamera.CalculateBounds();
		lightManager.Init(stageManager.stageObjects);
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

		if (mainCharacter.isActive)
		{
			mainCharacter.SetActive(false);
			diveTarget.Dive();

			characterCamera.SetCharacter(diveTarget.transform);
		}
		else
		{
			mainCharacter.SetActive(true);
			diveTarget.GetOut();
			diveTarget = null;

			characterCamera.SetCharacter(mainCharacter.transform);
		}

		lightManager.UpdateShadowData();
	}

	public void OnRestartButtonClick()
	{
	}
#endregion
}
