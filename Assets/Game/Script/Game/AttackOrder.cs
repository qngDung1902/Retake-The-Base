using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AttackOrder : MonoBehaviour
{
    public Slider Progress;
    Tween tween;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tween = Progress.DOValue(Progress.maxValue, 1f).SetEase(Ease.Linear).OnComplete(Attack);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tween?.Kill();
            Progress.value = 0;
        }
    }

    void Attack()
    {
        Debug.Log($"[Game] Attack!");
    }
}
