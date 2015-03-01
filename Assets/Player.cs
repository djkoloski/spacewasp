using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Powerups:
 * Siege: bomb enemies spawn
 * Grabby Claw: steal opponent's powerup
 * Iron Knee: deal damage on touch
 * Iron E: block spawn locations
 * Rebound: bullets bounce more
 * Fission: bullets split on enemy hit
 */

public class Player : MonoBehaviour
{
	private new Transform transform;
	private Rigidbody2D rb;
	private Animator animator;
	private GameObject sprite;

	public enum Powerup
	{
		None,
		Siege,
		GrabbyClaw,
		IronKnee,
		IronE,
		Rebound,
		Fission
	}

	public int playerNumber;
	public Player opponent;
	public Shooter shooter;
	public float moveSpeed;
	public int maxHealth;
	public int health;
	public Powerup heldPowerup;
	public int heldPowerupLevel;
	public GameObject spawnPoint;
	public float invulnTime;
	public float deathTime;
	public Arena arena;

	public bool knockedOut
	{
		get
		{
			return health <= 0;
		}
	}
	public string inputName
	{
		get
		{
			return "player" + (playerNumber + 1);
		}
	}

	private Vector2 leftStick;
	private Vector2 rightStick;
	private int kills;

	private int siegeLevel;
	private float siegeTime;
	private int ironKneeLevel;
	private float ironKneeTime;
	private int reboundLevel;
	private float reboundTime;
	private int fissionLevel;
	private float fissionTime;

	private float invulnLeft;
	private float deathTimeLeft;

	private bool invulnerable
	{
		get
		{
			return invulnLeft > 0;
		}
	}
	private bool dead
	{
		get
		{
			return health <= 0;
		}
	}

	public void Awake()
	{
		transform = GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		sprite = transform.Find("sprite").gameObject;
		Game.AddPlayer(this, playerNumber);

		siegeLevel = 0;
		siegeTime = 0;
		ironKneeLevel = 0;
		ironKneeTime = 0;
		reboundLevel = 0;
		reboundTime = 0;
		fissionLevel = 0;
		fissionTime = 0;
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

		if (Input.GetKeyDown(KeyCode.Space))
			Damage(1, null);
		else if (Input.GetKeyDown(KeyCode.A))
		{
			heldPowerup = Powerup.Rebound;
			heldPowerupLevel = 2;
			Debug.Log("POWERUP GET");
		}
		else if (Input.GetButtonDown(inputName + "_powerup"))
		{
			UsePowerup();
			Debug.Log("POWERUP USE");
		}
	}

	public void UpdateAnimator()
	{
		if (!dead)
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

			if (invulnerable)
			{
				invulnLeft -= Time.deltaTime;
				int flashPhase = Mathf.FloorToInt(invulnLeft / 0.25f) % 2;
				sprite.SetActive(flashPhase == 0);
			}
			else
				sprite.SetActive(true);
		}
		else
		{
			sprite.SetActive(true);

			deathTimeLeft -= Time.deltaTime;
			if (deathTimeLeft <= 0)
			{
				health = maxHealth;
				heldPowerup = Powerup.None;
				heldPowerupLevel = 0;
				transform.position = spawnPoint.transform.position;
				invulnLeft = invulnTime;
				arena.KillAll();
			}
		}

		if (siegeTime > 0)
			siegeTime -= Time.deltaTime;
		if (ironKneeTime > 0)
			ironKneeTime -= Time.deltaTime;

		if (reboundTime > 0)
			reboundTime -= Time.deltaTime;
		else
			opponent.shooter.bulletBounces = 0;

		if (fissionTime > 0)
			fissionTime -= Time.deltaTime;
		else
			opponent.shooter.bulletSplits = 0;
	}

	public void FixedUpdate()
	{
		if (leftStick.magnitude > 0.1f)
			rb.velocity = moveSpeed * leftStick;
		else
			rb.velocity = Vector2.zero;
	}

	public void UsePowerup()
	{
		switch (heldPowerup)
		{
			case Powerup.None:
				break;
			case Powerup.Siege:
				// alert the arena
				break;
			case Powerup.GrabbyClaw:
				heldPowerup = opponent.heldPowerup;
				opponent.heldPowerup = Powerup.None;
				opponent.heldPowerupLevel = 0;
				break;
			case Powerup.IronE:
				// alert the arena
				break;
			case Powerup.IronKnee:
				ironKneeTime = 5.0f + 1.0f * heldPowerupLevel;
				ironKneeLevel = heldPowerupLevel;

				heldPowerup = Powerup.None;
				heldPowerupLevel = 0;
				break;
			case Powerup.Rebound:
				opponent.shooter.bulletBounces = 1 + heldPowerupLevel;
				reboundTime = 6.0f + 2.5f * heldPowerupLevel;
				reboundLevel = heldPowerupLevel;

				heldPowerup = Powerup.None;
				heldPowerupLevel = 0;
				break;
			case Powerup.Fission:
				opponent.shooter.bulletSplits = heldPowerupLevel;
				fissionTime = 3.5f + 0.75f * heldPowerupLevel;
				fissionLevel = heldPowerupLevel;

				heldPowerup = Powerup.None;
				heldPowerupLevel = 0;
				break;
			default:
				break;
		}
	}

	public void Damage(int damage, Enemy enemy)
	{
		if (health <= 0 || invulnLeft > 0)
			return;

		if (enemy != null && ironKneeTime > 0)
		{
			// enemy.Damage(ironKneeLevel, this);
		}

		if (health <= damage)
		{
			health = 0;
			deathTimeLeft = deathTime;
		}
		else
		{
			health -= damage;
			invulnLeft = invulnTime;
		}
	}
}