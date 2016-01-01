using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class StageCreator : GameMonoBehaviour
{
	[SerializeField]
	private Transform baseStagePrefab;
	private Transform baseStageTransform;

	public GoalLight goalLight
	{
		get {return baseStageTransform.GetComponentInChildren<GoalLight>();}
	}

	public Vector3 characterDefaultPosition {get; private set;}

	private const string STAGE_PREFAB_PATH = "Prefabs/Stages/";
	private const string STAGE_JSON_PATH = "Stages/";

	private const string DEBUG_STAGE = "DebugStage";

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
			if (objectTransform == null) {continue;}
			objectTransform.SetParent(stageTransform);
		}

		return stageGameObject;
	}

	private Transform InstantiateObject(Dictionary<string, object> json)
	{
		string name = json["name"] as string;
		GameObject objectGameObject = Instantiate(Resources.Load<GameObject>(STAGE_PREFAB_PATH + name));

		if (objectGameObject == null) {return null;}
		Transform objectTransform = objectGameObject.transform;

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

		if (json.ContainsKey("text"))
		{
			string text = json["text"] as string;
			SignboardObject signboard = objectTransform.GetComponent<SignboardObject>();
			if (signboard != null)
			{
				signboard.SetText(text);
			}
		}

		return objectTransform;
	}

	public void InstantiateBaseStage()
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
		Dictionary<string, object> json = Json.Deserialize(stageJsonText) as Dictionary<string, object>;

		SetCharacterDefaultPosition(CustomVector.ConvertStringToVector3(json["character"] as string));

		SetMainLight(json.ContainsKey("main_light_shadow"));

		SetSubLight(json.ContainsKey("sub_light"));

		List<Dictionary<string, object>> objectsJson = new List<Dictionary<string, object>>();
		foreach (object obj in json["objects"] as List<object>)
		{
			objectsJson.Add(obj as Dictionary<string, object>);
		}

		return objectsJson;
	}

	private void SetCharacterDefaultPosition(Vector3 characterDefaultPosition)
	{
		this.characterDefaultPosition = characterDefaultPosition;
	}

	private void SetMainLight(bool hasKey)
	{
		bool setShadow = hasKey;

		foreach (Light light in baseStageTransform.GetComponentsInChildren<Light>())
		{
			if (light.type == LightType.Directional)
			{
				light.shadows = setShadow ? LightShadows.Hard : LightShadows.None;
			}
		}
	}

	private void SetSubLight(bool hasKey)
	{
		bool showLight = !hasKey;

		foreach (Light light in baseStageTransform.GetComponentsInChildren<Light>())
		{
			if (light.type == LightType.Point)
			{
				light.enabled = showLight;
			}
		}
	}

#if UNITY_EDITOR
	public GameObject CreateDebugStage()
	{
		InstantiateBaseStage();
		GameObject stageGameObject = Instantiate(Resources.Load<GameObject>(STAGE_PREFAB_PATH + DEBUG_STAGE));
		stageGameObject.transform.SetParent(baseStageTransform);

		characterDefaultPosition = new Vector3(0f, 1f, 0f);

		return stageGameObject;
	}
#endif
}
