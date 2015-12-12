using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseMenuParts : BaseUIParts
{
	[SerializeField]
	private GameObject pauseMenuBaseGameObject;

	[SerializeField]
	private ButtonParts pauseButtonParts;

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
		Time.timeScale = 0;
		pauseButtonParts.isEnabled = false;

		Show();
	}

	private void Resume()
	{
		Time.timeScale = 1;
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

	}

	public void OnRestartButtonClick()
	{

	}
#endregion
}
