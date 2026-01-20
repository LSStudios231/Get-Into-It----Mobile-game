using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    private string previousSceneName;

    void Start()
    {
        // Get the name of the previous scene
        previousSceneName = PlayerPrefs.GetString("PreviousScene");
    }

    public void RetryLevel()
    {
        // Load the previous scene
        SceneManager.LoadScene(previousSceneName);
    }
}
