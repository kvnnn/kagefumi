using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class GoalLight : GameMonoBehaviour
{
	[SerializeField]
	private Light spotLight;

	public void LightOn()
	{
		spotLight.enabled = true;
	}

	public void LightOff()
	{
		spotLight.enabled = false;
	}
}
