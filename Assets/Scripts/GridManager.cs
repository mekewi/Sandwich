using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GridManager : Singleton<GridManager>
{
	public GridSettings gridSettings;
	public Cell[,] grid;
	public Cell lastCellAdded;
	public GameObject dustParticle;
	void Start()
	{
		GenerateGrid();
	}
	private void GenerateGrid()
	{
		grid = new Cell[gridSettings.gridSize.x, gridSettings.gridSize.y];
		for (int row = 0; row < gridSettings.gridSize.x; row++)
		{
			for (int column = 0; column < gridSettings.gridSize.y; column++)
			{
				var newCell = Instantiate(gridSettings.baseCellPrefab);
				float cellWidth = newCell.GetComponent<BoxCollider>().bounds.size.x + gridSettings.spaceBetweenCells;
				float cellHeight = newCell.GetComponent<BoxCollider>().bounds.size.z + gridSettings.spaceBetweenCells;
				float halfOfCellWidth = cellWidth / 2;
				float halfOfGridWidth = gridSettings.gridSize.x * cellWidth / 2;
				float halfOfCellHeight = cellHeight / 2;
				float halfOfGridHeight = gridSettings.gridSize.x * cellHeight / 2;
				float cellPositionX = (row * cellWidth + halfOfCellWidth) - halfOfGridWidth;
				float cellPositionZ = column * cellHeight + halfOfCellHeight - halfOfGridHeight;
				newCell.transform.position = new Vector3(cellPositionX, 0, cellPositionZ);
				newCell.GetComponent<BoxCollider>().size = new Vector3(cellWidth,0.1f,cellHeight);
				newCell.GetComponent<Cell>().cellX = row;
				newCell.GetComponent<Cell>().cellY = column;
				Bounds newBounds = new Bounds(new Vector3(0,0,0), new Vector3(cellWidth * gridSettings.gridSize.x,cellWidth * gridSettings.gridSize.y) );
				MatchWidth.newBounds = newBounds;
				MatchWidth.PositionCamera();
				grid[row, column] = newCell.GetComponent<Cell>();
			}
		}
	}
	public void AddRandomToCell(int count, Cell cell)
	{
		Cell newIngrediant = AddGameplayCellToGrid(null,GetRandomNeighbor(cell));
		count--;
		if (count == 0)
		{
			return;
		}
		else
		{
			AddRandomToCell(count, newIngrediant);
		}
	}
	public GameObject GetSpecificIngrediant(string cellName)
	{
		if (cellName == "Random")
		{
			return GetRandomGamePlayCell();
		}
		return gridSettings.gamePlayCells.Find(x => x.name.ToLower() == cellName.ToLower());
	}

	public List<Cell> GetAllOccupiedCell()
	{
		List<Cell> allOccupiedCell = new List<Cell>();
		foreach (Cell cell in grid)
		{
			if (cell.HasChiled())
			{
				allOccupiedCell.Add(cell);
			}
		}
		return allOccupiedCell;
	}

	public Cell GetRandomOccupiedCell()
	{
		var allOccupiedCell = GetAllOccupiedCell();
		int randomNumber = Random.Range(0, allOccupiedCell.Count);
		return allOccupiedCell[randomNumber];
	}

	public List<Cell> GetAvailableNeighbor(Cell cell)
	{
		List<Cell> allAvailableNeighbor = new List<Cell>();
		Cell neighborCell = null;
		for (int i = 0; i < 4; i++)
		{
			switch (i)
			{
				case (int)Direction.Right:
					if (cell.cellX + 1 >= gridSettings.gridSize.x)
					{
						continue;
					}
					neighborCell = grid[cell.cellX + 1, cell.cellY];
					if (!neighborCell.HasChiled())
					{
						allAvailableNeighbor.Add(neighborCell);
					}
					break;
				case (int)Direction.Left:
					if (cell.cellX - 1 < 0)
					{
						continue;
					}
					neighborCell = grid[cell.cellX - 1, cell.cellY];
					if (!neighborCell.HasChiled())
					{
						allAvailableNeighbor.Add(neighborCell);
					}
					break;
				case (int)Direction.Up:
					if (cell.cellY + 1 >= gridSettings.gridSize.y)
					{
						continue;
					}
					neighborCell = grid[cell.cellX, cell.cellY + 1];
					if (!neighborCell.HasChiled())
					{
						allAvailableNeighbor.Add(neighborCell);
					}
					break;
				case (int)Direction.Down:
					if (cell.cellY - 1 < 0)
					{
						continue;
					}
					neighborCell = grid[cell.cellX, cell.cellY - 1];
					if (!neighborCell.HasChiled())
					{
						allAvailableNeighbor.Add(neighborCell);
					}
					break;
				default:
					return null;
			}
		}
		return allAvailableNeighbor;
	}
	public Cell GetRandomNeighbor(Cell cell)
	{
		var allNeighbor = GetAvailableNeighbor(cell);
		if (allNeighbor.Count ==0)
		{
			return null;
		}
		int randomNumber = Random.Range(0, allNeighbor.Count);
		return allNeighbor[randomNumber];
	}
	public Cell AddGameplayCellToGrid(GameObject cellObject = null,Cell cell = null)
	{
		if (cellObject == null)
		{
			cellObject = GetRandomGamePlayCell();
		}
		if (cell == null)
		{
			cell = GetRandomCell();
		}
		AddGameObjectToCell(cellObject,cell);
		lastCellAdded = cell;
		return cell;
	}
	public GameObject GetRandomGamePlayCell()
	{
		int randomNumber = Random.Range(0, gridSettings.gamePlayCells.Count);
		return gridSettings.gamePlayCells[randomNumber];
	}
	public Cell GetRandomCell()
	{
		int randomCellX = Random.Range(0, gridSettings.gridSize.x);
		int randomCellY = Random.Range(0, gridSettings.gridSize.y);
		while (grid[randomCellX,randomCellY].HasChiled())
		{
			randomCellX = Random.Range(0, gridSettings.gridSize.x);
			randomCellY = Random.Range(0, gridSettings.gridSize.y);
		}
		return grid[randomCellX, randomCellY];
	}
	public Cell GetCellByCoordinate(IntVector2 cordinates)
	{
		return grid[cordinates.x, cordinates.y];
	}
	public void AddGameObjectToCell(GameObject objectToPlace,Cell cell)
	{
		var newCell = Instantiate(objectToPlace,cell.transform);
		newCell.transform.localPosition = Vector3.zero;
	}
	public Cell GetTargetCell(Direction direction,Cell cell)
	{
		Cell targetCell;
		switch (direction)
		{
			case Direction.Right:
				if (cell.cellX + 1 >= gridSettings.gridSize.y)
				{
					return null;
				}
				targetCell = grid[cell.cellX + 1, cell.cellY];
				break;
			case Direction.Left:
				if (cell.cellX - 1 < 0)
				{
					return null;
				}
				targetCell = grid[cell.cellX - 1, cell.cellY];
				break;
			case Direction.Up:
				if (cell.cellY + 1 >= gridSettings.gridSize.y)
				{
					return null;
				}

				targetCell = grid[cell.cellX, cell.cellY + 1];
				break;
			case Direction.Down:
				if (cell.cellY - 1 < 0)
				{
					return null;
				}
				targetCell = grid[cell.cellX, cell.cellY - 1];
				break;
			default:
				return null;
		}

		if (targetCell.HasChiled())
		{
			return targetCell;
		}
		return null;
	}
	public void SelectedCell(SwipeData data, Cell cell)
	{
		GameObject pivot = new GameObject();
		pivot.transform.position = cell.transform.position;
		Cell targetCell = GetTargetCell(data.Direction,cell);
		if (targetCell == null || targetCell.GetCellPower() != cell.GetCellPower())
		{
			return;
		}
		
		Vector3 dir;
		Vector3 dirRotation;
		if (data.Direction == Direction.Right)
		{
			dir = Vector3.right;
			dirRotation = Vector3.back;
		}else if (data.Direction == Direction.Left)
		{
			dir = Vector3.left;
			dirRotation = Vector3.forward;
		}else if (data.Direction == Direction.Up)
		{
			dir = Vector3.forward;
			dirRotation = Vector3.right;
		}else if (data.Direction == Direction.Down)
		{
			dir = Vector3.back;
			dirRotation = Vector3.left;
		}
		else { return;}
		AddPivot(cell,targetCell,pivot.transform,dir);
		List<Transform> allChiled = cell.GetChild();
		for (int i = 0; i < allChiled.Count; i++)
		{
			allChiled[i].SetParent(pivot.transform);
		}
		pivot.transform.DORotate(180*dirRotation, 0.5f).onComplete = () =>
		{
			foreach (Transform item in allChiled)
			{
				item.SetParent(targetCell.transform);
			}
			Destroy(pivot);
			Destroy(Instantiate(dustParticle, targetCell.transform.position,Quaternion.identity),0.5f);
			
			GameEventsHub.Instance.Notify<CellSwipedEvent>(targetCell);
		};
	}
	public void AddPivot(Cell cell,Cell targetCell,Transform pivot,Vector3 pivotDir)
	{
		var cellSize = cell.GetComponent<BoxCollider>().size.x;
		var cellSizey = cell.GetComponent<BoxCollider>().size.y;
		if (gridSettings.isStackedCells)
		{
			cellSizey = ((cellSizey/2) * targetCell.transform.childCount)+(cell.transform.childCount*(cellSizey/2));
		}
		else
		{
			cellSizey = cellSizey/2;
		}
		var position = pivot.transform.position;
		position = new Vector3(position.x,
			cellSizey, position.z) + (pivotDir* (cellSize / 2));
		pivot.transform.position = position;
	}
}

