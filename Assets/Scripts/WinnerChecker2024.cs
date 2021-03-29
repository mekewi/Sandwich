using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WinnerChecker2024 : MonoBehaviour
{
    public GridSettings gridSettings;
    void Start()
    {
        GameEventsHub.Instance.Register<CellSwipedEvent>(OnCellSwiped);
    }

    private void OnDestroy()
    {
        GameEventsHub.Instance.UnRegister<CellSwipedEvent>(OnCellSwiped);
    }

    private void OnCellSwiped(CellSwipedEvent obj)
    {
        var cellPower = obj.targetCell.GetCellPower();
        List<Transform> allChild = obj.targetCell.GetChild();
        foreach (Transform child in allChild)
        {
            Destroy(child.gameObject);
        }
        GameObject newCell = gridSettings.gamePlayCells.Find(x => x.GetComponent<CellContent>().power == cellPower);
        GridManager.Instance.AddGameObjectToCell(newCell,obj.targetCell);
        if (GridManager.Instance.GetAllOccupiedCell().Count > 1)
        {
            return;
        }
        var allOccupiedCells = GridManager.Instance.GetAllOccupiedCell();
        if (GridManager.Instance.GetAllOccupiedCell().Count == 1)
        {
            GameEventsHub.Instance.Notify<WinLoseEvent>(true);
            Debug.Log("Win");
            MatchWidth.newBounds = allOccupiedCells[0].GetComponent<BoxCollider>().bounds;
            MatchWidth.PositionCamera();
        }
    }

}