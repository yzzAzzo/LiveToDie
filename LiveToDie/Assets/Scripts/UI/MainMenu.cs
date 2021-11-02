using System;
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

	public void LoadGame(int buttonNumber)
	{
		NavigationStatics.mapInUse = Texts[buttonNumber].GetComponent<TextMeshProUGUI>().text;
		DestroyOtherMaps(NavigationStatics.mapInUse);

		GameManager.instance.LoadGame();
	}

    private void DestroyOtherMaps(string mapInUse)
    {

		//This Avoids Multiple Maps Being Loaded At The Same Time
		foreach(var text in Texts)
        {
			var mapName = text.GetComponent<TextMeshProUGUI>().text;

			if (mapName != mapInUse)
            {
				Destroy(GameObject.Find(mapName + "(Clone)" ));
			}
        }
    }

    //TODO later on upgrade it(now only loads MAPS!)
    public void ListLoadableGames()
	{
		InitButtons();

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

	private void InitButtons()
    {
		//Subscribe every button to the handler
		//Note to self. We give a delegate to the AddListener to pass a single param that we use to Load the correct map

		//  for (int i = 0; i < Buttons.Count; i++)
		//  {
		//		Buttons[i].onClick.AddListener(() => LoadGame(i));
		//	}

		//Hogy oszinte legyek fogalmam sincs miert nem mukodik a fenti kód. Olyan mintha referenciat adna át az "i" vel és ezért minen 
		//Esemeny 4 et ad parameterul tovabb pedig az bele se fut viszon az i atallitodik ra tehat ez megerositi a referencia teoriat(DE PEDIG AZ int).
		Buttons[0].onClick.AddListener(() => LoadGame(0));
		Buttons[1].onClick.AddListener(() => LoadGame(1));
		Buttons[2].onClick.AddListener(() => LoadGame(2));
		Buttons[3].onClick.AddListener(() => LoadGame(3));
	}
	public void Quit()
	{
		Application.Quit();    
	}
}
