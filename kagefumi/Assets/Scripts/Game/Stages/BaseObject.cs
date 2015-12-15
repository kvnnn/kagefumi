using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class BaseObject : GameMonoBehaviour
{
	public List<Vector2> shadowPointList {get; private set;}

	private LTDescr alphaTween;

	private MainCharacterController mainCharacterController
	{
		get {return GetComponent<MainCharacterController>();}
	}

	private const float FADE_SPEED = 0.5f;

	protected virtual void Awake()
	{
		shadowPointList = new List<Vector2>();
	}

	public void Dive()
	{
		if (mainCharacterController == null)
		{
			StopBlink();
			gameObject.AddComponent<MainCharacterController>();
		}
	}

	protected virtual void OnDive() {}

	public void GetOut()
	{
		if (mainCharacterController)
		{
			Destroy(mainCharacterController);
		}
	}

	protected virtual void OnGetOut() {}

#region Tween
	public void StartBlink()
	{
		OnBlinkStart();
		LeanTween.cancel(gameObject);
		LeanTween.alpha(gameObject, 0.5f, FADE_SPEED).setLoopPingPong().setRepeat(-1);
	}

	protected virtual void OnBlinkStart() {}

	public void StopBlink()
	{
		OnBlinkStop();
		LeanTween.cancel(gameObject);
		LeanTween.alpha(gameObject, 1f, FADE_SPEED);
	}

	protected virtual void OnBlinkStop() {}
#endregion

#region Shadow
	public void UpdateShadowBounds(Vector3 lightPosition, float lightRange)
	{
		shadowPointList.Clear();

		List<Vector3> shadowVerts = GetShadowVerts(lightPosition, lightRange);
		CalculateShadowPointList(shadowVerts, lightPosition, lightRange);
	}

	private List<Vector3> GetShadowVerts(Vector3 lightPosition, float lightRange)
	{
		Mesh mesh = gameObject.GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = mesh.vertices;
		List<Vector3> shadowVerts = new List<Vector3>();
		foreach (Vector3 vertex in vertices)
		{
			shadowVerts.Add(transform.TransformPoint(vertex));
		}

		foreach (Vector3 vertex in vertices)
		{
			Ray ray = new Ray(lightPosition, (vertex - lightPosition));
			RaycastHit hit;

			if(Physics.Raycast(ray, out hit, lightRange))
			{
				if(hit.collider == gameObject.GetComponent<Collider>())
				{
					shadowVerts.Remove(vertex);
				}
			}
		}

		return shadowVerts;
	}

	private void CalculateShadowPointList(List<Vector3> shadowVerts, Vector3 lightPosition, float lightRange)
	{
		foreach (Vector3 vertex in shadowVerts)
		{
			Ray ray = new Ray(lightPosition, (vertex - lightPosition));
			RaycastHit hit;
			LayerMask layermask = 1<<LayerMask.NameToLayer("StageObject");

			if(Physics.Raycast(ray, out hit, lightRange, layermask))
			{
				if (hit.transform != transform && hit.transform.GetComponent<BaseObject>() != null)
				{
					continue;
				}

#if UNITY_EDITOR
				Debug.DrawLine(ray.origin, hit.point, Color.blue);
#endif

				Vector2 point = new Vector2(hit.point.x, hit.point.z);
				if(!shadowPointList.Contains(point))
				{
					shadowPointList.Add(point);
				}
			}
		}
	}
#endregion
}
