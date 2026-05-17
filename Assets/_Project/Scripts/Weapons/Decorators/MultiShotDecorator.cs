using UnityEngine;
using CoreBreach.Interfaces;

namespace CoreBreach.Weapons.Decorators
{
    public class MultiShotDecorator : WeaponDecorator
    {
        private int extraShots;
        private float spreadAngle;

        public MultiShotDecorator(IWeapon inner, int extra = 2, float spread = 12f) : base(inner)
        {
            extraShots = extra;
            spreadAngle = spread;
        }

        public override void Fire(Vector3 origin, Vector3 direction, int damageOverride = -1)
        {
            // Center shot
            inner.Fire(origin, direction, damageOverride);

            // Spread shots: alternate left and right of center
            for (int i = 1; i <= extraShots; i++)
            {
                float side = (i % 2 == 0) ? -1f : 1f;
                float angle = side * spreadAngle * ((i + 1) / 2);
                Vector3 rotated = Quaternion.AngleAxis(angle, Vector3.up) * direction;
                inner.Fire(origin, rotated, damageOverride);
            }
        }

        public override string DescribeChain()
        {
            return $"MultiShot(x{extraShots + 1}, " + inner.DescribeChain() + ")";
        }
    }
}
