using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class SignboardObject : BaseObject
{
	private TextMesh textMesh_;
	private TextMesh textMesh
	{
		get
		{
			if (textMesh_ == null)
			{
				textMesh_ = GetComponentInChildren<TextMesh>();
			}
			return textMesh_;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		HideText();
	}

	public void SetText(string text)
	{
		textMesh.text = text;
	}

	private void ShowText()
	{
		textMesh.gameObject.SetActive(true);
	}

	private void HideText()
	{
		textMesh.gameObject.SetActive(false);
	}

#region Event
	protected override void OnDive()
	{
		HideText();
	}

	protected override void OnBlinkStart()
	{
		ShowText();
	}

	protected override void OnBlinkStop()
	{
		HideText();
	}
#endregion
}
