using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class CubeObject : BaseObject
{
	private const float CLIMBABLE_SCALE = 1f;

	public bool isClimbable
	{
		get
		{
			return transform.localScale.y <= CLIMBABLE_SCALE;
		}
	}

	public override Vector3 GetOutPosition()
	{
		return base.GetOutPosition();
	}

	public bool IsSide(Vector3 position)
	{
		return position.y < transform.position.y;
	}

	public Vector3 TopPosition()
	{
		return new Vector3(transform.position.x, TopPositionY(), transform.position.z);
	}

	public float TopPositionY()
	{
		return transform.position.y + transform.localScale.y/2;
	}
}
