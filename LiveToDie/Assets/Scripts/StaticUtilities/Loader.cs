using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class Loader
{
    //Ures class mert a Corutinet csak olyan class bol lehet hasznalni ami Hasznalja a MonoBehaviour-t
    private class LoadingMonoBehaviour : MonoBehaviour { };

    public enum Scene
    {
        Game,
        Loading,
        MainMenu
    }

    private static Action onLoaderCallBack;
    private static AsyncOperation operation;

    public static void Load(Scene scene)
    {
        onLoaderCallBack = () =>
        {
            GameObject loadingObject = new GameObject("Loading Game Ovject");
            loadingObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(AsyncLoad(scene));
            AsyncLoad(scene);
        };

        SceneManager.LoadScene(Scene.Loading.ToString());

    }


    //Corutine tobb framen at fut --> Unity dokumentacio szerint ezt kell hasznalni
    private static IEnumerator AsyncLoad(Scene scene)
    {
        //egy frame et tovabblep toltes elott
        yield return null;

        operation = SceneManager.LoadSceneAsync(scene.ToString());

        while (!operation.isDone)
        {
            yield return null;
        }
    }   

    public static void LoaderCallBack()
    {
        if (onLoaderCallBack != null)
        {
            onLoaderCallBack();
            onLoaderCallBack = null;
        }
    }
}
