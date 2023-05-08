using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance => instance;

    protected virtual void Awake()
    {
        if (instance != null && gameObject != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = (T)this;
        }

        if (!gameObject.transform.parent) DontDestroyOnLoad(gameObject);
    }

}
