using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();

        AddItem(new Item() { amount = 1, itemType = Item.ItemType.Weapon });
        AddItem(new Item() { amount = 1, itemType = Item.ItemType.HealtPotion });
        AddItem(new Item() { amount = 1, itemType = Item.ItemType.ManaPotion });
        AddItem(new Item() { amount = 1, itemType = Item.ItemType.ManaPotion });

        Debug.Log("Inventory");
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    public List<Item> GetItems()
    {
        return itemList;
    }
}
