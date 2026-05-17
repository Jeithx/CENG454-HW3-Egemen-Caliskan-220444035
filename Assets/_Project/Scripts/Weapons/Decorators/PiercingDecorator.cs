using UnityEngine;
using CoreBreach.Interfaces;

namespace CoreBreach.Weapons.Decorators
{
    public class PiercingDecorator : WeaponDecorator
    {
        private BaseWeapon root;

        public PiercingDecorator(IWeapon inner) : base(inner)
        {
            IWeapon current = inner;
            while (current is WeaponDecorator wd)
            {
                current = wd.GetInner();
            }
            root = current as BaseWeapon;
        }

        public override void Fire(Vector3 origin, Vector3 direction, int damageOverride = -1)
        {
            if (root != null) root.EnableNextShotPiercing();
            inner.Fire(origin, direction, damageOverride);
        }

        public override string DescribeChain()
        {
            return "Piercing(" + inner.DescribeChain() + ")";
        }
    }
}
