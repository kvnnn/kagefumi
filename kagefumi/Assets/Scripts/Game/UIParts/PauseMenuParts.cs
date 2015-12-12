using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseMenuParts : BaseUIParts
{
	[SerializeField]
	private GameObject pauseMenuBaseGameObject;

	[SerializeField]
	private ButtonParts pauseButtonParts;

	public System.Action onPause;
	public System.Action onResume;
	public System.Action onHomeButtonClick;
	public System.Action onRestartButtonClick;

	public void Show()
	{
		pauseMenuBaseGameObject.SetActive(true);
	}

	public void Hide()
	{
		pauseMenuBaseGameObject.SetActive(false);
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
