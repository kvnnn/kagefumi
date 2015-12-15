using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightManager : GameMonoBehaviour
{
	private ShadowDetector[] shadowDetectors_;
	private ShadowDetector[] shadowDetectors
	{
		get
		{
			if (shadowDetectors_ == null)
			{
				shadowDetectors_ = GetComponentsInChildren<ShadowDetector>();
			}

			return shadowDetectors_;
		}
	}

	public void Init(BaseObject[] objects)
	{
		shadowDetectors_ = null;

		foreach (ShadowDetector shadowDetector in shadowDetectors)
		{
			shadowDetector.Init(objects);
		}

		UpdateShadowData();
	}

	private void Update()
	{
		UpdateShadowData();
	}

	public void UpdateShadowData()
	{
		foreach (ShadowDetector shadowDetector in shadowDetectors)
		{
			shadowDetector.UpdateShadowData();
		}
	}

	public BaseObject GetShadowObject(Vector3 position)
	{
		Vector2 positionInVector2 = new Vector2(position.x, position.z);

		foreach (ShadowDetector shadowDetector in shadowDetectors)
		{
			BaseObject baseObject = shadowDetector.GetShadowObject(positionInVector2);
			if (baseObject != null)
			{
				return baseObject;
			}
		}
		return null;
	}
}
