using UnityEngine;

public class LootManager : MonoBehaviour
{
    //[SerializeField] private Loot[] _loots;
    [SerializeField] private ExperienceLoot _experienceLoot;
    [SerializeField] private HealthLoot _healthLoot;

    private EnemyManager _enemyManager;
    private int _expCounter = 0;
    private int _hpCounter = 0;

    public void Init(EnemyManager enemyManager)
    {
        _enemyManager = enemyManager;
        _enemyManager.OnEnemyDied += SpawnLoot;
    }

    private void SpawnLoot(Vector3 position)
    {
        Loot newLoot = null;

        if (_expCounter % 2 == 0) newLoot = _experienceLoot;
        if (_hpCounter % 5 == 0) newLoot = _healthLoot;

        if (_expCounter % 3 == 0 && _hpCounter % 11 == 0)
        {
            newLoot = _experienceLoot;
        }

        if (newLoot)
        {
            Instantiate(newLoot, position, Quaternion.identity, transform);
        }

        _expCounter++;
        _hpCounter++;
    }

    private void OnDestroy()
    {
        _enemyManager.OnEnemyDied -= SpawnLoot;
    }
}