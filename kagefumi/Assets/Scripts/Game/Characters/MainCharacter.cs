using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using System.Collections.Generic;

public class MainCharacter : GameMonoBehaviour
{
	private bool hasKey = false;

	public bool isActive
	{
		get {return gameObject.activeSelf;}
	}

	public bool isDive {get; private set;}
	private bool isTween;

	private MainCharacterController controller
	{
		get {return GetComponent<MainCharacterController>();}
	}

	private System.Action onKeyGet;
	private System.Action onClear;

	public void Init(System.Action<Vector3> onUpdate, System.Action onKeyGet, System.Action onClear)
	{
		this.onKeyGet = onKeyGet;
		this.onClear = onClear;

		controller.onUpdate = onUpdate;
	}

	public void Reset()
	{
		hasKey = false;
		isDive = false;

		controller.lockMove = false;
		controller.allowRotation = true;
	}

	public void SetActive(bool active)
	{
		gameObject.SetActive(active);
	}

	public bool isDead
	{
		get {return gameObject == null;}
	}

	private void GetKey(KeyTrigger key)
	{
		key.Get();
		hasKey = true;

		onKeyGet();
	}

	private void OpenDoor()
	{
		if (hasKey)
		{
			controller.lockMove = true;
			onClear();
		}
	}

#region DiveInOut
	public bool Dive()
	{
		if (isTween) {return false;}
		isTween = true;

		Vector3 position = new Vector3(transform.position.x, -1f, transform.position.z);
		controller.TweenMove(position, OnDiveComplete);

		controller.enabled = false;

		return true;
	}

	private void OnDiveComplete()
	{
		isTween = false;
		isDive = true;
		SetActive(false);
	}

	public bool GetOut(Vector3 position)
	{
		if (isTween) {return false;}
		isTween = true;

		SetActive(true);
		transform.position = new Vector3(position.x, -1f, position.z);
		controller.TweenMove(position, OnGetOutComplete);

		return true;
	}

	private void OnGetOutComplete()
	{
		isTween = false;
		isDive = false;

		controller.enabled = true;
	}
#endregion

#region Event
	private void OnTriggerEnter(Collider collider)
	{
		GameObject triggerGameObject = collider.gameObject;
		BaseTrigger trigger = triggerGameObject.GetComponent<BaseTrigger>();

		if (trigger is KeyTrigger)
		{
			GetKey(trigger as KeyTrigger);
		}
		else if (trigger is DoorTrigger)
		{
			OpenDoor();
		}
	}
#endregion
}
