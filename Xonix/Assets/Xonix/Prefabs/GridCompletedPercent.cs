using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GridCompletedPercent : MonoBehaviour
{
    public UnityEvent OnVictory;

    [SerializeField] private Text _percentText;
    [SerializeField] private int _percentTarget;

    private MainGrid _grid;

    private int _countOfCells;
    private int _countOfFilledCells;

    private void Awake()
    {
        _grid = GetComponent<MainGrid>();
    }

    private void Start()
    {
        _countOfCells = _grid.GroundCells.Count;
        CalculatePercent();
    }

    public void AddWallCellCount(int count)
    {
        _countOfFilledCells += count;
        CalculatePercent();
    }

    private void CalculatePercent()
    {
        float percent = Mathf.Round((float)_countOfFilledCells / _countOfCells * 100);
        percent = Mathf.Clamp(percent, 0, 100);
        _percentText.text = percent.ToString() + " %";

        if(percent >= _percentTarget)
        {
            OnVictory?.Invoke();
        }
    }

}
