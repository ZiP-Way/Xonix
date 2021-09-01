using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void CollidedWithLine();
    public event CollidedWithLine OnCollidedWithLine;

    public Vector2 Position => transform.position;
    private Vector2 _direction;

    private void Start()
    {
        SetDirection();
        StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.15f);

            CanMoveToNextCell();
            Move();
        }
    }

    private void SetDirection()
    {
        _direction = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)) * 0.5f;

        if (_direction == Vector2.zero)
        {
            SetDirection();
        }
    }

    private void CanMoveToNextCell()
    {
        Vector3 nextPos = new Vector3(transform.position.x + _direction.x, transform.position.y + _direction.y, 0);
        Cell nextCell = GridController.GetCell(nextPos);

        if (nextCell.Type != CellType.GROUND)
        {
            Cell cell;
            cell = GridController.GetCell(new Vector3(transform.position.x + 0.5f, transform.position.y));
            if (cell.Type == CellType.WALL)
            {
                _direction.x = -_direction.x;
            }

            cell = GridController.GetCell(new Vector3(transform.position.x, transform.position.y + 0.5f));
            if (cell.Type == CellType.WALL)
            {
                _direction.y = -_direction.y;
            }

            cell = GridController.GetCell(new Vector3(transform.position.x - 0.5f, transform.position.y));
            if (cell.Type == CellType.WALL)
            {
                _direction.x = -_direction.x;
            }

            cell = GridController.GetCell(new Vector3(transform.position.x, transform.position.y - 0.5f));
            if (cell.Type == CellType.WALL)
            {
                _direction.y = -_direction.y;
            }

            if (_direction.x == 0)
            {
                _direction.x = Random.Range(-1, 2) * 0.5f;
            }

            if (_direction.y == 0)
            {
                _direction.y = Random.Range(-1, 2) * 0.5f;
            }
        }

        if (nextCell.Type == CellType.LINE)
        {
            OnCollidedWithLine?.Invoke();
        }
    }
    private void Move()
    {
        transform.position = new Vector2(transform.position.x + _direction.x,
                transform.position.y + _direction.y);
    }
}
