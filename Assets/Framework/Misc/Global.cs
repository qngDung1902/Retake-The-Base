using UnityEngine;

public class Global : MonoBehaviour
{
    static GAMEPHASE phase;
    public static bool IsPhase(GAMEPHASE gamePhase) => phase == gamePhase;
    public static void SetPhase(GAMEPHASE gamePhase)
    {
        phase = gamePhase;
        // Debug.LogError($"[Game]: {phase}");
    }

    static SCREEN currentScreen;
    public static bool IsScreen(SCREEN screen) => (currentScreen == screen);
    public static void SetScreen(SCREEN screen)
    {
        currentScreen = screen;
    }

    public static int? CurrentChallengeLevel;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;
        Input.multiTouchEnabled = false;
    }

#if UNITY_EDITOR
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Q))
    //     {
    //         PopupDailyReward.Get.Open(BoosterType.Knife);
    //     }
    //     else if (Input.GetKeyDown(KeyCode.W))
    //     {
    //         PopupCongratulation.Get.Queue(PhoneShake.Types);
    //     }
    // }
#endif

}

public enum SCREEN
{
    LOADING, HOME, GAME, CHALLENGE
}

public enum GAMEPHASE
{
    UNPLAY,
    PLAYING,
    PAUSE,
    LOSE,
    WIN
}
