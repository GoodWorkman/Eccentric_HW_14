using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _scale;
    [SerializeField] private PlayerHealth _playerHealth;

    private void Awake()
    {
        _playerHealth.OnHealthChanged += SetScale;
    }

    private void SetScale(float currentValue, float maxValue)
    {
        _scale.fillAmount = currentValue / maxValue;
    }

    private void OnDestroy()
    {
        _playerHealth.OnHealthChanged -= SetScale;
    }
}
