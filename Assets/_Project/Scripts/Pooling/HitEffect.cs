using UnityEngine;
using CoreBreach.Interfaces;

namespace CoreBreach.Pooling
{
    public class HitEffect : MonoBehaviour, IPoolable
    {
        [SerializeField] private float duration = 0.4f;

        private float deathTime;
        private bool isActive;

        public System.Action<HitEffect> ReturnAction;

        public void OnSpawn()
        {
            deathTime = Time.time + duration;
            isActive = true;
        }

        public void OnDespawn()
        {
            isActive = false;
            ReturnAction = null;
        }

        private void Update()
        {
            if (isActive && Time.time >= deathTime)
            {
                ReturnAction?.Invoke(this);
            }
        }
    }
}
