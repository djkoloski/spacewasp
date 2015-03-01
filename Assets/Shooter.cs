using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shooter : MonoBehaviour
{
	private SpriteRenderer sr;

	public float radius;
	public float converge_speed;
	
	private float curAngle;
	private float toAngle;

	public void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
		curAngle = 0;
		toAngle = 0;
	}

	public void Start()
	{

	}

	public void Update()
	{
		curAngle %= 2.0f * Mathf.PI;
		toAngle %= 2.0f * Mathf.PI;

		float nearest_angle = (toAngle < curAngle ? toAngle + 2.0f * Mathf.PI : toAngle - 2.0f * Mathf.PI);
		if (Mathf.Abs(curAngle - toAngle) < Mathf.Abs(curAngle - nearest_angle))
			nearest_angle = toAngle;

		curAngle = Mathf.Lerp(curAngle, nearest_angle, Time.deltaTime * converge_speed);
		transform.localPosition = new Vector3(Mathf.Cos(curAngle), Mathf.Sin(curAngle), 0.0f) * radius;

		if ((curAngle > 0 && curAngle < Mathf.PI) || curAngle < -Mathf.PI)
			sr.sortingOrder = -2;
		else
			sr.sortingOrder = 0;
	}

	public void SetAim(Vector2 direction)
	{
		toAngle = Mathf.Atan2(direction.y, direction.x);
	}
}