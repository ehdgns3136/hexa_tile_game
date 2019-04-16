using UnityEngine;

namespace Resources.Scripts
{
    /// http://wiki.unity3d.com/index.php/Singleton
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static object _mutex = new object();
        private static bool applicationIsQuitting = false;

        public static T Instance
        {
            get
            {
                if (applicationIsQuitting)
                {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                                     "' already destroyed on application quit." +
                                     " Won't create again - returning null.");
                    return null;
                }

                lock(_mutex)
                {
                    if (_instance == null)
                    {
                        var founds = FindObjectsOfType(typeof(T));
                        if (founds.Length > 1)
                        {
                            Debug.LogError("[Singleton] Singlton '" + typeof(T) +
                                           "' should never be more than 1!");
                            return null;
                        }
                        else if (founds.Length > 0)
                        {
                            _instance = (T)founds[0];

                            DontDestroyOnLoad(_instance.gameObject);
                        }
                        else
                        {
                            GameObject singleton = new GameObject();
                            _instance = singleton.AddComponent<T>();
                            singleton.name = "(Singleton) " + typeof(T).ToString();

                            DontDestroyOnLoad(singleton);
                        }
                    }

                    return _instance;
                }
            }
        }

        protected virtual void OnDestroy()
        {
            applicationIsQuitting = true;
        }
    }

}