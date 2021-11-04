using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIcons : MonoBehaviour
{

    public Sprite weaponSprite;
    public Sprite coinSprite;
    public Sprite healtPotionSprite;
    public Sprite manaPotionSprite;

    public static ItemIcons Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public Transform WorldItemPrefab;
}
