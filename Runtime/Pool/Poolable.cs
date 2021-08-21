using UnityEngine;
using System.Collections;

namespace BennyKok.DamageDisplay
{
    public class Poolable : MonoBehaviour
    {
        [System.NonSerialized]
        public bool used;

        private float lifetime;

        public void ResetToPool(float lifetime = 0)
        {
            this.lifetime = lifetime;
            if (lifetime > 0)
            {
                StopCoroutine("DelayReset");
                StartCoroutine("DelayReset");
            }
            else
            {
                ResetToPoolInstant();
            }
        }

        IEnumerator DelayReset()
        {
            yield return new WaitForSeconds(lifetime);
            ResetToPoolInstant();
        }

        protected virtual void ResetToPoolInstant()
        {
            used = false;
            gameObject.SetActive(false);
        }

    }
}