using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
	private new Transform transform;
	private Rigidbody2D rb;
	private Animator animator;

	public bool color;
	public float speed;
	public Vector2 direction;
	public int bounces;
	public int splits;

	public void Awake()
	{
		transform = GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	public void Start()
	{
		animator.SetBool("color", color);
		animator.SetTrigger("transition");
	}

	public void Update()
	{

	}

	public void FixedUpdate()
	{
		rb.velocity = direction * speed;
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		Vector2 normal = collision.contacts[0].normal;
		direction -= 2.0f * normal * Vector2.Dot(normal, direction);

		GameObject go = collision.gameObject;

		if (go.tag == "Enemy")
		{
			Enemy enemy = go.GetComponent<Enemy>();

		}
		else
		{
			if (bounces == 0)
				GameObject.Destroy(gameObject);
			--bounces;
		}
	}
}