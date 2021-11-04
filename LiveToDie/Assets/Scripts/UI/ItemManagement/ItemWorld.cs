using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEngine.Experimental.Rendering.Universal;

public class ItemWorld : MonoBehaviour
{
    private Item item;
    private SpriteRenderer spriteRenderer;
    private Light2D light;

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
    }

    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        light.color = item.GetColor();
    }

}
