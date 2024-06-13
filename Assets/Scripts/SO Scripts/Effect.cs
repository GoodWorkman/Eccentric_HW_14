using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public string Name;
    [TextArea(1, 3)] 
    public string Description;
    public Sprite Sprite;
    public int Level = 0;

    protected EffectsManager _effectsManager;
    protected Player _player;
    protected EnemyManager _enemyManager;
    protected Transform _misslesContainer;

    private int _maxLevel = 10;

    public virtual void Init(EffectsManager effectsManager, EnemyManager enemyManager, Player player, Transform container)
    {
        _effectsManager = effectsManager;
        _enemyManager = enemyManager;
        _player = player;
        _misslesContainer = container;
    }

    public bool ReachedMaxLevel()
    {
        return Level >= _maxLevel;
    }

    public virtual void Activate()
    {
        Level++;
    }
}
