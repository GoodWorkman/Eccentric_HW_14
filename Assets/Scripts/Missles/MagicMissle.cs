using UnityEngine;

public class MagicMissle : MonoBehaviour
{
    private Enemy _targetEnemy;
    private float _damage;
    private float _speed;
    private float _minDistance;

    public void Init(Enemy targetEnemy, float damage, float speed)
    {
        _targetEnemy = targetEnemy;
        _damage = damage;
        _speed = speed;

        Destroy(gameObject, 4f);
    }

    private void Update()
    {
        if (_targetEnemy)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetEnemy.transform.position,
                _speed * Time.deltaTime);

            if (transform.position == _targetEnemy.transform.position) //MoveTowards приводит точное соответствие
            {
                AffectEnemy();
                
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void AffectEnemy()
    {
        _targetEnemy.SetDamage(_damage);
    }
}