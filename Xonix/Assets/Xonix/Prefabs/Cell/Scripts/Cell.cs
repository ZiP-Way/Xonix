using UnityEngine;

public enum CellType
{
    GROUND,
    LINE,
    WALL,
    TEMP
}

public class Cell : MonoBehaviour
{
    public CellType Type
    {
        get
        {
            return _cellType;
        }

        set
        {
            _cellType = value;
        }
    }
    public Color Color
    {
        get
        {
            return _color;
        }
        set
        {
            if (_cellType != CellType.WALL)
            {
                if (_spriteRenderer != null)
                {
                    _color = value;
                    _spriteRenderer.color = _color;
                }
                else
                {
                    throw new System.Exception($"{gameObject.name}: SpriteRenderer is null");
                }
            }
        }
    }
    public Vector3 Position => _position;

    [SerializeField] private CellType _cellType;
    private Color _color;
    private Vector3 _position;

    private SpriteRenderer _spriteRenderer;

    public void Initialization(Vector3 position, Color color, CellType cellType)
    {
        _cellType = cellType;
        _color = color;
        _position = position;

        transform.position = position;
        if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
        {
            _spriteRenderer.color = color;
        }
    }
}
