using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClearMenuParts : BaseUIParts
{
	[SerializeField]
	private ButtonParts nextButton;

	public System.Action onPause;
	public System.Action onResume;
	public System.Action onHomeButtonClick;
	public System.Action onRestartButtonClick;
	public System.Action onNextButtonClick;

	private void Show(bool isLastStage)
	{
		if (isLastStage)
		{
			nextButton.isEnabled = false;
		}
		else
		{
			nextButton.isEnabled = true;
		}

		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}

	public void Pause(bool isLastStage)
	{
		onPause();
		Show(isLastStage);
	}

	private void Resume()
	{
		onResume();
		Hide();
	}

#region Event
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
