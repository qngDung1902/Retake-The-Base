using System.Collections;
using System.Collections.Generic;
using PopupSystem;
using UnityEngine;

public class HomeUIManager : MonoSingleton<HomeUIManager>
{
    private void Awake()
    {
        PopupManager.Get.UpdateCanvasCamera();
    }
}
