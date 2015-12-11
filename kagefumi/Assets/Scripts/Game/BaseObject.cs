using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class BaseObject : GameMonoBehaviour
{
	public List<Vector2> shadowPointList {get; private set;}

	private LTDescr alphaTween;

	private MainCharacter mainCharacter
	{
		get {return GetComponent<MainCharacter>();}
	}

	private const float FADE_SPEED = 0.5f;
	private readonly Vector3 ALPHA_FADE_OUT = new Vector3(0.2f, 0f, 0f);
	private readonly Vector3 ALPHA_FADE_IN = new Vector3(1f, 0f, 0f);

	private void Awake()
	{
		shadowPointList = new List<Vector2>();
	}

	public void Dive()
	{
		if (mainCharacter == null)
		{
			StopBlink();
			gameObject.AddComponent<MainCharacter>();
			SetShadow(false);
		}
	}

	public void GetOut()
	{
		if (mainCharacter)
		{
			Destroy(mainCharacter);
			SetShadow(true);
		}
	}

	private void SetShadow(bool hasShadow)
	{
		Renderer renderer = GetComponent<Renderer>();
		renderer.receiveShadows = hasShadow;
		renderer.shadowCastingMode = hasShadow ? ShadowCastingMode.On : ShadowCastingMode.Off;
	}

#region Tween
	public void StartBlink()
	{
		LeanTween.cancel(gameObject);
		LeanTween.alpha(gameObject, 0.5f, FADE_SPEED).setLoopPingPong().setRepeat(-1);
	}

	public void StopBlink()
	{
		LeanTween.cancel(gameObject);
		LeanTween.alpha(gameObject, 1f, FADE_SPEED);
	}
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
			LayerMask layermask = 1<<LayerMask.NameToLayer("Room");

			if(Physics.Raycast(ray, out hit, lightRange, layermask))
			{

#if UNITY_EDITOR
				// Debug.DrawRay(ray.origin, ray.direction * lightRange, Color.red);
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
