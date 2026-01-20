using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{
    public int sceneToContinue;

    public void ContinueGame() //Continue to another stage
    {
        SceneManager.LoadScene("Stage1");
        sceneToContinue = PlayerPrefs.GetInt("SavedScene");

        SceneManager.LoadScene(sceneToContinue);

    }
}
