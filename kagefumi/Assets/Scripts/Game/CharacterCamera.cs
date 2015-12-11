using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterCamera : GameMonoBehaviour
{
	private Transform characterTransform;

	private const float CAMERA_SPEED = 5.0f;
	private const float CAMERA_ANGLE_X = 30f;

	public void SetCharacter(Transform characterTransform)
	{
		this.characterTransform = characterTransform;
	}

	private void LateUpdate()
	{
		if (characterTransform == null) {return;}
		var rotation = Quaternion.LookRotation(characterTransform.position - transform.position);
		rotation.eulerAngles = new Vector3(CAMERA_ANGLE_X, rotation.eulerAngles.y, rotation.eulerAngles.z);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, CAMERA_SPEED * Time.deltaTime);
	}
}
