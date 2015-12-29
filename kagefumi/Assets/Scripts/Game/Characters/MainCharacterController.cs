using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using System.Collections.Generic;

public class MainCharacterController : GameMonoBehaviour
{
	private bool isMoveLock = false;
	public bool lockMove
	{
		set {isMoveLock = value;}
	}

	private bool isRotationLock = false;
	public bool lockRotation
	{
		set {isRotationLock = value;}
	}

	private float totalTimeOnGround;
	private const float LOCK_ROTATION_INTERVAL = 0.75f;

	private CubeObject climbTarget;
	private float collisionLastTime;
	private float collisionTotalTime;
	private const float CLIMB_DECISION_INTERVAL_SEC = 0.1f;
	private const float CLIMB_DECISION_TOTAL_SEC = 0.25f;
	private const float CLIMB_OFFSET_Y = 0.15f;

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

		if (controller.isGrounded)
		{
			totalTimeOnGround += Time.deltaTime;

			if (totalTimeOnGround > LOCK_ROTATION_INTERVAL)
			{
				lockRotation = false;
			}
		}
		else
		{
			totalTimeOnGround = 0f;
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
		if (!isRotationLock)
		{
			transform.LookAt(transform.position + direction);
		}

		Vector3 forward = Vector3.zero;
		if (direction != Vector3.zero)
		{
			forward = transform.TransformDirection(Vector3.forward);
			forward *= 5.0f;
		}

		controller.SimpleMove(forward);
	}

#region ClimbCube
	private bool IsClimbableCube(CubeObject cube)
	{
		return cube != null && cube.isClimbable && transform.position.y < cube.TopPositionY();
	}

	private void Climb(CubeObject cube)
	{
		if (climbTarget == null || climbTarget != cube)
		{
			climbTarget = cube;
			collisionTotalTime = 0f;
			collisionLastTime = Time.time;
		}

		float collisionInterval = Time.time - collisionLastTime;
		collisionLastTime = Time.time;

		if (collisionInterval > CLIMB_DECISION_INTERVAL_SEC)
		{
			collisionTotalTime = 0f;
		}
		else
		{
			collisionTotalTime += collisionInterval;
		}

		if (collisionTotalTime > CLIMB_DECISION_TOTAL_SEC)
		{
			ClimbTween(cube.TopPosition());
		}
	}

	private void ClimbTween(Vector3 position)
	{
		lockRotation = true;

		transform.LookAt(new Vector3(position.x, transform.position.y, position.z));

		controller.Move(Vector3.up * 1.1f);
	}
#endregion

#region Event
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		CubeObject cube = hit.collider.transform.parent.GetComponent<CubeObject>();

		if (IsClimbableCube(cube))
		{
			Climb(cube);
		}
	}
#endregion
}
