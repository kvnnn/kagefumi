using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class KeyTrigger : BaseTrigger
{
	public void Get()
	{
		Destroy(gameObject);
	}
}
