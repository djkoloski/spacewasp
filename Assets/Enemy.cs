using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	private new Transform transform;
	private Rigidbody2D rb;
	
	public Player target;
	public Arena arena;
	
	public float speed;
	
	
	void Awake ()
	{
		transform = GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D>();
	}
	
	void Start () 
	{
		if(target == null)
			SetTarget(GetArenaIndexFromPosition() );
		Game.AddEnemy(gameObject, arena.arenaNumber);
	}
	
	void Update () 
	{
		if(target == null)
			SetTarget( GetArenaIndexFromPosition() );
		
		Debug.Log(target.playerNumber);
		
		if(!target.knockedOut)
			 PursueTarget();
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
	
	public void PursueTarget()
	{
		if((target.transform.position - transform.position).magnitude > .3)
			rb.velocity = speed * ((target.transform.position - transform.position).normalized) ;
		else
		{
			rb.velocity = Vector2.zero;
			Attack();
		}
	}
	
	public void Attack()
	{
		//Debug.Log ("Attack");
	}
}
