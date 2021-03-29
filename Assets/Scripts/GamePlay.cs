using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class GamePlay : Singleton<GamePlay>
{
	private float zOffset = 10;
	private GridSettings _settings;
	public LevelSteps levelSteps;
	private GridManager _gridManager ;
	public override void Awake()
    {
	    base.Awake();
	    _gridManager = GridManager.Instance;
	    GameEventsHub.Instance.Register<SwipeDetectedEvent>(OnSwipeDetectoed);
    }

	private void Start()
	{
		for (int i = 0; i < levelSteps.Steps.Count; i++)
		{
			GameObject gamePlayCell = _gridManager.GetSpecificIngrediant(levelSteps.Steps[i].cellTag);
			Cell cellToOccupy = levelSteps.Steps[i].addToLastCellNeighbor ? _gridManager.GetRandomNeighbor(_gridManager.lastCellAdded) : null;
			if (levelSteps.Steps[i].addToSpecificCell)
			{
				cellToOccupy = _gridManager.GetCellByCoordinate(levelSteps.Steps[i].cellCoordinated);
			}
			if (levelSteps.Steps[i].addSetOfRandomCells)
			{
				StartCoroutine(AddSetOfRandomCells(levelSteps.Steps[i]));
				continue;
			}
			_gridManager.AddGameplayCellToGrid(gamePlayCell,cellToOccupy);
		}
	}

	private IEnumerator AddSetOfRandomCells(LevelStep step)
	{
		for (int j = 0; j < step.amountOfRandomCells; j++)
		{
			GameObject gamePlayCell = _gridManager.GetSpecificIngrediant(step.cellTag);
			Cell cellNeighbor = null;
			while (cellNeighbor == null)
			{
				var cell = _gridManager.GetRandomOccupiedCell();
				cellNeighbor = _gridManager.GetRandomNeighbor(cell);
				yield return new WaitForEndOfFrame();
			}

			if (cellNeighbor == null)
			{
				continue;
			}
			_gridManager.AddGameplayCellToGrid(gamePlayCell, cellNeighbor);
		}
	}

	private void OnDestroy()
    {
	    GameEventsHub.Instance.UnRegister<SwipeDetectedEvent>(OnSwipeDetectoed);
    }
    private void OnSwipeDetectoed(SwipeDetectedEvent eventData)
    {
	    var data = eventData.swipeData;
	    Ray ray = Camera.main.ScreenPointToRay(new Vector3(data.EndPosition.x, data.EndPosition.y, zOffset));
	    if ( Physics.Raycast (ray,out RaycastHit hit,100.0f)) {
		    Cell cell = hit.transform.GetComponent<Cell>();
		    _gridManager.SelectedCell(data,cell);
	    }
    }

}
