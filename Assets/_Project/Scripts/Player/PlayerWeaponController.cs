using UnityEngine;
using CoreBreach.Interfaces;
using CoreBreach.Weapons;
using CoreBreach.Weapons.Decorators;
using CoreBreach.Core;

namespace CoreBreach.Player
{
    public class PlayerWeaponController : MonoBehaviour
    {
        [SerializeField] private BaseWeapon baseWeapon;
        [SerializeField] private Transform muzzle;

        private IWeapon currentWeapon;
        private bool hasDoubleDamage = false;
        private bool hasPiercing = false;
        private bool hasMultiShot = false;
        private float nextFireTime;

        private void Awake()
        {
            currentWeapon = baseWeapon;
            RebuildChain();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
            {
                currentWeapon.Fire(muzzle.position, muzzle.forward);
                nextFireTime = Time.time + currentWeapon.Cooldown;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                hasDoubleDamage = !hasDoubleDamage;
                RebuildChain();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                hasPiercing = !hasPiercing;
                RebuildChain();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                hasMultiShot = !hasMultiShot;
                RebuildChain();
            }
        }

        private void RebuildChain()
        {
            IWeapon chain = baseWeapon;
            if (hasDoubleDamage) chain = new DoubleDamageDecorator(chain);
            if (hasPiercing) chain = new PiercingDecorator(chain);
            if (hasMultiShot) chain = new MultiShotDecorator(chain);
            currentWeapon = chain;
            GameEvents.RaiseWeaponChainChanged(chain.DescribeChain());
        }
    }
}
