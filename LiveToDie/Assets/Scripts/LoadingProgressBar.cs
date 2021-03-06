﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class LoadingProgressBar : MonoBehaviour
{
    public  Slider slider;
    private static LoadingProgressBar instance;

    public static LoadingProgressBar GetInstance()
    {
        return instance;
    }
    public void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public  void SliderProgression(float progression)
    {
        slider.value = progression;
    }
}
