using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private Animator animator;
    private string currentState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnimation(string newState)
    {
        if (currentState == newState || !animator) return;

        animator.SetTrigger(newState);
        currentState = newState;
    }
}
