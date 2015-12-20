using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class BaseObject : GameMonoBehaviour
{
	public List<Vector2> shadowPointList {get; private set;}
	public Vector2 shadowCenterPoint {get; private set;}

	private MainCharacterController mainCharacterController
	{
		get {return GetComponent<MainCharacterController>();}
	}

	private const float FADE_SPEED = 0.5f;

	protected virtual void Awake()
	{
		shadowPointList = new List<Vector2>();
	}

	public void SetLayer(string layerName)
	{
		gameObject.layer = LayerMask.NameToLayer(layerName);
	}

#region Action Event
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

	public virtual Vector3 GetOutPosition()
	{
		return new Vector3(shadowCenterPoint.x, 0f, shadowCenterPoint.y);
	}

	protected virtual void OnGetOut() {}
#endregion

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
	public void SetShadowPointList(List<Vector2> shadowPointList)
	{
		this.shadowPointList = shadowPointList;
	}

	public void SetShadowCenterPoint(Vector2 shadowCenterPoint)
	{
		this.shadowCenterPoint = shadowCenterPoint;
	}
#endregion
}
