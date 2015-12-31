using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ClearMenuParts : BaseUIParts
{
	[SerializeField]
	private ButtonParts nextButton;
	[SerializeField]
	private Text text;

	public System.Action onPause;
	public System.Action onResume;
	public System.Action onHomeButtonClick;
	public System.Action onRestartButtonClick;
	public System.Action onNextButtonClick;

	private const string CLEAR_TEXT = "CLEAR";
	private const string GAME_OVER_TEXT = "GAME OVER";

	private void Show(bool isLastStage)
	{
		nextButton.gameObject.SetActive(true);
		if (isLastStage)
		{
			nextButton.isEnabled = false;
		}
		else
		{
			nextButton.isEnabled = true;
		}

		text.text = CLEAR_TEXT;

		gameObject.SetActive(true);
	}

	private void ShowGameOver()
	{
		nextButton.gameObject.SetActive(false);
		text.text = GAME_OVER_TEXT;
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

	public void PauseGameOver()
	{
		onPause();
		ShowGameOver();
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
