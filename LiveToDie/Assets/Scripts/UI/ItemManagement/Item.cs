using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item 
{
    public enum ItemType
    {
        Weapon,
        Coin,
        HealtPotion,
        ManaPotion
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite() 
    {
        //Probs unsafe
        switch (itemType)
        {
            default:
            case ItemType.Weapon:
                return ItemIcons.Instance.weaponSprite;
            case ItemType.Coin:
                return ItemIcons.Instance.coinSprite;
            case ItemType.HealtPotion:
                return ItemIcons.Instance.healtPotionSprite;
            case ItemType.ManaPotion:
                return ItemIcons.Instance.manaPotionSprite;
        }

    }

    public Color GetColor()
    {
        switch (itemType)
        {
            case ItemType.Weapon:
                return new Color(0.8113208f, 0.746262f, 0.746262f);
            case ItemType.Coin:
                return new Color(1, 1, 0);
            case ItemType.HealtPotion:
                return new Color(1, 0, 0);
            case ItemType.ManaPotion:
                return new Color(0, 0, 1);
            default:
                return new Color(0, 0, 0);
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            case ItemType.Coin:
            case ItemType.HealtPotion:
            case ItemType.ManaPotion:
                return true;
            case ItemType.Weapon:
                return false;
            default:
                return false;
        }
    }
}
