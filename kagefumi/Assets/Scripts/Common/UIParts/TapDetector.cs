using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TapDetector : BaseUIParts, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	private float lastTimeClick;
	private int clickCount = 1;

	public System.Action onDoubleTap;
	public System.Action<PointerEventData> onUp;
	public System.Action<PointerEventData> onDrag;

	public void Init(Vector3 scale)
	{
		(transform as RectTransform).sizeDelta = scale;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		float currentTimeClick = Time.timeSinceLevelLoad;

		if (Mathf.Abs(currentTimeClick - lastTimeClick) < 0.4f)
		{
			clickCount++;
		}
		else
		{
			clickCount = 1;
		}

		if (clickCount == 2)
		{
			onDoubleTap();
		}

		lastTimeClick = currentTimeClick;
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
