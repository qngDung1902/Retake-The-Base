using System.Collections;
using PopupSystem;

public class GameUIManager : MonoSingleton<GameUIManager>
{
    private void OnEnable()
    {
        PopupManager.Get.UpdateCanvasCamera();
        Global.SetScreen(SCREEN.GAME);
        Global.SetPhase(GAMEPHASE.PLAYING);
    }
}
