using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainCharacter : GameMonoBehaviour
{
	private CharacterController controller
	{
		get {return GetComponent<CharacterController>();}
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
			controller.Move(moveDirection * Time.deltaTime);
		}
	}
}
