using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class EnemyObject : BaseObject
{
	protected override void AddMainCharacterController()
	{
		base.AddMainCharacterController();
		if (mainCharacterController != null)
		{
			mainCharacterController.allowRotation = true;
		}
	}
}
