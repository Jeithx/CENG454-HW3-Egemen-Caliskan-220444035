using UnityEngine;

namespace CoreBreach.Interfaces
{
    public interface IWeapon
    {
        int Damage { get; }
        void Fire(Vector3 origin, Vector3 direction);
    }
}
