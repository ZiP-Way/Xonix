using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    private static GridController _instance;

    [SerializeField] private EnemySpawner _enemySpawner;

    private MainGrid _grid;
    private GridCompletedPercent _gridCompletedPercent;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        _grid = GetComponent<MainGrid>();
        _gridCompletedPercent = GetComponent<GridCompletedPercent>();
    }


    public static Cell GetRandomCell(CellType cellType)
    {
        Cell cell;
        if (cellType == CellType.GROUND)
        {
            cell = _instance._grid.GroundCells[Random.Range(0, _instance._grid.GroundCells.Count)];
            return cell;
        }
        else if (cellType == CellType.WALL)
        {
            cell = _instance._grid.WallCells[Random.Range(0, _instance._grid.WallCells.Count)];
            return cell;
        }

        throw new System.Exception($"GridController: no type {cellType} cells found");
    }

    public static Cell GetCell(Vector3 cellPosition)
    {
        for (int i = 0; i < _instance._grid.Cells.Count; i++)
        {
            if (_instance._grid.Cells[i].Position == cellPosition)
            {
                return _instance._grid.Cells[i];
            }
        }

        Debug.LogWarning($"{_instance.gameObject.name}: Sended null");
        return null;
    }

    public void RecheckAreaAndFill()
    {
        foreach (Enemy enemy in _enemySpawner.Enemies)
        {
            RecheakCell(enemy.Position.x, enemy.Position.y);
        }
        FillArea();
    }

    private void RecheakCell(float x, float y)
    {
        Cell cell = GetCell(new Vector2(x, y));

        if (cell.Type == CellType.GROUND)
        {
            cell.Type = CellType.TEMP;
        }

        cell = GetCell(new Vector2(x + 0.5f, y));
        if (cell.Type == CellType.GROUND) RecheakCell(x + 0.5f, y);
        cell = GetCell(new Vector2(x - 0.5f, y));
        if (cell.Type == CellType.GROUND) RecheakCell(x - 0.5f, y);
        cell = GetCell(new Vector2(x, y + 0.5f));
        if (cell.Type == CellType.GROUND) RecheakCell(x, y + 0.5f);
        cell = GetCell(new Vector2(x, y - 0.5f));
        if (cell.Type == CellType.GROUND) RecheakCell(x, y - 0.5f);
    }

    private void FillArea()
    {
        int countOfFilledCells = 0;
        for (int i = 0; i < _grid.Cells.Count; i++)
        {
            if (_grid.Cells[i].Type == CellType.LINE || _instance._grid.Cells[i].Type == CellType.GROUND)
            {
                _grid.Cells[i].Color = Color.grey;
                _grid.Cells[i].Type = CellType.WALL;
                countOfFilledCells++;
            }
        }

        _gridCompletedPercent.AddWallCellCount(countOfFilledCells);

        for (int i = 0; i < _instance._grid.Cells.Count; i++)
        {
            if (_grid.Cells[i].Type == CellType.TEMP)
            {
                _grid.Cells[i].Type = CellType.GROUND;
            }
        }
    }
}
