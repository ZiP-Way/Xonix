using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    public UnityEvent OnDefeat;

    public List<Enemy> Enemies => _enemies;

    [SerializeField] private Enemy _prefab;
    [SerializeField] private int _countOfEnemies;

    private List<Enemy> _enemies;

    private void Awake()
    {
        _enemies = new List<Enemy>();
    }

    private void Start()
    {
        EnemyInitialization();
    }

    private void EnemyInitialization ()
    {
        for (int i = 0; i < _countOfEnemies; i++)
        {
            Enemy enemy = Instantiate(_prefab, transform);
            enemy.transform.position = GridController.GetRandomCell(CellType.GROUND).Position;
            enemy.OnCollidedWithLine += CollideWithLine;
            _enemies.Add(enemy);
        }
    }

    private void CollideWithLine()
    {
        OnDefeat?.Invoke();
        Time.timeScale = 0;
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].OnCollidedWithLine -= CollideWithLine;
        }
    }
}
