using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _creationRadius = 5f;
    [SerializeField] private ChapterSettings _chapterSettings;

    private List<Enemy> _enemiesList = new();
    private int _wave;
    private float _second = 1f;
    private bool _isMaxWave = false;

    public event Action<Vector3> OnEnemyDied;

    public void WaveLevelUp(int level)
    {
        IncrementWave(level);
        StartNewWave(_wave);
    }
    
    public void RemoveEnemy(Enemy enemy)
    {
        OnEnemyDied?.Invoke(enemy.transform.position);
        _enemiesList.Remove(enemy);
    }

    private void StartNewWave(int wave)
    {
        StopAllCoroutines();

        for (int i = 0; i < _chapterSettings.EnemyWavesArray.Length; i++)
        {
            if (wave < _chapterSettings.EnemyWavesArray[i].NumberPerSeconds.Length &&
                _chapterSettings.EnemyWavesArray[i].NumberPerSeconds[wave] > 0)
            {
                StartCoroutine(CreateEnemyInSeconds(_chapterSettings.EnemyWavesArray[i].Enemy,
                    _chapterSettings.EnemyWavesArray[i].NumberPerSeconds[wave]));
            }
        }
    }

    private IEnumerator CreateEnemyInSeconds(Enemy enemy, float enemyPerSecond)
    {
        while (gameObject.activeInHierarchy)
        {
            //yield return new WaitForSecondsRealtime(_second / enemyPerSecond);
            yield return new WaitForSeconds(_second / enemyPerSecond);
            Create(enemy);
        }
    }

    private void Create(Enemy enemy)
    {
        Vector2 randomPoint = Random.insideUnitCircle.normalized;

        Vector3 position = new Vector3(randomPoint.x, 0f, randomPoint.y) * _creationRadius + _playerTransform.position;

        Enemy newEnemy = Instantiate(enemy, position, Quaternion.identity, transform);

        newEnemy.Init(_playerTransform, this);

        _enemiesList.Add(newEnemy);
    }

    private void IncrementWave(int level)
    {
        if (_isMaxWave) return;
        
        _wave = level;

        foreach (EnemyWaves waves in _chapterSettings.EnemyWavesArray)
        {
            if (_wave >= waves.NumberPerSeconds.Length)
            {
                _isMaxWave = true;
                _wave = waves.NumberPerSeconds.Length - 1;
                break;
            }
        }
    }
    
    public Enemy[] GetNearest(Vector3 point, int ammoCount)
    {
        _enemiesList = _enemiesList.OrderBy(x => Vector3.Distance(point, x.transform.position)).ToList();

        int returnNumber = Mathf.Min(ammoCount, _enemiesList.Count);

        Enemy[] nearestEnemies = new Enemy[returnNumber];

        for (int i = 0; i < nearestEnemies.Length; i++)
        {
            nearestEnemies[i] = _enemiesList[i];
        }

        return nearestEnemies;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
       {
           Handles.color = Color.yellow;
           Handles.DrawWireDisc(_playerTransform.position, Vector3.up, _creationRadius);
       }
#endif
   
}