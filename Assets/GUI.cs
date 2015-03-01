using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GUI : MonoBehaviour
{
	public Player player;
	public Sprite sprite;
	public Vector2 offset;
	public Vector2 spacing;
	public bool flipped;

	private GameObject[] healthSprites;

	public void Awake()
	{

	}

	public void Start()
	{
		GenerateHealth();
		UpdateHealth();
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

			hs.name = "Health";

			hs.transform.SetParent(transform);
			hs.transform.localScale = Vector3.one;

			if (!flipped)
			{
				rt.anchorMin = new Vector2(0, 1);
				rt.anchorMax = new Vector2(0, 1);
				rt.pivot = new Vector2(0, 1);
			}
			else
			{
				rt.anchorMin = new Vector2(1, 1);
				rt.anchorMax = new Vector2(1, 1);
				rt.pivot = new Vector2(1, 1);
			}
			rt.anchoredPosition = new Vector2(spacing.x * i + offset.x, spacing.y * i + offset.y);
			rt.sizeDelta = new Vector2(sprite.texture.width, sprite.texture.height);

			im.sprite = sprite;
		}
	}

	public void UpdateHealth()
	{
		for (int i = 0; i < healthSprites.Length; ++i)
		{
			if (i < player.health)
				healthSprites[i].SetActive(true);
			else
				healthSprites[i].SetActive(false);
		}
	}

	public void Update()
	{
		UpdateHealth();
	}
}