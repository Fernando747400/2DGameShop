using System;
using UnityEngine;

public class BaseItemSO : ScriptableObject
{
    public int ID;
    public string Name;
    public Sprite Sprite;
    public BodyPartType BodyPartType;
    public int Price;
    public bool Purchased;

    public void LoadInfo()
    {
        if (!PlayerPrefs.HasKey(Name)) return;
        Purchased = Convert.ToBoolean(PlayerPrefs.GetInt(Name));
    }

    public void SaveInfo(bool purchased)
    {
        PlayerPrefs.SetInt(Name, purchased ? 1 : 0);
    }
}
