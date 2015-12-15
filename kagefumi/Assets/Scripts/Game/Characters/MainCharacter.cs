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

	public void Init(System.Action<Vector3> onUpdate)
	{
		GetComponent<MainCharacterController>().onUpdate = onUpdate;
	}

	public void Reset()
	{
		hasKey = false;
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
	}

	private void OpenDoor()
	{
		if (hasKey)
		{
			Debug.Log("clear");
		}
		else
		{
			Debug.Log("no key");
		}
	}

	public void GetOutFromObject(Vector2 shadowCentroid)
	{
		transform.position = new Vector3(shadowCentroid.x, transform.position.y, shadowCentroid.y);
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
