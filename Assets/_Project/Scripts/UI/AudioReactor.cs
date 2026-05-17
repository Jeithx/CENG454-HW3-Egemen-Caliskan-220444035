using UnityEngine;
using CoreBreach.Core;

namespace CoreBreach.UI
{
    public class AudioReactor : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip coreHitClip;
        [SerializeField] private AudioClip gameOverClip;

        private void OnEnable()
        {
            GameEvents.OnCoreDamaged += HandleCoreDamaged;
            GameEvents.OnGameOver += HandleGameOver;
        }

        private void OnDisable()
        {
            GameEvents.OnCoreDamaged -= HandleCoreDamaged;
            GameEvents.OnGameOver -= HandleGameOver;
        }

        private void HandleCoreDamaged(int current, int max)
        {
            if (audioSource != null && coreHitClip != null)
            {
                audioSource.PlayOneShot(coreHitClip);
            }
            Debug.Log($"[Audio] Core hit at {current}/{max}");
        }

        private void HandleGameOver()
        {
            if (audioSource != null && gameOverClip != null)
            {
                audioSource.PlayOneShot(gameOverClip);
            }
            Debug.Log("[Audio] Game over sting");
        }
    }
}
