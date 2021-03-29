using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewLevelSteps", menuName = "Level/Steps")]
public class LevelSteps : ScriptableObject
{
    public List<LevelStep> Steps = new List<LevelStep>();
}
[Serializable]
public class LevelStep
{
    public string cellTag;
    public bool addToLastCellNeighbor;
    public bool addToSpecificCell;
    public IntVector2 cellCoordinated;
    public bool addSetOfRandomCells;
    public int amountOfRandomCells;
}