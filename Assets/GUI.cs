using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUI : MonoBehaviour
{
	public Player player;
	public IntegerGUI health;

	public void Awake()
	{

	}

	public void Start()
	{
		health.SetMaxValue(player.maxHealth);
	}

	public void Update()
	{
		health.SetValue(player.health);
	}
}