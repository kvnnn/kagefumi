using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomeCharacter : GameMonoBehaviour
{
	private Vector3 targetPosition;

	private const float SPEED = 5.0f;
	private const float Z_OFFSET = -1.0f;

	private CharacterController characterController
	{
		get {return GetComponent<CharacterController>();}
	}

	public void Init()
	{
		LookFront();
	}

	private void LookFront()
	{
		transform.RotateLocalEulerAnglesY(180f);
	}

	public void SetTargetPosition(Vector3 position)
	{
		targetPosition = new Vector3(position.x, 0f , position.z + Z_OFFSET);
		transform.LookAt(targetPosition);
	}

	private void Update()
	{
		if (targetPosition == Vector3.zero) {return;}
		MoveTowardTargetPosition();
	}

	private void MoveTowardTargetPosition()
	{
		Vector3 distance = targetPosition - transform.position;

		if(distance.magnitude > 0.1f)
		{
			characterController.Move(distance.normalized * SPEED * Time.deltaTime);
		}
		else
		{
			EndMove();
		}
	}

	private void EndMove()
	{
		targetPosition = Vector3.zero;
		LookFront();
	}
}
