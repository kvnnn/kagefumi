using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TapDetector : BaseUIParts, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	public System.Action onDoubleTap;
	public System.Action<PointerEventData> onUp;
	public System.Action<PointerEventData> onDrag;

	public void Init(Vector3 scale)
	{
		(transform as RectTransform).sizeDelta = scale;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (eventData.clickCount == 2)
		{
			if (onDoubleTap != null)
			{
				onDoubleTap();
			}
			eventData.clickCount = 0;
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (onUp != null)
		{
			onUp(eventData);
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (onDrag != null)
		{
			onDrag(eventData);
		}
	}
}
