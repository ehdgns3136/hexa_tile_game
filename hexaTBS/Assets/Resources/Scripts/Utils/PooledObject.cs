using UnityEngine;

namespace Resources.Scripts
{
    [AddComponentMenu("SlotMaker/GameObject/Object Pool/Pooled Object")]
    public class PooledObject : MonoBehaviour
    {
        public ObjectPool pool;

        public virtual void ReturnToPool()
        {
            if (pool)
            {
                pool.AddObject(this);
            }
            else
            {
                Debug.LogWarning("PooledObject has not pool.");
                Destroy(gameObject);
            }
        }
    }
}