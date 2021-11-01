using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsToGUI : MonoBehaviour
{

    public Player player;

    private void SetOrbs()
    {
        if (gameObject.name.Contains("health"))
        {
            gameObject.transform.GetComponent<Image>().fillAmount = ((float)player.currentHealth / (float)player.health);
        }
        else
        {
            gameObject.transform.GetComponent<Image>().fillAmount = ((float)player.currentMana / (float)player.mana);

        }
    }

    // Update is called once per frame
    void Update()
    {
        SetOrbs();
    }
}
