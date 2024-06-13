
using UnityEngine;

public class ContinuousEffect : Effect
{
    [SerializeField] private float _colldown;
    
    private float _timer;

    public void ProcessFrame(float frameTime)
    {
        _timer += frameTime;

        if (_timer > _colldown)
        {
            Produce();

            _timer = 0;
        }
    }

    protected virtual void Produce()
    {
    }
    
    
}
