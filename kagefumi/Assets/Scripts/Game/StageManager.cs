using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageManager : GameMonoBehaviour
{
	private GameObject stageGameObject;

	private StageCreator creator
	{
		get {return GetComponent<StageCreator>();}
	}

	public Vector3 characterDefaultPosition
	{
		get {return creator.characterDefaultPosition;}
	}

	private BaseObject[] stageObjects_;
	public BaseObject[] stageObjects
	{
		get
		{
			if (stageObjects_ == null)
			{
				stageObjects_ = stageGameObject.GetComponentsInChildren<BaseObject>();
			}

			return stageObjects_;
		}
	}

	public GoalLight goalLight
	{
		get {return creator.goalLight;}
	}

	public void Init(int id)
	{
		SetStage(id);
	}

	private void SetStage(int stageId)
	{
		DestoryStageIfExist();

#if UNITY_EDITOR
		// For Debug
		// stageId = 0;

		stageGameObject = creator.Create(stageId);
#else
		stageGameObject = creator.Create(stageId);
#endif

		goalLight.LightOff();
	}

	private void DestoryStageIfExist()
	{
		if (stageGameObject != null)
		{
			stageObjects_ = null;
			Destroy(stageGameObject);
		}
	}

	public void SetSignOff()
	{
		foreach (BaseObject obj in stageObjects)
		{
			if (obj is SignboardObject)
			{
				SignboardObject signboard = obj as SignboardObject;
				signboard.HideText();
			}
		}
	}
}
