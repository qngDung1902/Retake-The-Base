using System.Collections;
using PopupSystem;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoSingleton<GameUIManager>
{
    public TMP_Text MoneyText;
    private void OnEnable()
    {
        PopupManager.Get.UpdateCanvasCamera();
        Global.SetScreen(SCREEN.GAME);
        Global.SetPhase(GAMEPHASE.PLAYING);
    }

    public void UpdateMoney(int value)
    {
        MoneyText.text = $"{value}";
    }
}
