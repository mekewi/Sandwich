using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WinnerChecker : MonoBehaviour
{
    // Start is called before the first frame update
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
        CheckWin();
    }

    public void CheckWin()
    {
        int cellsCount = 0;
        Cell lastCell = null;
        foreach (Cell cell in GridManager.Instance.grid)
        {
            if (cell.HasChiled())
            {
                cellsCount++;
                lastCell = cell;
            }
        }

        if (cellsCount > 1)
        {
            return;
        }
        if (lastCell != null)
        {
            List<Transform> chiled = lastCell.GetChild().OrderBy(x=>x.transform.position.y).ToList();
            if (chiled[0].tag.Equals("Bread") && chiled[chiled.Count-1].tag.Equals("Bread"))
            {
                Debug.Log("Win");
                GameEventsHub.Instance.Notify<WinLoseEvent>(true);
                MatchWidth.newBounds = lastCell.GetComponent<BoxCollider>().bounds;
                MatchWidth.PositionCamera();

            }
            else
            {
                GameEventsHub.Instance.Notify<WinLoseEvent>(false);
                Debug.Log("Loser");
            }

            return;
        }

        return;
    }
}
