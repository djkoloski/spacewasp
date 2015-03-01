using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour
{
	private new Transform transform;
	
	public GameObject target;
	public AnimationCurve followCurve;
	public float maxRadius;
	public float lookAhead;

	public void Awake()
	{
		transform = GetComponent<Transform>();
	}

	public void FixedUpdate()
	{
		Vector3 poi = target.transform.position + new Vector3(target.GetComponent<Rigidbody2D>().velocity.x, target.GetComponent<Rigidbody2D>().velocity.y, 0.0f) * lookAhead;
		Vector3 diff = poi - transform.position;
		diff.z = 0;
		float dist = diff.magnitude;
		if (dist > maxRadius)
			transform.position += diff.normalized * (dist - maxRadius);
		transform.position += followCurve.Evaluate(Mathf.Clamp01(dist / maxRadius)) * diff.normalized * Time.fixedDeltaTime;
	}
}