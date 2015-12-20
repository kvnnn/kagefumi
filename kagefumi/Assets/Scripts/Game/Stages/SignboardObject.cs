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

	private void Start()
	{
		ShowText();
	}

	public void SetText(string text)
	{
		textMesh.text = text;
	}

	private void ShowText()
	{
		Quaternion rotation = Quaternion.LookRotation(Camera.main.transform.position - textMesh.transform.position);
		rotation.eulerAngles = new Vector3(0f, rotation.y, rotation.z);
		textMesh.transform.rotation = rotation;

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

	protected override void OnGetOut()
	{
		ShowText();
	}

	protected override void OnBlinkStart()
	{
		// ShowText();
	}

	protected override void OnBlinkStop()
	{
		// HideText();
	}
#endregion
}
