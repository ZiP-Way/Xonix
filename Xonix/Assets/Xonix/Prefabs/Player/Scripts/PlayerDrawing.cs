using UnityEngine;

public class PlayerDrawing : MonoBehaviour
{
    [SerializeField] private GridController _gridController;

    private bool _isDrawing;

    public void Draw(Cell cell)
    {
        if (cell.Type == CellType.GROUND)
        {
            _isDrawing = true;
            cell.Type = CellType.LINE;
            cell.Color = Color.magenta;
        }
        else if (cell.Type == CellType.WALL && _isDrawing)
        {
            _gridController.RecheckAreaAndFill();
            _isDrawing = false;
        }
    }
}
