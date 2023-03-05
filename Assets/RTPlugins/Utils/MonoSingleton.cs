using UnityEngine;

namespace RTPlugins.Utils
{
    [DisallowMultipleComponent]
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (ReferenceEquals(_instance, null))
                    _instance = FindObjectOfType<T>();
                return _instance;
            }
        }
    }
}
