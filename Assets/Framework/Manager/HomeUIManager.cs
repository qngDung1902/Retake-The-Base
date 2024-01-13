using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PopupSystem;


public class HomeUIManager : MonoSingleton<HomeUIManager>
{
    private void OnEnable()
    {
        PopupManager.Get.UpdateCanvasCamera();
        Global.SetScreen(SCREEN.HOME);
        Global.SetPhase(GAMEPHASE.UNPLAY);
    }
}
