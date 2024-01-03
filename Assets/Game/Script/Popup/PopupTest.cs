using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PopupSystem;

public class PopupTest : SingletonPopup<PopupTest>
{
    public void Open()
    {
        base.Show(true);
    }
}
