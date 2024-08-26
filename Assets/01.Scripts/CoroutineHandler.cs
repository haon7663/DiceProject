using UnityEngine;
using System.Collections; 

public class CoroutineHandler : MonoBehaviour
{
    private IEnumerator _enumerator = null;
    private void Coroutine(IEnumerator coroutine)
    {
        _enumerator = coroutine;
        StartCoroutine(coroutine);        
    }
    
    private void Update()
    {
        if (_enumerator == null) return;
        if (_enumerator.Current == null)
        {
            Destroy(gameObject);
        }
    }
    
    public void Stop()
    {
        StopCoroutine(_enumerator.ToString());
        Destroy(gameObject);
    }

    public static CoroutineHandler Start_Coroutine(IEnumerator coroutine)
    {
        var obj = new GameObject("CoroutineHandler");
        var handler = obj.AddComponent<CoroutineHandler>();
        if (handler)
        {
            handler.Coroutine(coroutine);
        }
        return handler;
    }
}