using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shooter : MonoBehaviour
{
	public float radius;
	public float converge_speed;
	
	private float cur_angle;
	private float to_angle;

	public void Awake()
	{
		cur_angle = 0;
		to_angle = 0;
	}

	public void Start()
	{

	}

	public void Update()
	{
		cur_angle %= 2.0f * Mathf.PI;
		to_angle %= 2.0f * Mathf.PI;

		float nearest_angle = (to_angle < cur_angle ? to_angle + 2.0f * Mathf.PI : to_angle - 2.0f * Mathf.PI);
		if (Mathf.Abs(cur_angle - to_angle) < Mathf.Abs(cur_angle - nearest_angle))
			nearest_angle = to_angle;

		cur_angle = Mathf.Lerp(cur_angle, nearest_angle, Time.deltaTime * converge_speed);
		transform.localPosition = new Vector3(Mathf.Cos(cur_angle), Mathf.Sin(cur_angle), 0.0f) * radius;
	}

	public void SetAim(Vector2 direction)
	{
		to_angle = Mathf.Atan2(direction.y, direction.x);
	}
}