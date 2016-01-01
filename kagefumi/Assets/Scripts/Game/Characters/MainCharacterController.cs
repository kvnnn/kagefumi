using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using System.Collections.Generic;

public class MainCharacterController : GameMonoBehaviour
{
	private const float MOVE_SPEED = 5.0f;

	private bool isMoveLock = false;
	public bool lockMove
	{
		set {isMoveLock = value;}
	}

	public bool allowRotation = false;
	private Vector3 lockRotation;

	private float totalTimeOnGround;
	private const float LOCK_ROTATION_INTERVAL = 0.2f;

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
				lockRotation = Vector3.zero;
			}
		}

		if (controller != null)
		{
			if (allowRotation)
			{
				MoveAndRotateByCharacterController(direction);
			}
			else
			{
				MoveByCharacterController(direction);
			}
		}

		if (onUpdate != null)
		{
			onUpdate(transform.position);
		}
	}

	private void MoveAndRotateByCharacterController(Vector3 direction)
	{
		transform.LookAt(transform.position + (lockRotation == Vector3.zero ? direction : lockRotation));

		Vector3 forward = Vector3.zero;
		if (direction != Vector3.zero)
		{
			forward = transform.TransformDirection(Vector3.forward);
			forward *= MOVE_SPEED;
		}

		controller.SimpleMove(forward);
	}

	private void MoveByCharacterController(Vector3 direction)
	{
		direction *= MOVE_SPEED * Time.deltaTime;
		direction.y = direction.y - 9.8f * Time.deltaTime;

		controller.Move(direction);
	}

#region ClimbCube
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
			Jump(cube.TopPosition());
		}
	}

	private void Jump(Vector3 position)
	{
		position = new Vector3(position.x, transform.position.y, position.z) - transform.position;
		lockRotation = position.normalized;

		controller.Move(Vector3.up * 1.1f);
		totalTimeOnGround = 0f;
	}
#endregion

#region Tween
	public void TweenMove(Vector3 position, System.Action onComplete)
	{
		LeanTween.move(gameObject, position, 0.5f).setOnComplete(onComplete);
	}
#endregion

#region Event
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		CubeObject cube = hit.collider.transform.parent.GetComponent<CubeObject>();

		if (cube != null && cube.IsClimbable(transform.position.y))
		{
			Climb(cube);
		}
	}
#endregion
}
