using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class CubeObject : BaseObject
{
	public override Vector3 GetOutPosition()
	{
		return new Vector3(transform.position.x, transform.position.y + transform.localScale.y/2, transform.position.z);
	}
}
