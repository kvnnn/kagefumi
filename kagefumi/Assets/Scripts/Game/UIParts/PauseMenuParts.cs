using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseMenuParts : BaseUIParts
{
	private ButtonParts pauseButtonParts
	{
		get {return transform.parent.GetComponentInChildren<PauseMenuButtonParts>();}
	}

	public System.Action onPause;
	public System.Action onResume;
	public System.Action onHomeButtonClick;
	public System.Action onRestartButtonClick;

	private void Show()
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
		pauseButtonParts.isEnabled = false;

		Show();
	}

	private void Resume()
	{
		onResume();
		pauseButtonParts.isEnabled = true;

		Hide();
	}

#region Event
	public void OnPauseButtonClick()
	{
		Pause();
	}

	public void OnResumeButtonClick()
	{
		Resume();
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
#endregion
}
