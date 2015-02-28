using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	private new Transform transform;
	private Rigidbody2D rb;
	private Animator animator;

	public string input_name;
	public Player opponent;
	public Shooter shooter;
	public float move_speed;

	public Vector2 left_stick;
	public Vector2 right_stick;

	public int playerNumber;
	public bool knockedOut;

	public void Awake()
	{
		transform = GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		Game.AddPlayer(this, playerNumber);
	}

	public void Start()
	{

	}

	public void Update()
	{
		left_stick = new Vector2(Input.GetAxis(input_name + "_lx"), Input.GetAxis(input_name + "_ly"));
		right_stick = new Vector2(Input.GetAxis(input_name + "_rx"), Input.GetAxis(input_name + "_ry"));

		opponent.shooter.SetAim(right_stick);
	}

	public void FixedUpdate()
	{
		rb.velocity = move_speed * left_stick;

		bool old_walking = animator.GetBool("walking");
		int old_facing = animator.GetInteger("facing");
		bool new_walking = left_stick.magnitude > 0.1f;
		int new_facing = 0;
		if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y))
			new_facing = (rb.velocity.x > 0 ? 0 : 2);
		else
			new_facing = (rb.velocity.y > 0 ? 1 : 3);

		animator.SetBool("walking", new_walking);
		animator.SetInteger("facing", new_facing);
		if (old_walking != new_walking || old_facing != new_facing)
			animator.SetTrigger("transition");
	}
}