using UnityEngine;

namespace CoreBreach.Interfaces
{
    public interface ITargetStrategy
    {
        Transform GetTarget(Transform self);
    }
}
