using UnityEngine;
using CoreBreach.Interfaces;

namespace CoreBreach.Enemies.Strategies
{
    public class NearestTarget : ITargetStrategy
    {
        private Transform[] candidates;

        public NearestTarget(params Transform[] possibleTargets)
        {
            candidates = possibleTargets;
        }

        public Transform GetTarget(Transform self)
        {
            Transform best = null;
            float bestSqr = float.MaxValue;
            for (int i = 0; i < candidates.Length; i++)
            {
                var c = candidates[i];
                if (c == null) continue;
                float d = (c.position - self.position).sqrMagnitude;
                if (d < bestSqr)
                {
                    bestSqr = d;
                    best = c;
                }
            }
            return best;
        }
    }
}
