using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
	public int cellX;
	public int cellY;
	public bool HasChiled()
	{
		return transform.childCount > 0;
	}

	public List<Transform> GetChild()
	{
		List<Transform> allChiled = new List<Transform>();
		foreach (Transform chiled in transform)
		{
			allChiled.Add(chiled);
		}
		return allChiled;
	}
}
