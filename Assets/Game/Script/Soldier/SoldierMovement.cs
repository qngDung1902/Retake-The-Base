using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterAnimator))]
public class SoldierMovement : MonoBehaviour
{

    NavMeshAgent Agent;
    CharacterAnimator Animator;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<CharacterAnimator>();
    }

    void Start()
    {
        Animator.SetAnimation(ANIMATION.WALK);
    }

    private void Update()
    {
        Agent.transform.LookAt(Agent.transform.position, Vector3.up);
        Agent.destination = new Vector3(2, 0, 8);
    }

}
