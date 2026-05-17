using UnityEngine;
using CoreBreach.Interfaces;

namespace CoreBreach.Enemies.Strategies
{
    public class StraightLineMove : IMoveStrategy
    {
        public void Move(Transform self, Transform target, float speed)
        {
            if (target == null) return;
            Vector3 dir = (target.position - self.position);
            dir.y = 0f;
            dir.Normalize();
            self.position += dir * speed * Time.deltaTime;
            // Face movement direction so the enemy looks "alive"
            if (dir.sqrMagnitude > 0.001f)
                self.rotation = Quaternion.Slerp(self.rotation,
                    Quaternion.LookRotation(dir),
                    10f * Time.deltaTime);
        }
    }
}
