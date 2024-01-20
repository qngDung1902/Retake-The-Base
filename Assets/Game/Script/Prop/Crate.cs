using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Crate : MonoBehaviour/* , IInteractable */
{
    public CanvasGroup CanvasGroup;
    public Slider Progress;
    public SphereCollider Collider;

    // private void Awake()
    // {
    //     CanvasGroup.alpha = 0f;
    // }

    Tween tween1, tween2;
    // bool opened;
    // bool isOpen;
    // public void Interact(Unit unit)
    // {
    //     if (!isOpen)
    //     {
    //         Open();
    //     }
    // }

    // public void StopInteract()
    // {
    //     if (isOpen)
    //     {
    //         Stop();
    //     }
    // }

    // void Open()
    // {
    //     isOpen = true;
    // }

    // void Stop()
    // {
    //     isOpen = false;
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnableInteractableButton(true);
            Progress.value = 0f;
            tween2 = Progress.DOValue(Progress.maxValue, 2f).SetEase(Ease.Linear).OnComplete(Open);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnableInteractableButton(false);
            tween2?.Kill();
        }
    }

    void Open()
    {
        // opened = true;
        Debug.Log($"[{name}] Open!");
        Collider.enabled = false;
        EnableInteractableButton(false);
        GameManager.Get.Money += Random.Range(50, 200);
    }

    void EnableInteractableButton(bool value)
    {
        tween1?.Kill();
        tween1 = CanvasGroup.DOFade(value ? 1f : 0, 0.2f);
    }
}
