using UnityEngine;
using CoreBreach.Interfaces;

namespace CoreBreach.Enemies.Strategies
{
    public class AlwaysCoreTarget : ITargetStrategy
    {
        private Transform coreTransform;

        public AlwaysCoreTarget(Transform core)
        {
            coreTransform = core;
        }

        public Transform GetTarget(Transform self)
        {
            return coreTransform;
        }
    }
}
