using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

	public List<GameObject> Texts;
	public List<Button> Buttons;


	public void NewGame()
	{
		GameManager.instance.NewGame();
	}

	public void LoadGame()
	{
		GameManager.instance.LoadGame();
	}
	//TODO later on upgrade it(now only loads MAPS!)
	public void ListLoadableGames()
	{ 

		List<Button> _loadableGameButtons = new List<Button>();
		List<FileInfo> filteredInfos = new List<FileInfo>();

		var info = new DirectoryInfo(GameConstants.SavedGamesPath);
		var fileInfo = info.GetFiles();

		//Load File Names
		foreach(var file in fileInfo)
        {
            if (!file.Name.Contains(".meta"))
            {
				filteredInfos.Add(file);
			}
        }
		
		//Set Button Texts
		for (int i = 0; i < GameConstants.NumberOfSaveableMaps; i++)
		{
			var check = Texts[i].GetComponent<TextMeshProUGUI>();

            if (i < filteredInfos.Count)
            {
                check.text = filteredInfos[i].Name.Replace(".prefab", "");
            }
            else
            {
				check.text = "EMPTY";
				Buttons[i].GetComponent<Button>().interactable = false;
				Buttons[i].GetComponent<Image>().color = new Color32(169, 169, 169, 100);
			}
		}
	}
	public void Quit()
	{
		Application.Quit();    
	}
}
