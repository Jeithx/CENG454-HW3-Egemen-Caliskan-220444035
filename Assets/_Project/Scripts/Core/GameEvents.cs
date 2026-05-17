using System;

namespace CoreBreach.Core
{
    public static class GameEvents
    {
        public static event Action<int, int> OnCoreDamaged;
        public static event Action OnCoreDestroyed;
        public static event Action<int> OnEnemyKilled;
        public static event Action<int> OnWaveCleared;
        public static event Action OnGameOver;
        public static event Action OnGameWon;

        public static void RaiseCoreDamaged(int currentHealth, int maxHealth)
        {
            OnCoreDamaged?.Invoke(currentHealth, maxHealth);
        }

        public static void RaiseCoreDestroyed()
        {
            OnCoreDestroyed?.Invoke();
        }

        public static void RaiseEnemyKilled(int scoreAwarded)
        {
            OnEnemyKilled?.Invoke(scoreAwarded);
        }

        public static void RaiseWaveCleared(int waveNumber)
        {
            OnWaveCleared?.Invoke(waveNumber);
        }

        public static void RaiseGameOver()
        {
            OnGameOver?.Invoke();
        }

        public static void RaiseGameWon()
        {
            OnGameWon?.Invoke();
        }
    }
}
