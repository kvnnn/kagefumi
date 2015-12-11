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

#if UNITY_EDITOR
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {moveDirection.z = 1;}
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {moveDirection.x = -1;}
		if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {moveDirection.z = -1;}
		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {moveDirection.x = 1;}
#endif

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
