using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CoreBreach.Core;

namespace CoreBreach.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private TMP_Text coreHealthText;
        [SerializeField] private TMP_Text gameStateText;
        [SerializeField] private Slider coreHealthBar;
        [SerializeField] private TMP_Text weaponChainText;

        private void OnEnable()
        {
            GameEvents.OnCoreDamaged += HandleCoreDamaged;
            GameEvents.OnGameOver += HandleGameOver;
            GameEvents.OnGameWon += HandleGameWon;
            GameEvents.OnWeaponChainChanged += HandleWeaponChainChanged;
        }

        private void OnDisable()
        {
            GameEvents.OnCoreDamaged -= HandleCoreDamaged;
            GameEvents.OnGameOver -= HandleGameOver;
            GameEvents.OnGameWon -= HandleGameWon;
            GameEvents.OnWeaponChainChanged -= HandleWeaponChainChanged;
        }

        private void Start()
        {
            if (gameStateText != null)
            {
                gameStateText.text = "";
            }

            if (weaponChainText != null)
            {
                weaponChainText.text = "Weapon: Base";
            }
        }

        private void HandleCoreDamaged(int current, int max)
        {
            if (coreHealthText != null)
            {
                coreHealthText.text = $"Core: {current} / {max}";
            }

            if (coreHealthBar != null)
            {
                coreHealthBar.maxValue = max;
                coreHealthBar.value = current;
            }
        }

        private void HandleGameOver()
        {
            if (gameStateText != null)
            {
                gameStateText.text = "DEFEAT";
            }
        }

        private void HandleGameWon()
        {
            if (gameStateText != null)
            {
                gameStateText.text = "VICTORY";
            }
        }

        private void HandleWeaponChainChanged(string description)
        {
            if (weaponChainText != null)
            {
                weaponChainText.text = "Weapon: " + description;
            }
        }
    }
}
