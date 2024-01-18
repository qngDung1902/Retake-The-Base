using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IInteractable>().Interact();
    }
}
