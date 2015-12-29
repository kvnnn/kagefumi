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

		controller.lockMove = false;
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

	public void GetOutFromObject(Vector3 position)
	{
		transform.position = new Vector3(position.x, position.y, position.z);
		SetActive(true);
	}

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
