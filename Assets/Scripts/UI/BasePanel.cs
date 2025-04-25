using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel<T> : MonoBehaviour where T : class
{
    private static T _instance;

    public static T Instance => _instance;

    protected void Awake()
    {
        _instance = this as T;
    }

    void Start()
    {
        Init();
    }

    protected abstract void Init();

    public virtual void ShowMe()
    {
        gameObject.SetActive(true);
    }

    public virtual void HideMe()
    {
        gameObject.SetActive(false);
    }
}