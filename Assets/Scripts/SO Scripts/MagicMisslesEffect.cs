using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(MagicMisslesEffect), menuName = "Effects/Continuous/" + nameof(MagicMisslesEffect))]

public class MagicMisslesEffect : ContinuousEffect
{
   [SerializeField] private MagicMissle _magicMisslePrefab;
   [SerializeField] private float _bulletSpeed = 6f;
   [SerializeField] private float _bulletDamage = 6f;
   [SerializeField] private int _ammoCount = 4;

   protected override void Produce()
   {
      base.Produce();

      _effectsManager.StartCoroutine(EffectProcess());
   }

   private IEnumerator EffectProcess()
   {
      Enemy[] nearestEnemy = _enemyManager.GetNearest(_player.transform.position, _ammoCount);

      if (nearestEnemy.Length > 0)
      {
         for (int i = 0; i < nearestEnemy.Length; i++)
         {
            Vector3 position = _player.transform.position;

            MagicMissle newMissle = Instantiate(_magicMisslePrefab, position, Quaternion.identity, _misslesContainer);
            
            newMissle.Init(nearestEnemy[i], _bulletDamage, _bulletSpeed);

            yield return new WaitForSeconds(.3f);
         }
      }
   }
}
