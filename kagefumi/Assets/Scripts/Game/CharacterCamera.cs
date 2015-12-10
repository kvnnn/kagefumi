using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterCamera : GameMonoBehaviour
{
	private Transform characterTransform;

	public void SetCharacter(Transform characterTransform)
	{
		this.characterTransform = characterTransform;
	}

	private void LateUpdate()
	{
		if (characterTransform == null) {return;}
		Vector3 lookAt = characterTransform.position;
		lookAt.y = transform.position.y;
		transform.LookAt(lookAt);
		transform.RotateLocalEulerAnglesX(30f);
	}
}
