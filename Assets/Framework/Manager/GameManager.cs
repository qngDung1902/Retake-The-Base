using System;
using PopupSystem;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    int money;
    public int Money
    {
        get => money;
        set
        {
            money = value;
            GameUIManager.Get.UpdateMoney();
        }
    }

    public bool EnoughMoney(int value) => Money >= value;
    public void Pay(int value, Action onDone)
    {
        if (Money >= value)
        {
            // Debug.Log($"{Money} - {value}");
            Money -= value;
            onDone?.Invoke();
        }
    }

    private void Awake()
    {
        Money = 0;
    }
}
