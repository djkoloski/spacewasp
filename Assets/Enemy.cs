using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour 
{
	private new Transform transform;
	private Rigidbody2D rb;
	private SpriteRenderer sr;
	private Animator animator;
	
	private int facing;
	private bool attacking;
	
	public Player target;
	public Arena arena;
	
	public float speed;
	public bool type;
	
	public bool buffered;
	public Vector3 bufferedDash;
	
	public GameObject hitbox;
	private GameObject activeHitbox;
	void Awake ()
	{
		transform = GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponentInChildren<SpriteRenderer>();
		animator = GetComponent<Animator>();
		attacking = false;
	}
	
	void Start () 
	{
		if(target == null)
			SetTarget(GetArenaIndexFromPosition() );
		Game.AddEnemy(gameObject, arena.arenaNumber);
		buffered = false;
	}
	
	void Update () 
	{
		if(target == null)
			SetTarget( GetArenaIndexFromPosition() );
		
		if(target.transform.position.y < transform.position.y)
			sr.sortingOrder = -2;
		else
			sr.sortingOrder = 0;
		
		animator.SetBool("type", type);
		
		if(attacking && !type)
			animator.SetInteger("facing", -1);
		if(!target.knockedOut && !attacking)
			PursueTarget();
			 
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
	}
	
	public void TriggerDash()
	{
		rb.velocity = 1.5f * speed * ((target.transform.position - transform.position).normalized) ;
		if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y))
			facing = (rb.velocity.x > 0 ? 0 : 2);
		else
			facing = (rb.velocity.y > 0 ? 1 : 3);
			
		animator.SetInteger("facing", facing);
		animator.SetBool("rest", false);
	}
	
	public void PursueTarget()
	{
		if(((Vector2)(target.transform.position - transform.position)).magnitude > ( type ? 1.5 : .3 ) )
		{
			rb.velocity = speed * ((target.transform.position - transform.position).normalized) ;
			animator.SetBool("walking", true);
		}
		else
		{
			rb.velocity = Vector2.zero;
			Attack();
		}
		if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y))
			facing = (rb.velocity.x > 0 ? 0 : 2);
		else
			facing = (rb.velocity.y > 0 ? 1 : 3);
		
		if(!attacking)
			animator.SetInteger("facing", facing);
	}
	
	public void Attack()
	{
		attacking = true;
		if(!type)
		{
			animator.SetBool("walking", false);
			animator.SetInteger("facing", 1);
		}	
		else 
		{
			animator.SetBool("walking", false);
			animator.SetInteger("facing", 1);
		}
	}
	
	public void ReAwake()
	{
		attacking = false;
		buffered = false;
	}
	
	public void DashStart()
	{
		if(activeHitbox)
			Destroy(activeHitbox);
		activeHitbox = (GameObject)Instantiate(hitbox, transform.position, Quaternion.identity);
		activeHitbox.transform.localScale = new Vector3(.8f, .8f);
		activeHitbox.transform.parent = transform;
		if(buffered)
			rb.velocity = bufferedDash;
	}
	
	public void DashEnd()
	{
		Destroy(activeHitbox);
		buffered = true;
		bufferedDash = rb.velocity;
		rb.velocity = Vector2.zero;
	}
	
	public void RestStart()
	{
		if(buffered)
			rb.velocity = Vector2.zero;
		buffered = false;
	}
	
	public void SlideEnd()
	{
		rb.velocity = Vector2.zero;
	}
	
	public void ExpolsionStart()
	{
		activeHitbox = (GameObject)Instantiate(hitbox, transform.position, Quaternion.identity);
		activeHitbox.transform.localScale = new Vector3(1.2f, 1.2f);
		activeHitbox.transform.parent = transform;
	}
	
	public void ExpolsionEnd()
	{
		Destroy(activeHitbox);
	}
	
	public void End()
	{
		Destroy(gameObject);
	}
	
	public void SetTarget(int arenaIndex)
	{
		target = Game.player[arenaIndex];
		arena = Game.arena[arenaIndex];
	}
	
	public void SetTarget(GameObject _playerObj, Arena _arena)
	{
		target = _playerObj.GetComponent<Player>();
		arena = _arena;
	}
	
	public int GetArenaIndexFromPosition()
	{
		if(transform.position.x < 0)
			return 0;
		else
			return 1;
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("hello");
		if(other.transform.parent == target.transform)
		{
			animator.SetBool("rest", false);
			target.Damage((type ? 1 : 2), this);
			if(type)
			{
				rb.velocity = -3 * speed * ((target.transform.position - transform.position).normalized);
			}
		}
	}
}
