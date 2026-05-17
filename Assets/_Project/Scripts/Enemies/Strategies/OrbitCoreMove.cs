using UnityEngine;
using CoreBreach.Interfaces;

namespace CoreBreach.Enemies.Strategies
{
    public class OrbitCoreMove : IMoveStrategy
    {
        private float startRadius;
        private float currentRadius;
        private float orbitSpeed;
        private float shrinkPerSecond;
        private float minRadius;

        public OrbitCoreMove(float radius = 7f,
                             float orbit = 2f,
                             float shrink = 0.9f,
                             float minR = 0.5f)
        {
            startRadius = radius;
            currentRadius = radius;
            orbitSpeed = orbit;
            shrinkPerSecond = shrink;
            minRadius = minR;
        }

        public void Move(Transform self, Transform target, float speed)
        {
            if (target == null) return;

            Vector3 toTarget = target.position - self.position;
            toTarget.y = 0f;
            float distance = toTarget.magnitude;
            if (distance < 0.001f) return;

            Vector3 inward = toTarget / distance;
            Vector3 tangent = Vector3.Cross(Vector3.up, inward);

            // Shrink the orbit over time so enemies eventually reach
            // the core. This guarantees the wave is winnable.
            currentRadius = Mathf.Max(
                minRadius,
                currentRadius - shrinkPerSecond * Time.deltaTime);

            // Radial pull: positive when outside currentRadius,
            // negative when inside.
            float radial = (distance - currentRadius);
            Vector3 step = (inward * radial * speed
                           + tangent * orbitSpeed * speed)
                           * Time.deltaTime;
            self.position += step;

            Vector3 look = (inward + tangent * 0.3f).normalized;
            if (look.sqrMagnitude > 0.001f)
                self.rotation = Quaternion.Slerp(
                    self.rotation,
                    Quaternion.LookRotation(look),
                    10f * Time.deltaTime);
        }
    }
}
