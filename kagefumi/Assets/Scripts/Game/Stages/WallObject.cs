using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class WallObject : BaseObject
{
	public override bool isDivable
	{
		get {return false;}
	}
}
