using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private GameStateManager _gameStateManager;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private CardManager _cardManager;
    [SerializeField] private EffectsManager _effectsManager;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _misslesContainer;
    [SerializeField] private LootManager _lootManager;

    private void Awake()
    {
        _gameStateManager.Init();
        _playerHealth.Init(_gameStateManager);
        _effectsManager.Init(_cardManager, _enemyManager, _player, _misslesContainer);
        _lootManager.Init(_enemyManager);
    }
}