using System.Collections.Generic;
using UnityEngine;

public class MainGrid : MonoBehaviour
{
    public List<Cell> Cells => _cells;
    public List<Cell> GroundCells => _groundCells;
    public List<Cell> WallCells => _wallCells;

    public Vector2 StartPoint => _gridStartPoint; 

    [SerializeField] private Vector2 _gridSize;
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private Transform _cellContainer;

    [Header("Colors")]
    [SerializeField] private Color _wallColor;
    [SerializeField] private Color _groundColor;

    private List<Cell> _cells;
    private List<Cell> _wallCells;
    private List<Cell> _groundCells;

    private float _cellSize;

    private Vector2 _gridStartPoint; //the point from which the generation began
    private GridCompletedPercent _gridCompletedPercent;
    private void Awake()
    {
        _cells = new List<Cell>();
        _wallCells = new List<Cell>();
        _groundCells = new List<Cell>();

        _cellSize = _cellPrefab.transform.localScale.x;
        Generate();
    }

    private void Generate()
    {
        Cell cell;
        Vector2 cellPosition;
        CellType cellType;
        Color cellColor;

        for (float i = 0, y = -_gridSize.y / 4; i <= _gridSize.y; i++, y += _cellSize)
        {
            for (float j = 0, x = -_gridSize.x / 4; j <= _gridSize.x; j++, x += _cellSize)
            {

                cell = Instantiate(_cellPrefab, _cellContainer);
                cellPosition = new Vector3(x, y, 0);
                cellType = CellType.GROUND;
                cellColor = _groundColor;

                if (y == -_gridSize.y / 4 || y == _gridSize.y / 4 ||
                    x == -_gridSize.x / 4 || x == _gridSize.x / 4)
                {
                    cellColor = _wallColor;
                    cellType = CellType.WALL;
                    _wallCells.Add(cell);
                }
                else
                {
                    _groundCells.Add(cell);
                }

                cell.Initialization(cellPosition, cellColor, cellType);
                _cells.Add(cell);

                if (i == 0 && j == 0)
                {
                    _gridStartPoint = cellPosition;
                }
            }
        }
    }
}