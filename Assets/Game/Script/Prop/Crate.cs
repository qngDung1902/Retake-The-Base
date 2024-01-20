using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Crate : MonoBehaviour, IInteractable
{
    public Transform t;
    bool isOpen;

    // private void Awake()
    // {
    //     t.localPosition = Vector2.zero;
    // }

    public void Interact(Unit unit)
    {
        if (!isOpen)
        {
            Open();
        }
    }

    public void StopInteract()
    {
        if (isOpen)
        {
            Stop();
        }
    }

    void Open()
    {
        isOpen = true;
    }

    void Stop()
    {
        isOpen = false;
    }
}
