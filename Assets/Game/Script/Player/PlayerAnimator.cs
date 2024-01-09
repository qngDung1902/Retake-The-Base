using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private string currentState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnimation(string newState)
    {
        if (currentState == newState) return;

        animator.SetTrigger(newState);
        currentState = newState;
    }
}
