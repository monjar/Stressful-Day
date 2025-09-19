using UnityEngine;

namespace Core
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        
       
        public virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this as T;
            }
        }
    }
    
    public class MonoImmortal<T> : MonoSingleton<T> where T : MonoBehaviour
    {
        public override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}