using UnityEngine;
using CoreBreach.Interfaces;

namespace CoreBreach.Weapons.Decorators
{
    public class DoubleDamageDecorator : WeaponDecorator
    {
        public DoubleDamageDecorator(IWeapon inner) : base(inner) { }

        public override int Damage => inner.Damage * 2;

        public override void Fire(Vector3 origin, Vector3 direction, int damageOverride = -1)
        {
            int effective = damageOverride == -1 ? Damage : damageOverride;
            inner.Fire(origin, direction, effective);
        }

        public override string DescribeChain()
        {
            return "DoubleDamage(" + inner.DescribeChain() + ")";
        }
    }
}
