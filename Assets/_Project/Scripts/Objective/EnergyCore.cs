using UnityEngine;
using CoreBreach.Interfaces;
using CoreBreach.Core;

namespace CoreBreach.Objective
{
    public class EnergyCore : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealth = 200;

        private int currentHealth;
        private bool isDestroyed = false;

        public int CurrentHealth => currentHealth;

        private void Start()
        {
            currentHealth = maxHealth;
            GameEvents.RaiseCoreDamaged(currentHealth, maxHealth);
        }

        public void TakeDamage(int amount)
        {
            if (isDestroyed) return;

            currentHealth -= amount;
            currentHealth = Mathf.Max(0, currentHealth);

            GameEvents.RaiseCoreDamaged(currentHealth, maxHealth);

            if (currentHealth == 0)
            {
                isDestroyed = true;
                GameEvents.RaiseCoreDestroyed();
                GameEvents.RaiseGameOver();
            }
        }
    }
}
