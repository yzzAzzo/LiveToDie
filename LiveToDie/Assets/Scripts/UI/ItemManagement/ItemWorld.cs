using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;
using System;

public class ItemWorld : MonoBehaviour
{
    private Item item;
    private SpriteRenderer spriteRenderer;
    private Light2D light;
    private TMP_Text text;

    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemIcons.Instance.WorldItemPrefab, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        light = gameObject.transform.Find("Light").GetComponent<Light2D>();
        text = gameObject.transform.Find("Amount").GetComponent<TMP_Text>();
    }

    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        light.color = item.GetColor();
        if (item.amount == 1)
        {
            text.SetText("");
        }
        else
        {
            text.SetText(item.amount.ToString());
        }
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public static ItemWorld DropItem(Vector3 position, Item item)
    {
        Vector3 randomDirection = new Vector3(UnityEngine.Random.Range(0.3f, -0.3f), UnityEngine.Random.Range(0.3f, -0.3f), 0);

        ItemWorld itemWorld = SpawnItemWorld(position + randomDirection, item);

        itemWorld.GetComponent<Rigidbody2D>().AddForce(randomDirection, ForceMode2D.Impulse);

        return itemWorld;

    }
}
