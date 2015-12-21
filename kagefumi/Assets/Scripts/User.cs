using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class User
{
	public const string STAGE_ID_KEY = "user_stage_id";
	public const int DEFAULT_STAGE_ID = 1;

	private const int DEBUG_STAGE_ID = 10;

	public static int stageId
	{
		get
		{
// #if UNITY_EDITOR
// 			return DEBUG_STAGE_ID;
// #endif
			if (PlayerPrefs.HasKey(STAGE_ID_KEY))
			{
				return PlayerPrefs.GetInt(STAGE_ID_KEY);
			}
			else
			{
				return DEFAULT_STAGE_ID;
			}
		}

		set
		{
			PlayerPrefs.SetInt(STAGE_ID_KEY, value);
			PlayerPrefs.Save();
		}
	}
}
