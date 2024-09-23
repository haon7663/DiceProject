using UnityEngine;

public class SingletonDontDestroyOnLoad<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _inst;
    public static bool HasInstance => _inst != null;
    public static T TryGetInstance() => HasInstance ? _inst : null;
    public static T Current => _inst;
    
    public static T Inst
    {
        get
        {
            if (_inst) return _inst;
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            _inst = (T)FindObjectOfType(typeof(T));

            if (_inst) return _inst;
            var obj = new GameObject(typeof(T).Name, typeof(T));
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            _inst = obj.GetComponent<T>();
            
            return _inst;
        }
    }
    
    protected virtual void Awake()
    {
        var obj = FindObjectsOfType<SingletonDontDestroyOnLoad<T>>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}