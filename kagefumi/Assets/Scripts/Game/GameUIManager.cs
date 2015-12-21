using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using System.Collections.Generic;

public class GameUIManager : GameMonoBehaviour
{
	[SerializeField]
	private List<GameObject> uiPartsPrefabs;

	public bool isPause {get; private set;}
	private System.Action onHomeButtonClick;

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

	private PauseMenuParts pauseMenuParts_;
	private PauseMenuParts pauseMenuParts
	{
		get
		{
			if (pauseMenuParts_ == null)
			{
				pauseMenuParts_ = GetComponentInChildren<PauseMenuParts>();
			}
			return pauseMenuParts_;
		}
	}

	private ClearMenuParts clearMenuParts_;
	private ClearMenuParts clearMenuParts
	{
		get
		{
			if (clearMenuParts_ == null)
			{
				clearMenuParts_ = GetComponentInChildren<ClearMenuParts>();
			}
			return clearMenuParts_;
		}
	}

#region Init
	public void Init(System.Action onDoubleTap, System.Action onHomeButtonClick, System.Action onRestartButtonClick, System.Action onNextButtonClick)
	{
		InitializeUIParts();

		InitTapDetector(onDoubleTap);
		InitJoystick();
		InitPauseMenu(onHomeButtonClick, onRestartButtonClick);
		InitClearMenu(onHomeButtonClick, onRestartButtonClick, onNextButtonClick);
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

	private void InitTapDetector(System.Action onDoubleTap)
	{
		tapDetector.Init((transform as RectTransform).sizeDelta);
		tapDetector.onDoubleTap = onDoubleTap;
		tapDetector.onUp = OnUp;
		tapDetector.onDrag = OnDrag;
	}

	private void InitJoystick()
	{
		HideJoystick();
		if (!CrossPlatformInputManager.AxisExists("Horizontal") && !CrossPlatformInputManager.AxisExists("Vertical"))
		{
			joystick.CreateVirtualAxes();
		}
	}

	private void InitPauseMenu(System.Action onHomeButtonClick, System.Action onRestartButtonClick)
	{
		isPause = false;
		pauseMenuParts.onPause = OnPause;
		pauseMenuParts.onResume = OnResume;
		pauseMenuParts.onHomeButtonClick = onHomeButtonClick;
		pauseMenuParts.onRestartButtonClick = onRestartButtonClick;
	}

	private void InitClearMenu(System.Action onHomeButtonClick, System.Action onRestartButtonClick, System.Action onNextButtonClick)
	{
		isPause = false;
		clearMenuParts.onPause = OnPause;
		clearMenuParts.onResume = OnResume;
		clearMenuParts.onHomeButtonClick = onHomeButtonClick;
		clearMenuParts.onRestartButtonClick = onRestartButtonClick;
		clearMenuParts.onNextButtonClick = onNextButtonClick;

		clearMenuParts.Hide();
	}
#endregion

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
		if (isPause) {return;}
		ShowJoystick(eventData.position);
		joystick.OnDrag(eventData);
	}

	private void OnUp(PointerEventData eventData)
	{
		HideJoystick();
		joystick.OnPointerUp(eventData);
	}

	private void OnPause()
	{
		Time.timeScale = 0;
		isPause = true;
	}

	private void OnResume()
	{
		Time.timeScale = 1;
		isPause = false;
	}

	public void OnClear(bool isLastStage)
	{
		clearMenuParts.Pause(isLastStage);
	}
#endregion
}
