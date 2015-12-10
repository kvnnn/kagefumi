using UnityEngine;
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
}
