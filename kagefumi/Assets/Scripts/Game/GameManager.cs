using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : GameMonoBehaviour
{
	private StageManager stageManager
	{
		get {return GetComponent<StageManager>();}
	}

	[SerializeField]
	private GameObject mainCharacterPrefab;
	private MainCharacter mainCharacter;

#region Init
	public void InitGame()
	{
		stageManager.Init();
		InitMainCharacter();

		PrepareGame();
	}

	private void InitMainCharacter()
	{
		if (mainCharacter == null || mainCharacter.isDead)
		{
			Transform characterTransform = Instantiate(mainCharacterPrefab).transform;
			characterTransform.SetParent(transform);
			characterTransform.MoveY(characterTransform.localScale.y);
			mainCharacter = characterTransform.GetComponent<MainCharacter>();
		}

		Camera.main.gameObject.GetComponent<CharacterCamera>().SetCharacter(mainCharacter.transform);
	}
#endregion

	public void PrepareGame()
	{

	}
}
