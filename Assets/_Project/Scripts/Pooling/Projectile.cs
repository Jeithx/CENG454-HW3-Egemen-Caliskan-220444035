using UnityEngine;
using CoreBreach.Interfaces;

namespace CoreBreach.Pooling
{
    public class Projectile : MonoBehaviour, IPoolable
    {
        [SerializeField] private float speed = 25f;
        [SerializeField] private float lifetime = 3f;
        [SerializeField] private int damage = 10;
        [SerializeField] private LayerMask hitMask;


        private Rigidbody rb;
        private float deathTime;
        private bool isActiveProjectile;

        public int Damage => damage;
        public System.Action<Projectile> ReturnAction;
        public bool Piercing = false;

        public void SetDamage(int newDamage)
        {
            damage = newDamage;
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
        }

        public void Launch(Vector3 direction)
        {
            rb.linearVelocity = direction.normalized * speed;
            deathTime = Time.time + lifetime;
            isActiveProjectile = true;
        }

        public void OnSpawn()
        {
            isActiveProjectile = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            Piercing = false;
        }

        public void OnDespawn()
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            isActiveProjectile = false;
            ReturnAction = null;
            Piercing = false;
        }

        private void Update()
        {
            if (isActiveProjectile && Time.time >= deathTime)
            {
                ReturnToPool();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!isActiveProjectile) return;

            if (other.CompareTag("Player")) return;

            if ((hitMask & (1 << other.gameObject.layer)) == 0) return;

            IDamageable dmg = other.GetComponentInParent<IDamageable>();
            if (dmg != null)
            {
                dmg.TakeDamage(damage);
            }

            if (!Piercing) ReturnToPool();
        }

        private void ReturnToPool()
        {
            ReturnAction?.Invoke(this);
        }
    }
}
