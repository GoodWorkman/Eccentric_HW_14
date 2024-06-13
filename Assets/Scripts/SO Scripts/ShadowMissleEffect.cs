using UnityEngine;

[CreateAssetMenu(fileName = nameof(ShadowMissleEffect), menuName = "Effects/Continuous/" + nameof(ShadowMissleEffect))]
public class ShadowMissleEffect : ContinuousEffect
{
    [SerializeField] private ShadowMissle _shadowMissle;
   
    [SerializeField] private float _missleSpeed = 4f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private int _bulletsCount = 5;
    [SerializeField] private int _passCount = 2;

    protected override void Produce()
    {
        base.Produce();

        Transform playerTransform = _player.transform;

        for (int i = 0; i < _bulletsCount; i++)
        {
            float angle = (360 / _bulletsCount) * i;

            Vector3 direction = Quaternion.Euler(0f, angle, 0f) * playerTransform.forward;

            ShadowMissle newMissle = Instantiate(_shadowMissle, playerTransform.position, Quaternion.identity,
                _misslesContainer);

            newMissle.Init(direction * _missleSpeed, _damage, _passCount);
        }
    }
}