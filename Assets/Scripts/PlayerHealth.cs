using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
   [SerializeField] private float _maxHealth = 100f;

   private float _currentHealth;
   private GameStateManager _gameStateManager;

   public event Action<float, float> OnHealthChanged; 
   public event Action OnPlayerDie;

   private void Start()
   {
      SetHealth(_maxHealth);
   }

   public void Init(GameStateManager gameStateManager)
   {
      _gameStateManager = gameStateManager;
   }

   public void TakeDamage(float value)
   {
      float damagedHealth = _currentHealth - value;
      damagedHealth = Mathf.Max(damagedHealth, 0f);

      SetHealth(damagedHealth);

      if (damagedHealth == 0)
      {
         Die();
      }
   }

   public void AddHealth(float value)
   {
      float addedHealth = _currentHealth + value;

      addedHealth = Mathf.Min(addedHealth, _maxHealth);
      
      SetHealth(addedHealth);
   }
   
   private void SetHealth(float newHealth)
   {
      _currentHealth = newHealth;
      
      OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
   }
   
   private void Die()
   {
      OnPlayerDie?.Invoke();
      
      _gameStateManager.SetLose();
      
      Debug.Log("Die");
   }
}
