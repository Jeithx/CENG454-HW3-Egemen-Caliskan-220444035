using UnityEngine;
using CoreBreach.Interfaces;

namespace CoreBreach.Weapons.Decorators
{
    public abstract class WeaponDecorator : IWeapon
    {
        protected IWeapon inner;

        protected WeaponDecorator(IWeapon inner)
        {
            this.inner = inner;
        }

        public IWeapon GetInner()
        {
            return inner;
        }

        public virtual int Damage => inner.Damage;
        public virtual float Cooldown => inner.Cooldown;

        public virtual void Fire(Vector3 origin, Vector3 direction, int damageOverride = -1)
        {
            inner.Fire(origin, direction, damageOverride);
        }

        public abstract string DescribeChain();
    }
}
