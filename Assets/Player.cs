using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	private new Transform transform;
	private Rigidbody2D rb;
	private Animator animator;

	public int playerNumber;
	public Player opponent;
	public Shooter shooter;
	public float moveSpeed;
	public int maxHealth;
	public int health;

	public bool knockedOut
	{
		get
		{
			return health <= 0;
		}
	}

	private Vector2 leftStick;
	private Vector2 rightStick;

	public string inputName
	{
		get
		{
			return "player" + (playerNumber + 1);
		}
	}

	public void Awake()
	{
		transform = GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		Game.AddPlayer(this, playerNumber);
	}

	public void Start()
	{
		animator.SetBool("player", playerNumber == 1);
		animator.SetTrigger("transition");
	}

	public void Update()
	{
		leftStick = new Vector2(Input.GetAxis(inputName + "_lx"), Input.GetAxis(inputName + "_ly"));
		rightStick = new Vector2(Input.GetAxis(inputName + "_rx"), Input.GetAxis(inputName + "_ry"));

		opponent.shooter.SetAim(rightStick);
		UpdateAnimator();
	}

	public void UpdateAnimator()
	{
		bool old_walking = animator.GetBool("walking");
		int old_facing = animator.GetInteger("facing");
		bool new_walking = leftStick.magnitude > 0.1f;
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

	public void FixedUpdate()
	{
		if(leftStick.magnitude > 0.1f)
			rb.velocity = moveSpeed * leftStick;
		else
			rb.velocity = Vector2.zero;
	}
}