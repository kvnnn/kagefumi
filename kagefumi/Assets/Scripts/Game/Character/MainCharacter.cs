using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using System.Collections.Generic;

public class MainCharacter : GameMonoBehaviour
{
	public bool isActive
	{
		get {return gameObject.activeSelf;}
	}

	public void Init(System.Action<Vector3> onUpdate)
	{
		GetComponent<MainCharacterController>().onUpdate = onUpdate;
	}

	public void SetActive(bool active)
	{
		gameObject.SetActive(active);
	}

	public bool isDead
	{
		get {return gameObject == null;}
	}

#region Event
	private void OnTriggerEnter(Collider collider)
	{
		GameObject triggerGameObject = collider.gameObject;
		Key key = triggerGameObject.GetComponent<Key>();
		if (key != null)
		{
			key.Get();
		}
	}
#endregion
}
