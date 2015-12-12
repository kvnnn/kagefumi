using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class StageCreator : GameMonoBehaviour
{
	[SerializeField]
	private Transform baseStagePrefab;
	private Transform baseStageTransform;

	private const string STAGE_PREFAB_PATH = "Prefabs/Stages/";
	private const string STAGE_JSON_PATH = "Stages/";

	public GameObject Create(int stageId)
	{
		return InstantiateStage(stageId);
	}

	private GameObject InstantiateStage(int id)
	{
		InstantiateBaseStage();

		GameObject stageGameObject = new GameObject();
		Transform stageTransform = stageGameObject.transform;
		stageTransform.SetParent(baseStageTransform);

		foreach (Dictionary<string, object> json in ParseStageJson(id))
		{
			Transform objectTransform = InstantiateObject(json);
			objectTransform.SetParent(stageTransform);
		}

		return stageGameObject;
	}

	private Transform InstantiateObject(Dictionary<string, object> json)
	{
		string name = json["name"] as string;
		Transform objectTransform = Instantiate(Resources.Load<GameObject>(STAGE_PREFAB_PATH + name)).transform;

		if (json.ContainsKey("position"))
		{
			Vector3 position = CustomVector.ConvertStringToVector3(json["position"] as string);
			objectTransform.position = position;
		}

		if (json.ContainsKey("rotation"))
		{
			Vector3 rotation = CustomVector.ConvertStringToVector3(json["rotation"] as string);
			objectTransform.localEulerAngles = rotation;
		}

		if (json.ContainsKey("scale"))
		{
			Vector3 scale = CustomVector.ConvertStringToVector3(json["scale"] as string);
			objectTransform.localScale = scale;
		}

		return objectTransform;
	}

	private void InstantiateBaseStage()
	{
		if (baseStageTransform == null)
		{
			baseStageTransform = Instantiate(baseStagePrefab).transform;
			baseStageTransform.transform.SetParent(transform);
		}
	}

	private List<Dictionary<string, object>> ParseStageJson(int id)
	{
		TextAsset stageJson = Resources.Load(STAGE_JSON_PATH + id) as TextAsset;
		string stageJsonText = stageJson.text;
		List<object> json = Json.Deserialize(stageJsonText) as List<object>;

		List<Dictionary<string, object>> objectsJson = new List<Dictionary<string, object>>();
		foreach (object obj in json)
		{
			objectsJson.Add(obj as Dictionary<string, object>);
		}

		return objectsJson;
	}
}
