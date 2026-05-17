using UnityEngine;

namespace CoreBreach.Core
{
    public class GameStateManager : MonoBehaviour
    {
        private bool isGameEnded = false;

        private void OnEnable()
        {
            GameEvents.OnGameOver += HandleGameOver;
            GameEvents.OnGameWon += HandleGameWon;
        }

        private void OnDisable()
        {
            GameEvents.OnGameOver -= HandleGameOver;
            GameEvents.OnGameWon -= HandleGameWon;
        }

        private void HandleGameOver()
        {
            if (isGameEnded) return;
            isGameEnded = true;
            Time.timeScale = 0f;
            Debug.Log("[GameState] Game Over - Core destroyed");
        }

        private void HandleGameWon()
        {
            if (isGameEnded) return;
            isGameEnded = true;
            Time.timeScale = 0f;
            Debug.Log("[GameState] Victory");
        }
    }
}
