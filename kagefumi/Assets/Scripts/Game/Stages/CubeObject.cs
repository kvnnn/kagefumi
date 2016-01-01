using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class CubeObject : BaseObject
{
	private const float CLIMBABLE_SCALE = 1.1f;

	public override Vector3 GetOutPosition()
	{
		return base.GetOutPosition();
	}

	public Vector3 TopPosition()
	{
		return new Vector3(transform.position.x, TopPositionY(), transform.position.z);
	}

	private float TopPositionY()
	{
		return transform.position.y + transform.localScale.y/2;
	}

	public bool IsClimbable(float positionY)
	{
		return positionY < TopPositionY() && (positionY >= TopPositionY() - CLIMBABLE_SCALE);
	}
}
