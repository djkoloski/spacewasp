using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Game 
{
	// arena constants 
	public static int screenHeight = 768;
	public static int screenWidth = 1024;
	public static int arenaHeight = 300;
	public static int arenaWidth = 300;
	public static int arenaBorderWidth = 30;

	// arena references
	public static Arena[] arena;
	// when an arena awakes, it is added to the arena array, and returns its index
	public static void AddArena(Arena _arena, int index)
	{
		if(Game.arena == null)
			Game.arena = new Arena[2]; 
			
		Game.arena[index] = _arena;
	}

	// player info 
	public static Player[] player;
	// when an arena awakes, it is added to the player array, index is from the players arena
	public static void AddPlayer(Player _player, int index)
	{
		if(Game.player == null)
			Game.player = new Player[2];
			
		Game.player[index] = _player;
	}

	// enemy list
	public static List<GameObject> enemy;

}
