﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions.Must;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    public static bool IsInitialized { get; private set; }
    private static T m_Instance = null;
    public static T Get
    {
        get
        {
            // Instance requiered for the first time, we look for it
            if (m_Instance == null)
            {
                m_Instance = GameObject.FindObjectOfType(typeof(T)) as T;

                // Object not found, we create a temporary one
                if (m_Instance == null)
                {
                    Debug.LogWarning("No instance of " + typeof(T).ToString() + ", a temporary one is created.");

                    m_Instance = new GameObject("Temp Instance of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();

                    // Problem during the creation, this should not happen
                    if (m_Instance == null)
                    {
                        Debug.LogError("Problem during the creation of " + typeof(T).ToString());
                    }
                }

                m_Instance.Initiate();
            }
            return m_Instance;
        }
    }

    private void Awake()
    {
        // if (m_Instance == null)
        // {
        m_Instance = this as T;
        m_Instance.Initiate();
        // }
        // else if (m_Instance != this)
        // {
        //     Debug.LogError("Another instance of " + GetType() + " is already exist! Destroying self...");
        //     Destroy(this.gameObject);
        //     return;
        // }
    }


    /// <summary>
    /// This function is called when the instance is used the first time
    /// Put all the initializations you need here, as you would do in Awake
    /// </summary>
    protected virtual void Initiate() { IsInitialized = true; }

    /// Make sure the instance isn't referenced anymore when the user quit, just in case.
    private void OnApplicationQuit()
    {
        IsInitialized = false;
        m_Instance = null;
    }
}