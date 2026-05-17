using UnityEngine;
using CoreBreach.Pooling;

namespace CoreBreach.Player
{
    /// <summary>
    /// Temporary direct-pool firing component. Will be replaced by an 
    /// IWeapon-based decorator chain in feat/weapon-decorator.
    /// </summary>
    public class PlayerFiring : MonoBehaviour
    {
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private Transform muzzle;
        [SerializeField] private int initialPoolSize = 20;
        [SerializeField] private float fireCooldown = 0.2f;

        private PoolManager<Projectile> projectilePool;
        private float nextFireTime;

        private void Awake()
        {
            projectilePool = new PoolManager<Projectile>(projectilePrefab, initialPoolSize, this.transform.parent);
        }

        private void Update()
        {
            if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
            {
                Fire();
                nextFireTime = Time.time + fireCooldown;
            }
        }

        private void Fire()
        {
            Vector3 origin = muzzle.position;
            Vector3 direction = muzzle.forward;
            
            Projectile p = projectilePool.Get(origin, Quaternion.LookRotation(direction));
            p.ReturnAction = (proj) => projectilePool.Return(proj);
            p.Launch(direction);
        }
    }
}
