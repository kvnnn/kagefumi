using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageManager : GameMonoBehaviour
{
	private GameObject stageGameObject;

	[SerializeField]
	private Transform baseStagePrefab;
	private Transform baseStageTransform;

	private const string STAGE_PREFAB_PATH = "Prefabs/Stages/";

	public void Init()
	{
		SetStage();
	}

	private void SetStage()
	{
		int stageId = 0;

#if UNITY_EDITOR
		// For Debug
		stageId = 0;
#endif

		InstantiateStage(stageId);
	}

	private void InstantiateStage(int id)
	{
		InstantiateBaseStage();

		if (stageGameObject != null)
		{
			Destroy(stageGameObject);
		}

		stageGameObject = Instantiate(Resources.Load<GameObject>(STAGE_PREFAB_PATH + id));
		stageGameObject.transform.SetParent(baseStageTransform);
	}

	private void InstantiateBaseStage()
	{
		if (baseStageTransform == null)
		{
			baseStageTransform = Instantiate(baseStagePrefab).transform;
			baseStageTransform.transform.SetParent(transform);
		}
	}
}
