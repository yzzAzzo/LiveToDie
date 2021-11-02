using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
