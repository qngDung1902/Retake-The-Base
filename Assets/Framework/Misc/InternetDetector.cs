using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetDetector : MonoBehaviour
{
    public bool IsShowing;
    private void Update()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable && !IsShowing)
        {
            IsShowing = true;
            PopupNoInternet.Get.Open(this);
        }
    }
}
