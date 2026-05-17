using UnityEngine;
using System.Collections.Generic;
using CoreBreach.Interfaces;

namespace CoreBreach.Pooling
{
    public class PoolManager<T> where T : Component, IPoolable
    {
        private T prefab;
        private Transform parent;
        private Queue<T> inactive = new Queue<T>();
        private List<T> all = new List<T>();
        private int initialSize;

        public PoolManager(T prefab, int initialSize, Transform parent = null)
        {
            this.prefab = prefab;
            this.initialSize = initialSize;
            this.parent = parent;

            for (int i = 0; i < initialSize; i++)
            {
                Grow();
            }
        }

        private T Grow()
        {
            T instance = Object.Instantiate(prefab, parent);
            instance.gameObject.SetActive(false);
            inactive.Enqueue(instance);
            all.Add(instance);
            return instance;
        }

        public T Get(Vector3 position, Quaternion rotation)
        {
            if (inactive.Count == 0)
            {
                Grow();
            }

            T item = inactive.Dequeue();
            item.transform.SetPositionAndRotation(position, rotation);
            item.gameObject.SetActive(true);
            item.OnSpawn();
            return item;
        }

        public void Return(T item)
        {
            if (item == null) return;
            if (!item.gameObject.activeSelf) return;

            item.OnDespawn();
            item.gameObject.SetActive(false);
            inactive.Enqueue(item);
        }

        public void ReturnAll()
        {
            foreach (var item in all)
            {
                if (item.gameObject.activeSelf)
                {
                    Return(item);
                }
            }
        }

        public int CountInactive => inactive.Count;
        public int CountTotal => all.Count;
    }
}
