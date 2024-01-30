using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private Animator animator;
    private string currentAnimation = string.Empty;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnimation(string animation)
    {
        if (currentAnimation == animation || !animator) return;

        if (currentAnimation != string.Empty)
        {
            animator.ResetTrigger(currentAnimation);
        }
        animator.SetTrigger(animation);
        currentAnimation = animation;
    }

    public void SetRandomAnimation(string animation)
    {
        if (currentAnimation == animation || !animator) return;
        if (currentAnimation != string.Empty)
        {
            animator.ResetTrigger(currentAnimation);
        }

        SetAnimatorSpeed(1f);
        animator.SetFloat(animation, Random.Range(0, 2));
        currentAnimation = animation;
    }

    public void SetAnimatorSpeed(float speed)
    {
        animator.speed = speed;
    }
}
