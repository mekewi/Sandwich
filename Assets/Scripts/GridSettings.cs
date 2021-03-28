using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewGridSetting", menuName = "Settings/GridSettings")]
public class GridSettings : ScriptableObject
{
	public IntVector2 gridSize;
	public float spaceBetweenCells;
	public List<GameObject> gamePlayCells;
	public GameObject baseCellPrefab;
}
[Serializable]
public struct IntVector2
{
	public int x;
	public int y;
}