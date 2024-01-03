using System.Collections;
using System.Collections.Generic;
using PopupSystem;
using UnityEngine;

public class GameUIManager : MonoSingleton<GameUIManager>
{
    private void Awake()
    {
        PopupManager.Get.UpdateCanvasCamera();
    }
}
