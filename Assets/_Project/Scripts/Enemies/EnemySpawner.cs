using UnityEngine;
using System.Collections;
using CoreBreach.Pooling;
using CoreBreach.Interfaces;
using CoreBreach.Core;
using CoreBreach.Enemies.Strategies;

namespace CoreBreach.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        public enum MoveStyle { Straight, ZigZag, Orbit }
        public enum TargetStyle { AlwaysCore, NearestPlayerOrCore }

        [System.Serializable]
        public struct WaveConfig
        {
            public int enemyCount;
            public float spawnInterval;
            public MoveStyle moveStyle;
            public TargetStyle targetStyle;
        }

        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private Transform coreTransform;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private int initialPoolSize = 30;
        [SerializeField] private WaveConfig[] waves;
        [SerializeField] private float delayBetweenWaves = 3f;

        private PoolManager<Enemy> enemyPool;
        private int aliveEnemies = 0;
        private int wavesCompleted = 0;
        private bool encounterFinished = false;

        private void Awake()
        {
            enemyPool = new PoolManager<Enemy>(
                enemyPrefab, initialPoolSize, this.transform);
        }

        private void OnEnable()
        {
            GameEvents.OnEnemyKilled += HandleEnemyKilled;
        }

        private void OnDisable()
        {
            GameEvents.OnEnemyKilled -= HandleEnemyKilled;
        }

        private void Start()
        {
            StartCoroutine(RunWaves());
        }

        private IEnumerator RunWaves()
        {
            for (int i = 0; i < waves.Length; i++)
            {
                yield return StartCoroutine(SpawnWave(waves[i]));
                // Wait for ALL enemies of this wave to be cleared
                // before starting the next.
                while (aliveEnemies > 0)
                    yield return null;

                wavesCompleted++;
                GameEvents.RaiseWaveCleared(wavesCompleted);

                if (i < waves.Length - 1)
                    yield return new WaitForSeconds(delayBetweenWaves);
            }

            // All waves spawned and cleared. Confirm one more time.
            while (aliveEnemies > 0) yield return null;

            if (!encounterFinished)
            {
                encounterFinished = true;
                GameEvents.RaiseGameWon();
            }
        }

        private IEnumerator SpawnWave(WaveConfig config)
        {
            for (int i = 0; i < config.enemyCount; i++)
            {
                SpawnOne(config);
                yield return new WaitForSeconds(config.spawnInterval);
            }
        }

        private void SpawnOne(WaveConfig config)
        {
            Transform sp = spawnPoints[
                Random.Range(0, spawnPoints.Length)];
            Enemy e = enemyPool.Get(sp.position, Quaternion.identity);

            // Wrap the pool return so we ALWAYS decrement
            // aliveEnemies, even if a future code path returns
            // an enemy without raising OnEnemyKilled.
            e.ReturnAction = (en) =>
            {
                enemyPool.Return(en);
            };

            IMoveStrategy move = config.moveStyle switch
            {
                MoveStyle.Straight => new StraightLineMove(),
                MoveStyle.ZigZag => new ZigZagMove(),
                MoveStyle.Orbit => new OrbitCoreMove(),
                _ => new StraightLineMove()
            };

            ITargetStrategy target = config.targetStyle switch
            {
                TargetStyle.AlwaysCore =>
                    new AlwaysCoreTarget(coreTransform),
                TargetStyle.NearestPlayerOrCore =>
                    new NearestTarget(coreTransform, playerTransform),
                _ => new AlwaysCoreTarget(coreTransform)
            };

            e.Configure(move, target);
            aliveEnemies++;
        }

        private void HandleEnemyKilled(int score)
        {
            aliveEnemies--;
            if (aliveEnemies < 0) aliveEnemies = 0;
        }
    }
}
