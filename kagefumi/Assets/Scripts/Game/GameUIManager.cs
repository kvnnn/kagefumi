using UnityEngine;
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

	public void Init(System.Action onDoubleTap)
	{
		tapDetector.onDoubleTap = onDoubleTap;
	}
}
