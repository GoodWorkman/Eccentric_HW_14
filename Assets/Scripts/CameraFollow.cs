using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _lerpRate = 10f;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position, _lerpRate * Time.deltaTime);
    }
}
