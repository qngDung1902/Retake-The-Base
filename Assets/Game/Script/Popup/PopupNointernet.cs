using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PopupSystem;

public class PopupNoInternet : SingletonPopup<PopupNoInternet>
{
    [SerializeField] private Button buttonConfirm;

    InternetDetector internetDetector;
    public void Open(InternetDetector detector)
    {
        buttonConfirm.onClick.AddListener(OnBtnOkClicked);
        internetDetector = detector;
        base.Show();
    }

    public void Close()
    {
        buttonConfirm.onClick.RemoveListener(OnBtnOkClicked);
        internetDetector.IsShowing = false;
        base.Hide();
    }

    public void OnBtnOkClicked()
    {
        try
        {
#if UNITY_ANDROID
            if (!(Application.internetReachability == NetworkReachability.NotReachable))
            {
                Close();
            }
            else
            {
                using (var unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                using (AndroidJavaObject currentActivityObject = unityClass.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    using (var intentObject = new AndroidJavaObject("android.content.Intent", "android.settings.WIFI_SETTINGS"))
                    {
                        currentActivityObject.Call("startActivity", intentObject);
                    }
                }
            }
#elif UNITY_IOS
        Close();
#endif
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
