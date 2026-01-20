using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisableStageButton : MonoBehaviour
{
    public Button stageButton;
    public string buttonDisableKey;
    public GameObject pointsText;
    public GameObject completedText;
    public string pointsTextKey;
    public string completedTextKey;

    void Start()
    {
        if (PlayerPrefs.GetInt(buttonDisableKey, 0) == 1)
        {
            stageButton.interactable = false;
        }

        if (PlayerPrefs.GetInt(pointsTextKey, 0) == 1)
        {
            pointsText.SetActive(false);
        }
        if (PlayerPrefs.GetInt(completedTextKey, 0) == 1)
        {
            completedText.SetActive(true);
        }
    }
}

