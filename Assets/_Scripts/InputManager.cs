using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
	public Action<GameObject> OnMouseClick;

	[SerializeField] private LayerMask groundMask;

	private Vector2 _cameraMovementVector;
	public Vector2 CameraMovementVector
	{
		get { return _cameraMovementVector; }
	}

	private void Update()
	{
		//CheckClickDownEvent();
	}

	private GameObject RaycastIsGround()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(new Vector2(ray.origin.x, ray.origin.y), Vector2.zero, Mathf.Infinity, groundMask);
		if (hit.collider != null)
		{
			return hit.collider.gameObject;
		}
		return null;
	}

	private void CheckClickDownEvent()
	{
		if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
		{
			var obj = RaycastIsGround();
			if (obj != null) OnMouseClick.Invoke(obj);
		}
	}
}
