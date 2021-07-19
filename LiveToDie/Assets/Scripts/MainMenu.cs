using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    public void NewGame()
    {
        GameManager.instance.LoadGame();
    }
    public void Quit()
    {
        Application.Quit();    
    }
}
