using UnityEngine;
using CoreBreach.Interfaces;

namespace CoreBreach.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealth = 100;

        private int currentHealth;

        public int CurrentHealth => currentHealth;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int amount)
        {
            currentHealth -= amount;
            
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Debug.Log("Player died");
            }
            
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        }
    }
}
