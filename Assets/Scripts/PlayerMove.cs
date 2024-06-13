using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _sensetivity = 2f;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Animator _animator;

    private Vector2 _inputVector;
    private Rigidbody _rigidbody;

    private void OnValidate()
    {
        _rigidbody ??= GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _inputVector = _joystick.Value;
        _animator.SetBool("IsRunning", _joystick.IsPressed);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_inputVector.x, 0f, _inputVector.y).normalized * _speed;

        if (_rigidbody.velocity != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_rigidbody.velocity, Vector3.up);

            _rigidbody.MoveRotation(Quaternion.Lerp(_rigidbody.rotation, targetRotation, _sensetivity));
        }
    }
}