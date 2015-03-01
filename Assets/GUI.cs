using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GUI : MonoBehaviour
{
	public Player player;
	public Sprite sprite;

	private GameObject[] healthSprites;

	public void Awake()
	{
		
	}

	public void Start()
	{
		GenerateHealth();
	}

	public void GenerateHealth()
	{
		if (healthSprites != null)
			foreach (GameObject sprite in healthSprites)
				GameObject.Destroy(sprite);

		healthSprites = new GameObject[player.maxHealth];

		for (int i = 0; i < healthSprites.Length; ++i)
		{
			GameObject hs = new GameObject();
			healthSprites[i] = hs;
			RectTransform rt = hs.AddComponent<RectTransform>();
			CanvasRenderer cr = hs.AddComponent<CanvasRenderer>();
			Image im = hs.AddComponent<Image>();

			//rt
		}
	}

	public void Update()
	{
		
	}
}