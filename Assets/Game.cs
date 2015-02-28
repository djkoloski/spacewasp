using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Game {

	// arena constants 
	public static int screenHeight = 768;
	public static int screenWidth = 1024;
	public static int arenaHeight = 300;
	public static int arenaWidth = 300;
	public static int arenaBorderWidth = 30;

	// arena reference
	public static GameObject arena;

	// player info 
	public static PlayerInfo[] player; 

	// enemy list
	public static List<GameObject> enemy;

	public class PlayerInfo 
	{
		public int killCount;
		public int powerUp;//PowerUp
		public GameObject gameObject;
		public PlayerInfo(GameObject _gameObject)
		{
			gameObject  = _gameObject;
		}
	}
}
