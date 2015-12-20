using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class KeyTrigger : BaseTrigger
{
	private const float ROTATE_SPEED = 1.5f;

	private void Start()
	{
		LeanTween.rotateAround(gameObject, Vector3.up, 360f, ROTATE_SPEED).setRepeat(-1);
	}

	public void Get()
	{
		Destroy(gameObject);
	}
}
