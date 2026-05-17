using UnityEngine;
using System.Collections;
using CoreBreach.Core;

namespace CoreBreach.UI
{
    public class VfxReactor : MonoBehaviour
    {
        [SerializeField] private Camera targetCamera;
        [SerializeField] private Color flashColor = new Color(1f, 0.2f, 0.2f, 1f);
        [SerializeField] private float flashDuration = 0.15f;

        private Color originalBackgroundColor;
        private Coroutine activeFlash;

        private void Awake()
        {
            if (targetCamera == null)
            {
                targetCamera = Camera.main;
            }
            
            if (targetCamera != null)
            {
                originalBackgroundColor = targetCamera.backgroundColor;
            }
        }

        private void OnEnable()
        {
            GameEvents.OnCoreDamaged += HandleCoreDamaged;
        }

        private void OnDisable()
        {
            GameEvents.OnCoreDamaged -= HandleCoreDamaged;
        }

        private void HandleCoreDamaged(int current, int max)
        {
            if (targetCamera == null) return;

            if (activeFlash != null)
            {
                StopCoroutine(activeFlash);
            }
            activeFlash = StartCoroutine(FlashRoutine());
        }

        private IEnumerator FlashRoutine()
        {
            targetCamera.backgroundColor = flashColor;
            yield return new WaitForSeconds(flashDuration);
            targetCamera.backgroundColor = originalBackgroundColor;
            activeFlash = null;
        }
    }
}
