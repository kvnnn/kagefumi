using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainCharacter : GameMonoBehaviour
{
	public System.Action<Vector3> onUpdate;

	private CharacterController controller
	{
		get {return GetComponent<CharacterController>();}
	}

	public bool isActive
	{
		get {return gameObject.activeSelf;}
	}

	public void SetActive(bool active)
	{
		gameObject.SetActive(active);
	}

	public bool isDead
	{
		get {return gameObject == null;}
	}

	private void Update()
	{
		Vector3 moveDirection = Vector3.zero;

		if(Input.GetKey(KeyCode.W))
		{
			moveDirection.z = 1;
		}

		if(Input.GetKey(KeyCode.A))
		{
			moveDirection.x = -1;
		}

		if(Input.GetKey(KeyCode.S))
		{
			moveDirection.z = -1;
		}

		if(Input.GetKey(KeyCode.D))
		{
			moveDirection.x = 1;
		}

		if (moveDirection != Vector3.zero)
		{
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= 5.0f;
			moveDirection *= Time.deltaTime;

			if (controller != null)
			{
				MoveByCharacterController(moveDirection);
			}

			if (onUpdate != null)
			{
				onUpdate(transform.position);
			}
		}
	}

	private void MoveByCharacterController(Vector3 direction)
	{
		controller.Move(direction);
	}
}
