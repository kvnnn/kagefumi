using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageSelecterManager : GameMonoBehaviour
{
	[SerializeField]
	private GameObject stageSelecterPrefab;

	private List<StageSelecter> stages;

	private System.Action<StageSelecter> onSelecterClick;

	private const float X_OFFSET = 2.5f;
	private const float Z_OFFSET = -4f;

	private const int MAX_ROW = 5;

	public void Init(System.Action<StageSelecter> onSelecterClick)
	{
		this.onSelecterClick = onSelecterClick;

		InstantiateStageSelecter();
		UpdateStageSelecters();
	}

	private void Update()
	{
		if (stages == null) {return;}

		if (IsTouch())
		{
			Ray ray = Camera.main.ScreenPointToRay(GetTouchPosition());
			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast(ray, out hit))
			{
				GameObject obj = hit.collider.gameObject;
				StageSelecter selecter = obj.GetComponent<StageSelecter>();
				if (selecter != null)
				{
					onSelecterClick(selecter);
					return;
				}

				selecter = obj.transform.parent.GetComponent<StageSelecter>();
				if (selecter != null)
				{
					onSelecterClick(selecter);
					return;
				}
			}
		}
	}

	private void InstantiateStageSelecter()
	{
		if (stages == null)
		{
			stages = new List<StageSelecter>();

			for (int i = 0; i < Stage.maxStageId; i++)
			{
				Transform selecterTransform = Instantiate(stageSelecterPrefab).transform;
				selecterTransform.name = i.ToString();
				selecterTransform.SetParent(transform);

				// Currently 8 stage is the last stage, so fix the position of the stage select parts
				int coulmn = (int)i/MAX_ROW;

				selecterTransform.position = new Vector3((i%MAX_ROW - 2 + coulmn) * X_OFFSET, selecterTransform.position.y, selecterTransform.position.z + (int)(i/MAX_ROW) * Z_OFFSET );

				StageSelecter stageSelecter = selecterTransform.GetComponent<StageSelecter>();
				stageSelecter.Init(i + 1);

				stages.Add(stageSelecter);
			}
		}

		onSelecterClick(stages[0]);
	}

	private void UpdateStageSelecters()
	{
		int maxId = User.stageId;
		foreach (StageSelecter stage in stages)
		{
			stage.UpdateSelecter(maxId);
		}
	}
}
