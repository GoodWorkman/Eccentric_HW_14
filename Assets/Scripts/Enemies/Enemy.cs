using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _rotationSensetivity = 2f;
    [SerializeField] private float _teleportDistanse = 17f;

    [SerializeField] private float _health = 10f;
    [SerializeField] private GameObject _dieParticles;

    [Space(30)]
    [SerializeField] private float _attackPeriod = 1f;
    [SerializeField] private float _damagePerPeriod = 1f;
    private float _attackTimer;
    
    private PlayerHealth _playerHealth;
    private Transform _playerTransform;
    private EnemyManager _enemyManager;
    
    private Rigidbody _rigidbody;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody ??= GetComponent<Rigidbody>();
        _animator ??= GetComponentInChildren<Animator>();
    }

    public void Init(Transform playerTransform, EnemyManager enemyManager)
    {
        _playerTransform = playerTransform;
        _enemyManager = enemyManager;
    }

    private void Update()
    {
        if (!_playerHealth) return;

        _attackTimer += Time.deltaTime;

        if (_attackTimer >= _attackPeriod)
        {
            _playerHealth.TakeDamage(_attackPeriod * _damagePerPeriod);

            _attackTimer = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (_playerTransform)
        {
            Move();
            Rotate();
            TryTeleport();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<PlayerHealth>() is PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
            _animator.SetBool("Attack", _playerHealth);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<PlayerHealth>())
        {
            _playerHealth = null;
            _animator.SetBool("Attack", _playerHealth);
        }
    }

    public void SetDamage(float damage)
    {
        _health -= damage;
        _health = Mathf.Max(_health, 0);

        if (_health == 0)
        {
            Die();
        }
    }
    
    private void Move()
    {
        _rigidbody.velocity = transform.forward * _speed;
    }

    private void Rotate()
    {
        Vector3 toPlayer = _playerTransform.position - transform.position;
        Quaternion toPlayerRotation = Quaternion.LookRotation(toPlayer, Vector3.up);

        _rigidbody.MoveRotation(Quaternion.Lerp(_rigidbody.rotation, toPlayerRotation,
            Time.fixedDeltaTime * _rotationSensetivity));
    }

    private void TryTeleport()
    {
        Vector3 toPlayer = _playerTransform.position - transform.position;

        if (toPlayer.magnitude > _teleportDistanse)
        {
            //transform.position += toPlayer * 1.95f;
            _rigidbody.MovePosition(_rigidbody.position + toPlayer * 1.95f);
        }
    }

    private void Die()
    {
        Instantiate(_dieParticles, transform.position, Quaternion.identity);
        _enemyManager.RemoveEnemy(this);
        Destroy(gameObject);
    }
}