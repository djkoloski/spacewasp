using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Arena : MonoBehaviour 
{
	private new Transform transform;
	
	public int arenaNumber;
	public GameObject player;
	public GameObject enemyPrefab;
	public SpawnZone[] spawnZones;
	public List<GameObject> enemies;
	public int maxEnemyCount;

	// awake is reserved for initalization of accessors
	void Awake ()
	{
		Game.AddArena(this, arenaNumber);
		transform = GetComponent<Transform>();
	}

	// start contains initalization of things that are access dependent
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	public void KillAll()
	{
		for (int i = 0; i < enemies.Count; ++i)
			GameObject.Destroy(enemies[i]);
		enemies = new List<GameObject>();
	}
	
	public void spawnEnemy(Vector2 location, bool type)
	{
		Enemy newEnemy = ((GameObject) Instantiate(enemyPrefab, (Vector3) location, Quaternion.identity)).GetComponent<Enemy>();
		newEnemy.SetTarget(player, this);
		newEnemy.type = type;
		enemies.Add(newEnemy.gameObject);
	}
}
