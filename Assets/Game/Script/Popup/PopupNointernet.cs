using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PopupSystem;

public class PopupNointernet : SingletonPopup<PopupNointernet>
{
    // [SerializeField] private Button btnOk;
    // public bool isShow = false;

    public override void Awake()
    {
        base.Awake();
    }

    // void Update()
    // {
    //     if (isShow)
    //     {
    //         if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork ||
    //             Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
    //         {
    //             // Close();
    //         }
    //     }

    //     if (Application.internetReachability == NetworkReachability.NotReachable && !isShow)
    //     {
    //         Show();
    //     }
    // }

    // public void Show(Action onShowComplete = null)
    // {
    //     if (isShow) { return; }
    //     isShow = true;

    //     Show(onShowComplete, canCloseWithOverlay: true);
    // }

    // public void Hide(Action oncloseComplete = null)
    // {
    //     if (!isShow) { return; }
    //     isShow = false;

    //     base.Hide(oncloseComplete);
    // }

    private void OnBtnOkClicked()
    {
        try
        {
#if UNITY_ANDROID
            using (var unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (AndroidJavaObject currentActivityObject = unityClass.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                using (var intentObject = new AndroidJavaObject("android.content.Intent", "android.settings.WIFI_SETTINGS"))
                {
                    currentActivityObject.Call("startActivity", intentObject);
                }
            }
#elif UNITY_IOS
        // OnHide();
#endif
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
