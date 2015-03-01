using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntegerGUI : MonoBehaviour
{
	private new Transform transform;

	public Sprite full;
	public Sprite empty;
	public int maxValue;
	public int value;
	public Vector2 spacing;

	private GameObject[] indicators;

	public void Awake()
	{
		transform = GetComponent<Transform>();
		Generate();
	}

	public void Generate()
	{
		if (indicators != null)
			for (int i = 0; i < indicators.Length; ++i)
				GameObject.Destroy(indicators[i]);

		indicators = new GameObject[maxValue];
		
		for (int i = 0; i < maxValue; ++i)
		{
			GameObject go = new GameObject();
			go.transform.parent = transform;
			go.transform.localPosition = new Vector3(spacing.x * i, spacing.y * i, 0);
			go.AddComponent<SpriteRenderer>();
			indicators[i] = go;
		}

		Refresh();
	}

	public void Refresh()
	{
		for (int i = 0; i < indicators.Length; ++i)
			indicators[i].GetComponent<SpriteRenderer>().sprite = (i < value ? full : empty);
	}

	public int GetMaxValue()
	{
		return value;
	}

	public void SetMaxValue(int newMaxValue)
	{
		maxValue = newMaxValue;
		Generate();
	}

	public int GetValue()
	{
		return value;
	}

	public void SetValue(int newValue)
	{
		value = newValue;
		Refresh();
	}
}