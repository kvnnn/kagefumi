using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseObject : GameMonoBehaviour
{
	public List<Vector2> shadowPointList {get; private set;}

	private void Awake()
	{
		shadowPointList = new List<Vector2>();
	}

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
				Debug.DrawRay(ray.origin, ray.direction * lightRange, Color.red);
#endif

				Vector2 point = new Vector2(hit.point.x, hit.point.z);
				if(!shadowPointList.Contains(point))
				{
					shadowPointList.Add(point);
				}
			}
		}
	}
}
