using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tab : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] Sprite[] backgroundSprites;
    [SerializeField] GameObject model, weapon;

    public void Select(bool value)
    {
        background.sprite = value ? backgroundSprites[1] : backgroundSprites[0];
        model.SetActive(value);
        weapon.SetActive(value);
        if (value)
        {
            TabController.Get.Select(this);
        }
    }
}
