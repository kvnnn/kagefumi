using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using System.Collections.Generic;

public class GameUIManager : GameMonoBehaviour
{
	private TapDetector tapDetector_;
	private TapDetector tapDetector
	{
		get
		{
			if (tapDetector_ == null)
			{
				tapDetector_ = GetComponentInChildren<TapDetector>();
			}
			return tapDetector_;
		}
	}

	[SerializeField]
	private Joystick joystick;

	public void Init(System.Action onDoubleTap)
	{
		tapDetector.onDoubleTap = onDoubleTap;
		tapDetector.onUp = OnUp;
		tapDetector.onDrag = OnDrag;

		HideJoystick();
		joystick.CreateVirtualAxes();
	}

	public void ShowJoystick(Vector3 position)
	{
		if (joystick.gameObject.activeSelf) {return;}
		joystick.SetStartPosition(position);
		joystick.gameObject.SetActive(true);
	}

	public void HideJoystick()
	{
		if (!joystick.gameObject.activeSelf) {return;}
		joystick.gameObject.SetActive(false);
	}

#region Event
	private void OnDrag(PointerEventData eventData)
	{
		ShowJoystick(eventData.position);
		joystick.OnDrag(eventData);
	}

	private void OnUp(PointerEventData eventData)
	{
		HideJoystick();
		joystick.OnPointerUp(eventData);
	}
#endregion
}
