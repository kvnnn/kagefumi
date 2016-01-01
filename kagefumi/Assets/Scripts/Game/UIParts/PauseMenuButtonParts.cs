using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseMenuButtonParts : ButtonParts
{
	public PauseMenuParts pauseMenuParts;

	public override void ButtonClick()
	{
		pauseMenuParts.OnPauseButtonClick();
	}
}
