using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	private new Transform transform;
	private Rigidbody2D rb;

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
		Game.AddPlayer(this, playerNumber);
	}

	public void Start()
	{
		knockedOut = false;
	}

	public void Update()
	{
		left_stick = new Vector2(Input.GetAxis(input_name + "_lx"), Input.GetAxis(input_name + "_ly"));
		right_stick = new Vector2(Input.GetAxis(input_name + "_rx"), Input.GetAxis(input_name + "_ry"));

		opponent.shooter.SetAim(right_stick);
	}

	public void FixedUpdate()
	{
		rb.velocity = move_speed * left_stick.normalized;
	}
}