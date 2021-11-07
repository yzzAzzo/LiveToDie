using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    public Player player;


    private void Awake()
    {
        //The whole Inventory system is hardly dependent on Script Execution Order(This could ruin everything if runs earlier)
        ChangeVisibility();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ChangeVisibility();
        }
    }

    private void ChangeVisibility()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            child.gameObject.SetActive(!child.gameObject.activeSelf);
        }

        var image = gameObject.GetComponent<Image>();
        image.enabled = !image.enabled;
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        UpdateInventoryItems();
        inventory.OnItemListChanged += Inventory_OnItemListChanged;

    }

    private void Inventory_OnItemListChanged(object sender, EventArgs e)
    {
        UpdateInventoryItems();
    }

    public void UpdateInventoryItems()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");

        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate)
            {
                continue;
            }

            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 105f;

        foreach (Item item in inventory.GetItems())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();

            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);

            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
                                                            {
                                                                inventory.UseItem(item);
                                                                inventory.RemoveItem(item);

                                                            };

            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () => 
                                                            {
                                                                //To have different references
                                                                Item droppedItem = new Item { amount = item.amount, itemType = item.itemType };
                                                                inventory.RemoveItem(item);
                                                                ItemWorld.DropItem(player.transform.position, droppedItem);

                                                            };

            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TMP_Text text = itemSlotRectTransform.Find("Amount").GetComponent<TMP_Text>();
            if (item.amount == 1)
            {
               text.SetText("");
            }
            else
            {
                text.SetText(item.amount.ToString());
            }


            x++;
            if (x >= 4)
            {
                x = 0;
                y--;
            }
        }
    }
}
