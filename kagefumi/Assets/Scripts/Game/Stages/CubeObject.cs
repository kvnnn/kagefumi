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
		// return new Vector3(transform.position.x, transform.position.y + transform.localScale.y/2, transform.position.z);
	}

	public bool IsSide(Vector3 position)
	{
		return position.y < transform.position.y;
	}

	public float TopPositionY()
	{
		return transform.position.y + transform.localScale.y/2;
	}
}
