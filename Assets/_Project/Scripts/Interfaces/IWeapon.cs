using UnityEngine;

namespace CoreBreach.Interfaces
{
    public interface IWeapon
    {
        int Damage { get; }
        float Cooldown { get; }
        void Fire(Vector3 origin, Vector3 direction, int damageOverride = -1);
        string DescribeChain();
    }
}
