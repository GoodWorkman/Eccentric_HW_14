using UnityEngine;

public class ShadowMissle : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;
    [SerializeField] private ParticleSystem _particleSystem;

    private float _damage;
    private int _passCount;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    public void Init(Vector3 velocity, float damage, int passCount)
    {
        _rigidbody.rotation = Quaternion.LookRotation(velocity);
        _damage = damage;
        _rigidbody.velocity = velocity;
        _passCount = passCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Enemy>() is Enemy enemy)
        {
            enemy.SetDamage(_damage);
            _passCount--;

            if (_passCount == 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        _rigidbody.velocity = Vector3.zero;
        _collider.enabled = false;
        _particleSystem.Stop();
        Destroy(gameObject, 2f);
    }
}
