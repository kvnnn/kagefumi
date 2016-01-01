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
			if (shadowDetector == null || !shadowDetector.isActive) {continue;}
			shadowDetector.UpdateShadowData();
		}
	}

	public BaseObject GetShadowObject(Vector3 position)
	{
		Vector2 positionInVector2 = new Vector2(position.x, position.z);

		foreach (ShadowDetector shadowDetector in shadowDetectors)
		{
			if (shadowDetector == null || !shadowDetector.isActive) {continue;}
			foreach (Vector2 offset in offsetList)
			{
				positionInVector2 = new Vector2(positionInVector2.x + offset.x, positionInVector2.y + offset.y);

				BaseObject baseObject = shadowDetector.GetShadowObject(positionInVector2, position.y);
				if (baseObject != null)
				{
					return baseObject;
				}
			}
		}
		return null;
	}

	private Vector2[] offsetList_;
	private Vector2[] offsetList
	{
		get
		{
			if (offsetList_ == null)
			{
				float offsetX, offsetY, offset;
				offset = offsetX = offsetY = 0.05f;

				offsetList_ = new Vector2[9];
				for (int i = 0; i < 9; i++)
				{
					if (i%3 == 0)
					{
						offsetX = offset;
						if (i/3 != 0)
						{
							offsetY -= offset;
						}
					}

					offsetList_[i] = new Vector2(offsetX, offsetY);
				}
			}

			return offsetList_;
		}
	}
}
