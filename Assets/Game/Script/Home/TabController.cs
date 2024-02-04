using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabController : MonoSingleton<TabController>
{
    [SerializeField] Tab InitTab;

    Tab lastSelect;

    private void Awake()
    {
        InitTab.Select(true);
    }

    public void Select(Tab tab)
    {
        lastSelect?.Select(false);
        lastSelect = tab;
    }
}
