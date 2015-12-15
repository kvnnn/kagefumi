using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class ShadowDetector : GameMonoBehaviour
{
	public BaseObject[] objects {get; private set;}

	public void Init(BaseObject[] objects)
	{
		this.objects = objects;
	}

	public void UpdateShadowData()
	{
		if (objects == null) {return;}

		foreach (BaseObject obj in objects)
		{
			obj.UpdateShadowBounds(transform.position, GetComponent<Light>().range);
		}
	}

	private bool IsObjectInLight(BaseObject obj)
	{
		Vector3 targetDir = obj.transform.position - transform.position;
		Vector3 forward = transform.forward;
		float angle = Vector3.Angle(targetDir, forward);

		return angle < GetComponent<Light>().spotAngle/2 && Vector3.Distance(obj.transform.position, transform.position) < GetComponent<Light>().range;
	}

	public BaseObject GetShadowObject(Vector2 position)
	{
		if (objects == null) {return null;}

		foreach (BaseObject obj in objects)
		{
			if (IsOnShadow(position, obj.shadowPointList))
			{
				return obj;
			}
		}

		return null;
	}

	private bool IsOnShadow(Vector2 p, List<Vector2> pointList)
	{
		bool isInside = false;
		if (pointList.Count <= 2) {return isInside;}

		int j = pointList.Count - 1;
		for (int i = 0; i < pointList.Count; j = i++)
		{
			if (((pointList[i].y <= p.y && p.y < pointList[j].y) || (pointList[j].y <= p.y && p.y < pointList[i].y)) && (p.x < (pointList[j].x - pointList[i].x) * (p.y - pointList[i].y) / (pointList[j].y - pointList[i].y) + pointList[i].x))
			{
				isInside = !isInside;
			}
		}

		return isInside;
	}
}
