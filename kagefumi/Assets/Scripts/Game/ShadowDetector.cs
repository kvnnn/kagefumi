using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class ShadowDetector : GameMonoBehaviour
{
	public BaseObject[] objects {get; private set;}

	public void Init(BaseObject[] objects)
	{
		this.objects = objects;
	}

	public void UpdateShadowData()
	{
		if (objects == null) {return;}

		foreach (BaseObject obj in objects)
		{
			UpdateObjectShadowBounds(obj, GetComponent<Light>().range);
		}
	}

	private bool IsObjectInLight(BaseObject obj)
	{
		if (GetComponent<Light>().type != LightType.Spot)
		{
			return true;
		}

		Vector3 targetDir = obj.transform.position - transform.position;
		Vector3 forward = transform.forward;
		float angle = Vector3.Angle(targetDir, forward);

		return angle < GetComponent<Light>().spotAngle/2 && Vector3.Distance(obj.transform.position, transform.position) < GetComponent<Light>().range;
	}

	public BaseObject GetShadowObject(Vector2 position)
	{
		if (objects == null) {return null;}

		foreach (BaseObject obj in objects)
		{
			if (IsOnShadow(position, obj.GetShadowPointList(this)))
			{
				return obj;
			}
		}

		return null;
	}

	private bool IsOnShadow(Vector2 p, List<Vector2> pointList)
	{
		bool isInside = false;
		if (pointList.Count <= 2) {return isInside;}

		int j = pointList.Count - 1;
		for (int i = 0; i < pointList.Count; j = i++)
		{
			if (((pointList[i].y <= p.y && p.y < pointList[j].y) || (pointList[j].y <= p.y && p.y < pointList[i].y)) && (p.x < (pointList[j].x - pointList[i].x) * (p.y - pointList[i].y) / (pointList[j].y - pointList[i].y) + pointList[i].x))
			{
				isInside = !isInside;
			}
		}

		return isInside;
	}

#region ObjectShadow
	public void UpdateObjectShadowBounds(BaseObject baseObject, float lightRange)
	{
		baseObject.ClearPointList(this);

		if (IsObjectInLight(baseObject))
		{
			List<Vector3> shadowVerts = CalculateShadowVerts(baseObject, lightRange);
			CalculateShadowPointList(baseObject, shadowVerts, lightRange);
			CalculateCentroidOfShadowPoint(baseObject);
		}
	}

	private List<Vector3> CalculateShadowVerts(BaseObject baseObject, float lightRange)
	{
		Mesh mesh = baseObject.gameObject.GetComponent<MeshFilter>().sharedMesh;
		Vector3[] vertices = mesh.vertices;
		List<Vector3> shadowVerts = new List<Vector3>();
		foreach (Vector3 vertex in vertices)
		{
			shadowVerts.Add(baseObject.transform.TransformPoint(vertex));
		}

		foreach (Vector3 vertex in vertices)
		{
			Ray ray = new Ray(transform.position, (vertex - transform.position));
			RaycastHit hit;

			if(Physics.Raycast(ray, out hit, lightRange))
			{
				if(hit.collider == baseObject.gameObject.GetComponent<Collider>())
				{
					shadowVerts.Remove(vertex);
				}
			}
		}

		return shadowVerts;
	}

	private void CalculateShadowPointList(BaseObject baseObject, List<Vector3> shadowVerts, float lightRange)
	{
		baseObject.SetLayer("Default");

		List<Vector2> shadowPointList = new List<Vector2>();
		foreach (Vector3 vertex in shadowVerts)
		{
			Ray ray = new Ray(transform.position, (vertex - transform.position));
			RaycastHit hit;
			LayerMask layermask = 1<<LayerMask.NameToLayer("StageObject");

			if(Physics.Raycast(ray, out hit, lightRange, layermask))
			{
				if (hit.transform != baseObject.transform && hit.transform.GetComponent<BaseObject>() != null)
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

		baseObject.SetLayer("StageObject");
		baseObject.SetShadowPointList(this, shadowPointList);
	}

	private void CalculateCentroidOfShadowPoint(BaseObject baseObject)
	{
		List<Vector2> shadowPointList = baseObject.GetShadowPointList(this);
		int pointCount = shadowPointList.Count;
		if (pointCount <= 2) {return;}

		List<Vector2> filteredPoint = new List<Vector2>();
		float distanceFromLight = Vector3.Distance(transform.position, baseObject.transform.position);

		foreach (Vector2 point in shadowPointList)
		{
			if (distanceFromLight > Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z), new Vector3(point.x, 0f, point.y)))
			{
				continue;
			}

			filteredPoint.Add(point);
		}

		pointCount = filteredPoint.Count;
		if (pointCount <= 1) {return;}

		Vector2 centroid = Vector2.zero;
		foreach (Vector2 point in filteredPoint)
		{
			centroid += point / pointCount;
		}

#if UNITY_EDITOR
		Debug.DrawLine(baseObject.transform.position, new Vector3(centroid.x, 0f, centroid.y), Color.red);
#endif

		baseObject.SetShadowCenterPoint(this, centroid);
	}
#endregion
}
