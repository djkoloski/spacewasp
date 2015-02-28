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
			setTarget( getArenaIndexFromPosition() );
	}
	
	void Update () 
	{
		if(target == null)
			setTarget( getArenaIndexFromPosition() );
		
		if(!target.knockedOut)
			 persueTarget();
	}
	
	public void setTarget(int arenaIndex)
	{
		target = Game.player[arenaIndex];
		arena = Game.arena[arenaIndex];
	}
	
	public int getArenaIndexFromPosition()
	{
		if(transform.position.x < 0)
			return 0;
		else
			return 1;
	}
	
	public void persueTarget()
	{
		rb.AddForce( speed * (target.transform.position - transform.position).normalized );
	}
}
