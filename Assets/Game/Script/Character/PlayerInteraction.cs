using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    Player Player;
    private void Awake()
    {
        Player = GetComponent<Player>();
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     // Player.ChangeState();
    //     Player.ChangeState(Player.PickUpState);
    //     other.GetComponent<IInteractable>().Interact(Player);
    // }

    // private void OnTriggerExit(Collider other)
    // {

    // }
}
