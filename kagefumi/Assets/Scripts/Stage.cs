using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stage
{
	private const string STAGE_JSON_PATH = "Stages/";

	private static int maxStageId_ = 0;
	public static int maxStageId
	{
		get
		{
			if (maxStageId_ == 0)
			{
				foreach (Object obj in Resources.LoadAll(STAGE_JSON_PATH))
				{
					int id = System.Int32.Parse(obj.name);
					if (id > maxStageId_)
					{
						maxStageId_ = id;
					}
				}
			}

			return maxStageId_;
		}
	}
}
