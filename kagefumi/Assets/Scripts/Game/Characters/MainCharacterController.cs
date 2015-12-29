using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using System.Collections.Generic;

public class MainCharacterController : GameMonoBehaviour
{
	private bool isMoveLock = false;
	public bool lockMove
	{
		set
		{
			isMoveLock = value;
		}
	}

	public System.Action<Vector3> onUpdate;

	private CharacterController controller
	{
		get {return GetComponent<CharacterController>();}
	}

	private void OnEnable()
	{
		if (onUpdate != null)
		{
			onUpdate(transform.position);
		}
	}

	private void Update()
	{
		Vector3 direction = Vector3.zero;

		if (!isMoveLock)
		{
#if UNITY_EDITOR
			if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {direction.z = 1;}
			if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {direction.x = -1;}
			if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {direction.z = -1;}
			if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {direction.x = 1;}
#endif

			float x = CrossPlatformInputManager.GetAxisRaw("Horizontal");
			float z = CrossPlatformInputManager.GetAxisRaw("Vertical");
			if (x != 0 && z != 0)
			{
				direction = new Vector3(x, 0f, z).normalized;
			}

		}

		if (controller != null)
		{
			MoveByCharacterController(direction);
		}

		if (onUpdate != null)
		{
			onUpdate(transform.position);
		}
	}

	private void MoveByCharacterController(Vector3 direction)
	{
		transform.LookAt(transform.position + direction);

		Vector3 forward = Vector3.zero;
		if (direction != Vector3.zero)
		{
			forward = transform.TransformDirection(Vector3.forward);
			forward *= 5.0f;
		}

		controller.SimpleMove(forward);
	}
}
