using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSwitcher : MonoBehaviour
{
    public GameObject[] levelCanvases;

    public void StageButtonClicked(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < levelCanvases.Length)
        {
            DisableAllCanvases();

            levelCanvases[levelIndex].SetActive(true);
        }
        else
        {
            Debug.LogError("Invalid level index: " + levelIndex);
        }
    }

    public void BackButtonPressed()
    {
        DisableAllCanvases();
    }

    private void DisableAllCanvases()
    {
        foreach (var canvas in levelCanvases)
        {
            canvas.SetActive(false);
        }
    }
}



