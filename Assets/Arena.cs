using UnityEngine;
using System.Collections;

public class Arena : MonoBehaviour 
{
	public int arenaNumber;
	public float spawnRate;

	// awake is reserved for initalization of accessors
	void Awake ()
	{
		Game.AddArena(this, arenaNumber);
	}

	// start contains initalization of things that are access dependent
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
