using System.Collections.Generic;
using UnityEngine;

namespace BennyKok.DamageDisplay
{
    public class PoolSystem<K, T> : Singleton<K> where K : MonoBehaviour where T : Poolable
    {
        [Header("Pool")]
        public GameObject prefab;
        public int initCount;

        private List<T> viewPool = new List<T>();

        protected override void Awake()
        {
            base.Awake();

            for (int i = 0; i < initCount; i++)
            {
                viewPool.Add(GetNewItem());
            }
        }

        public T GetNewItem()
        {
            var o = Instantiate(prefab);
            o.gameObject.SetActive(false);
            o.transform.SetParent(transform);
            o.transform.localScale = Vector3.one;
            var view = o.GetComponent<T>();
            view.used = false;
            return view;
        }

        public T GetAvailableItem()
        {
            var free = viewPool.Find(x => !x.used);
            if (!free)
            {
                free = GetNewItem();
                viewPool.Add(free);
            }

            free.used = true;
            free.gameObject.SetActive(true);

            return free;
        }
    }
}