using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList;

    public event EventHandler OnItemListChanged;
    private Action<Item> iseItemAction;

    public Inventory(Action<Item> useItemFnc)
    {
        iseItemAction = useItemFnc;
        itemList = new List<Item>();

        Debug.Log("Inventory");
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            foreach (Item currentItem in itemList)
            {
                if (currentItem.itemType == item.itemType)
                {
                    currentItem.amount += item.amount;
                    OnItemListChanged?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        itemList.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItems()
    {
        return itemList;
    }

    public void RemoveItem(Item item)
    {
        Item modifiedItem = null;

        if (item.IsStackable())
        {
            foreach (Item currentItem in itemList)
            {
                if (currentItem.itemType == item.itemType)
                {
                    currentItem.amount -= item.amount;
                    modifiedItem = currentItem;
                }
            }
            if (modifiedItem != null && modifiedItem.amount <= 0)
            {
                itemList.Remove(item);
            }
        }
        else
        {
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    internal void UseItem(Item item)
    {
        iseItemAction(item);
    }
}
