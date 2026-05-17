using UnityEngine;
using CoreBreach.Interfaces;
using CoreBreach.Pooling;

namespace CoreBreach.Weapons
{
    public class BaseWeapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private int initialPoolSize = 30;
        [SerializeField] private int baseDamage = 10;
        [SerializeField] private float baseCooldown = 0.2f;

        private PoolManager<Projectile> projectilePool;
        private bool nextShotPiercing = false;

        public int Damage => baseDamage;
        public float Cooldown => baseCooldown;

        private void Awake()
        {
            projectilePool = new PoolManager<Projectile>(projectilePrefab, initialPoolSize, this.transform.parent);
        }

        public void EnableNextShotPiercing()
        {
            nextShotPiercing = true;
        }

        public void Fire(Vector3 origin, Vector3 direction, int damageOverride = -1)
        {
            int finalDamage = damageOverride == -1 ? baseDamage : damageOverride;
            Vector3 dir = direction.normalized;
            Projectile p = projectilePool.Get(origin, Quaternion.LookRotation(dir));
            p.SetDamage(finalDamage);
            p.Piercing = nextShotPiercing;
            nextShotPiercing = false;
            p.ReturnAction = (proj) => projectilePool.Return(proj);
            p.Launch(dir);
        }

        public string DescribeChain()
        {
            return "Base";
        }
    }
}
