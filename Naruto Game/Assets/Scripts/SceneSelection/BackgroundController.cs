using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour {

    public GameObject[] backgrounds;
    public GameObject[] scenes;
    private int currentBackground = 0;

    public void GoRight()
    {
        backgrounds[currentBackground].SetActive(false);
        scenes[currentBackground].SetActive(false);

        currentBackground++;
        if (currentBackground >= backgrounds.Length)
        {
            currentBackground = 0;
        }

        scenes[currentBackground].SetActive(true);
        backgrounds[currentBackground].SetActive(true);
    }

    public void GoLeft()
    {
        backgrounds[currentBackground].SetActive(false);
        scenes[currentBackground].SetActive(false);

        currentBackground--;
        if (currentBackground < 0)
        {
            currentBackground = backgrounds.Length - 1;
        }

        scenes[currentBackground].SetActive(true);
        backgrounds[currentBackground].SetActive(true);
    }

    public int GetCurrentBGIndex()
    {
        return currentBackground;
    }
}
