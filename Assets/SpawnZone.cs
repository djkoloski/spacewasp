using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnZone : MonoBehaviour
{
	public float spawnPause;
	public float spawnInterval;
	public bool enabled;
	public Arena arena;

	private float spawnTime;

	public void Awake()
	{
		spawnTime = spawnPause;
	}

	public void Update()
	{
		if (!enabled)
			return;

		spawnTime -= Time.deltaTime;
		if (spawnTime <= 0)
		{
			spawnTime = spawnInterval;
			arena.spawnEnemy(transform.position, false);
		}
	}
}