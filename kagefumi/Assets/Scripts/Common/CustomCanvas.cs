using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomCanvas : GameMonoBehaviour
{
	void Awake()
	{
		Canvas canvas = GetComponent<Canvas>();
		if (canvas.worldCamera == null)
		{
			canvas.worldCamera = Camera.main;
		}
	}
}
