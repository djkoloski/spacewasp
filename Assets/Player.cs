using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	private new Transform transform;
	private Rigidbody2D rb;

	public string input_name;
	public Player opponent;
	public Vector2 move_speed;

	public Vector2 left_stick;
	public Vector2 right_stick;

	public void Awake()
	{
		transform = GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D>();
	}

	public void Start()
	{

	}

	public void Update()
	{
		left_stick = new Vector2(Input.GetAxis(input_name + "_lx"), Input.GetAxis(input_name + "_ly"));
		right_stick = new Vector2(Input.GetAxis(input_name + "_rx"), Input.GetAxis(input_name + "_ry"));
	}

	public void FixedUpdate()
	{
		rb.velocity = Vector2.Scale(left_stick, move_speed);
	}
}