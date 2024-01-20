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
            money += value;
            GameUIManager.Get.UpdateMoney(money);
        }
    }

    public bool EnoughMoney(int value) => Money >= value;

    private void Awake()
    {
        Money = 0;
    }
}
