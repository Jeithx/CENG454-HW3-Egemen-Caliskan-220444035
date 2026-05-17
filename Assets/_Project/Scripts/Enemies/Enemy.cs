using UnityEngine;
using CoreBreach.Interfaces;
using CoreBreach.Core;

namespace CoreBreach.Enemies
{
    public class Enemy : MonoBehaviour, IDamageable, IPoolable
    {
        [SerializeField] private int maxHealth = 30;
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private int contactDamage = 5;
        [SerializeField] private float contactDamageInterval = 1f;
        [SerializeField] private int scoreValue = 10;

        private int currentHealth;
        private IMoveStrategy moveStrategy;
        private ITargetStrategy targetStrategy;
        private float lastContactDamageTime;

        public System.Action<Enemy> ReturnAction;

        public int CurrentHealth => currentHealth;

        public void Configure(IMoveStrategy move, ITargetStrategy target)
        {
            moveStrategy = move;
            targetStrategy = target;
        }

        public void TakeDamage(int amount)
        {
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                GameEvents.RaiseEnemyKilled(scoreValue);
                ReturnAction?.Invoke(this);
            }
        }

        public void OnSpawn()
        {
            currentHealth = maxHealth;
            lastContactDamageTime = 0f;
            moveStrategy = null;
            targetStrategy = null;
        }

        public void OnDespawn()
        {
            moveStrategy = null;
            targetStrategy = null;
            ReturnAction = null;
        }

        private void Update()
        {
            if (moveStrategy == null || targetStrategy == null) return;
            Transform tgt = targetStrategy.GetTarget(transform);
            if (tgt == null) return;
            moveStrategy.Move(transform, tgt, moveSpeed);
        }

        private void OnCollisionStay(Collision collision)
        {
            if (Time.time < lastContactDamageTime + contactDamageInterval) return;

            var dmg = collision.gameObject.GetComponentInParent<IDamageable>();
            if (dmg != null && dmg != (IDamageable)this)
            {
                dmg.TakeDamage(contactDamage);
                lastContactDamageTime = Time.time;
            }
        }
    }
}
