using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TapDetector : BaseUIParts, IPointerDownHandler
{
	public System.Action onDoubleTap;

	public void OnPointerDown(PointerEventData eventData)
	{
		if(eventData.clickCount == 2)
		{
			onDoubleTap();
			eventData.clickCount = 0;
		}
	}
}
