using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public AudioSource audioSource;

    private void Awake()
    {
      
        DontDestroyOnLoad(audioSource);
    }
    public void LoadScene(string sceneName)
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        SceneManager.LoadScene(sceneName);

        
    }

    
}
