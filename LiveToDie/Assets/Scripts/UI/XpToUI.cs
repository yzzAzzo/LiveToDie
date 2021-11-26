using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XpToUI : MonoBehaviour
{
    public Player player;
    private void SetXp()
    {
        var imageComponent = gameObject.transform.GetComponent<Image>();
        imageComponent.fillAmount = ((float)player.Xp / (float)player.XpNeeded);
    }

    // Update is called once per frame
    void Update()
    {
        SetXp();
    }
}
