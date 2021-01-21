using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    public void NewGame()
    {
        Loader.Load(Loader.Scene.Game);
    }
    public void Quit()
    {
        Application.Quit();    
    }
}
