using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shooter : MonoBehaviour
{
	private SpriteRenderer sr;

	public GameObject bullet;
	public float radius;
	public float convergeSpeed;
	public float shotSpeed;
	
	private bool shooting;
	private float curAngle;
	private float toAngle;
	private float shotTimeout;

	public void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
		shooting = false;
		curAngle = 0;
		toAngle = 0;
		shotTimeout = 0;
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

		curAngle = Mathf.Lerp(curAngle, nearest_angle, Time.deltaTime * convergeSpeed);
		transform.localPosition = new Vector3(Mathf.Cos(curAngle), Mathf.Sin(curAngle), 0.0f) * radius;

		if ((curAngle > 0 && curAngle < Mathf.PI) || curAngle < -Mathf.PI)
			sr.sortingOrder = -2;
		else
			sr.sortingOrder = 0;

		if (shooting && shotTimeout <= 0.0f)
			Shoot();
		else
			shotTimeout -= Time.deltaTime;
	}

	public void Shoot()
	{
		GameObject go = (GameObject)GameObject.Instantiate(bullet);
		go.transform.position = transform.position + new Vector3(Mathf.Cos(curAngle), Mathf.Sin(curAngle), 0.0f) * 0.2f;
		Bullet b = go.GetComponent<Bullet>();
		b.direction = new Vector2(Mathf.Cos(curAngle), Mathf.Sin(curAngle));

		shotTimeout = shotSpeed;
	}

	public void SetAim(Vector2 direction)
	{
		shooting = direction.magnitude > .7;
		toAngle = Mathf.Atan2(direction.y, direction.x);
	}
}