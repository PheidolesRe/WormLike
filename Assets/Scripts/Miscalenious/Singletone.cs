using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singletone<T> : MonoBehaviour where T : Singletone<T>
{
    private static T instance;

    public static T Instance { get { return instance; } }

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
    }
}
