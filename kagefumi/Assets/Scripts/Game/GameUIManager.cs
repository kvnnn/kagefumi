using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using System.Collections.Generic;

public class GameUIManager : GameMonoBehaviour
{
	[SerializeField]
	private List<GameObject> uiPartsPrefabs;

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

	private Joystick joystick_;
	private Joystick joystick
	{
		get
		{
			if (joystick_ == null)
			{
				joystick_ = GetComponentInChildren<Joystick>();
			}
			return joystick_;
		}
	}

	public void Init(System.Action onDoubleTap)
	{
		InitializeUIParts();

		tapDetector.onDoubleTap = onDoubleTap;
		tapDetector.onUp = OnUp;
		tapDetector.onDrag = OnDrag;

		HideJoystick();
		joystick.CreateVirtualAxes();
	}

	private void InitializeUIParts()
	{
		if (transform.childCount == 0)
		{
			foreach (GameObject prefab in uiPartsPrefabs)
			{
				RectTransform partsTransfrom = Instantiate(prefab).transform as RectTransform;
				Vector3 position = partsTransfrom.anchoredPosition;
				partsTransfrom.SetParent(transform);
				partsTransfrom.anchoredPosition = position;
			}
		}
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
