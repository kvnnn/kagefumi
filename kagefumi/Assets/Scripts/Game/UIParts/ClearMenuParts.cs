using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClearMenuParts : BaseUIParts
{
	public System.Action onPause;
	public System.Action onResume;
	public System.Action onHomeButtonClick;
	public System.Action onRestartButtonClick;
	public System.Action onNextButtonClick;

	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}

	private void Pause()
	{
		onPause();
		Show();
	}

	private void Resume()
	{
		onResume();
		Hide();
	}

#region Event
	public void OnPauseButtonClick()
	{
		Pause();
	}

	public void OnHomeButtonClick()
	{
		Resume();
		onHomeButtonClick();
	}

	public void OnRestartButtonClick()
	{
		Resume();
		onRestartButtonClick();
	}

	public void OnNextButtonClick()
	{
		Resume();
		onNextButtonClick();
	}
#endregion
}
