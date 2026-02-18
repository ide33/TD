using UnityEngine;
using System.Collections;
using System;
using System.Runtime.CompilerServices;

public class DelaySystem : MonoBehaviour
{
    public static DelaySystem Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public Coroutine Run(float delay, Action action)
    {
        return StartCoroutine(DelayCoroutine(delay, action));
    }

    public IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    private IEnumerator DelayCoroutine(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
}
