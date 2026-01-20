using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLost : MonoBehaviour
{
    void OnTriggerEnter(Collider other) //Game lost
    {
        SceneManager.LoadScene(62);
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
    }
}
