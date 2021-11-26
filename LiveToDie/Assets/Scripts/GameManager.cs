using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject LoadingScreen;
    public Slider LoadingSlider;

    private float _totalLoadingProgress;

    private float _totalMapProgress;

    private void Awake()
    {
        instance = this;

        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    internal void NewGame()
    {
        NavigationStatics.isNewGame = true;
        LoadGameScene();
    }

    internal void LoadGame()
    {
        NavigationStatics.isNewGame = false;
        LoadGameScene();
    }

    private void LoadGameScene()
    {
        LoadingScreen.gameObject.SetActive(true);

        scenesToLoad.Add(SceneManager.UnloadSceneAsync((int)Scenes.MainMenu));
        scenesToLoad.Add(SceneManager.LoadSceneAsync((int)Scenes.Game, LoadSceneMode.Additive));
        //try to delete this bro

        StartCoroutine(GetSceneLoadProgress());
        StartCoroutine(GetTotalProgress());
    }

    public void LoadMenuScene()
    {
        scenesToLoad.Add(SceneManager.UnloadSceneAsync((int)Scenes.Game));
        scenesToLoad.Add(SceneManager.LoadSceneAsync((int)Scenes.MainMenu, LoadSceneMode.Additive));
    }

    public IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < scenesToLoad.Count; i++)
        {
            while (!scenesToLoad[i].isDone)
            {
                _totalLoadingProgress = 0;

                foreach( AsyncOperation operation in scenesToLoad)
                {
                    _totalLoadingProgress += operation.progress;
                }

                _totalLoadingProgress = (_totalLoadingProgress / scenesToLoad.Count);

                yield return null; 
            }

        }

        LoadingScreen.gameObject.SetActive(false);
    }

    public IEnumerator GetTotalProgress()
    {
        float totalProgress = 0f;

        while(MapGenerator.instance == null || !MapGenerator.instance.IsDone)
        {
            if(MapGenerator.instance == null)
            {
                _totalMapProgress = 0f;
            }
            else
            {
                _totalMapProgress = MapGenerator.instance.Progression;
            }

            totalProgress = (_totalLoadingProgress + _totalMapProgress) / 2f;

            LoadingSlider.value = _totalLoadingProgress;

            yield return null;
        }

        LoadingScreen.gameObject.SetActive(false);

    }
}
