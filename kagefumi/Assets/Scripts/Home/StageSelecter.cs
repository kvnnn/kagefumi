using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageSelecter : GameMonoBehaviour
{
	public int id;

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

	public void Init(int id)
	{
		this.id = id;

		SetText();
	}

	private void SetText()
	{
		textMesh.text = id.ToString();
	}

	public void UpdateSelecter(int maxId)
	{
		if (id > maxId)
		{
			gameObject.SetActive(false);
		}
		else
		{
			gameObject.SetActive(true);
		}
	}
}
