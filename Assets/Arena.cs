using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Arena : MonoBehaviour 
{
	private new Transform transform;
	
	public int arenaNumber;
	public float spawnRate;
	private float timeSinceLastSpawn;
	public GameObject player;
	public GameObject enemyPrefab;
	public List<GameObject> enemies;

	// awake is reserved for initalization of accessors
	void Awake ()
	{
		Game.AddArena(this, arenaNumber);
		transform = GetComponent<Transform>();
	}

	// start contains initalization of things that are access dependent
	void Start () 
	{
		timeSinceLastSpawn = 0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeSinceLastSpawn += Time.deltaTime;
		if(timeSinceLastSpawn > spawnRate)
		{
			spawnEnemy((Vector2)transform.position + new Vector2(0, 2));
			timeSinceLastSpawn = 0f;
		}
	}
	
	public void spawnEnemy(Vector2 location)
	{
		Enemy newEnemy = ((GameObject) Instantiate(enemyPrefab, (Vector3) location, Quaternion.identity)).GetComponent<Enemy>();
		newEnemy.SetTarget(player, this);
	}
}
