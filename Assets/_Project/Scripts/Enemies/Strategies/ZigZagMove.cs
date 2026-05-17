using UnityEngine;
using CoreBreach.Interfaces;

namespace CoreBreach.Enemies.Strategies
{
    public class ZigZagMove : IMoveStrategy
    {
        private float zigzagAmplitude;
        private float zigzagFrequency;
        private float phaseOffset;

        public ZigZagMove(float amplitude = 2f, float frequency = 3f)
        {
            zigzagAmplitude = amplitude;
            zigzagFrequency = frequency;
            phaseOffset = Random.value * Mathf.PI * 2f;
        }

        public void Move(Transform self, Transform target, float speed)
        {
            if (target == null) return;
            Vector3 toTarget = target.position - self.position;
            toTarget.y = 0f;
            Vector3 forward = toTarget.normalized;
            Vector3 right = Vector3.Cross(Vector3.up, forward);
            float wave = Mathf.Sin(Time.time * zigzagFrequency + phaseOffset) * zigzagAmplitude;
            Vector3 step = (forward * speed + right * wave) * Time.deltaTime;
            self.position += step;
            if (forward.sqrMagnitude > 0.001f)
                self.rotation = Quaternion.Slerp(self.rotation,
                    Quaternion.LookRotation(forward),
                    10f * Time.deltaTime);
        }
    }
}
