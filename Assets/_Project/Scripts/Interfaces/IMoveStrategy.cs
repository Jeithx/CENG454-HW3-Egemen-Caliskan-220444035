using UnityEngine;

namespace CoreBreach.Interfaces
{
    public interface IMoveStrategy
    {
        void Move(Transform self, Transform target, float speed);
    }
}
