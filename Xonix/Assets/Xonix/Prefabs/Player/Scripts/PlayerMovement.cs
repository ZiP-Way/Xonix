using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 _direction;

    private PlayerDrawing _playerDrawing;
    private Coroutine _currentCoroutine;

    private void Awake()
    {
        _playerDrawing = GetComponent<PlayerDrawing>();
    }

    private void Start()
    {
        transform.position = GridController.GetRandomCell(CellType.WALL).Position; ;
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;

        if (_currentCoroutine == null)
        {
            _currentCoroutine = StartCoroutine(MovingDelay());
        }
    }

    private void Move()
    {
        transform.position = new Vector2(transform.position.x + _direction.x,
            transform.position.y + _direction.y);
    }

    private IEnumerator MovingDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.15f);

            Vector3 nextPosition = new Vector3(transform.position.x + _direction.x, transform.position.y + _direction.y, 0);
            Cell nextCell = GridController.GetCell(nextPosition);

            if (nextCell)
            {
                Move();
                _playerDrawing.Draw(nextCell);
            }
        }
    }


}
