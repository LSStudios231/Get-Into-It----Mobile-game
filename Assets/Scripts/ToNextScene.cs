using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ToNextScene : MonoBehaviour
{
    public int nextLevelIndex;
    public string buttonDisableKey;
    public string pointsTextKey;
    public string completedTextKey;

    public Material changedMaterial;
    public int pointsToAdd = 5; 

    private Material originalMaterial;
    private MeshRenderer[] wallMeshRenderers;

    private void Start()
    {
        // Reset the "PointsAdded" key when the game starts or a new level is loaded
        PlayerPrefs.DeleteKey("PointsAdded");

        wallMeshRenderers = GetComponentsInChildren<MeshRenderer>();
        originalMaterial = wallMeshRenderers[0].material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UnlockNextLevel();
            LoadNextScene();
            DisableButtonInPreviousScene();
            EnableTextInPreviousScene();
            DisableTextInPreviousScene();

            // Check if points have been added already
            if (!PlayerPrefs.HasKey("PointsAdded"))
            {
                AddPoints(pointsToAdd);
                PlayerPrefs.SetInt("PointsAdded", 1);

                // Notify the SkinManager script to update UI
                SkinManager skinManager = FindObjectOfType<SkinManager>();
                if (skinManager != null)
                {
                    skinManager.UpdatePointsDisplay();
                }
                else
                {
                    Debug.LogWarning("SkinManager script not found in the scene.");
                }
            }

            StartCoroutine(ChangeWallMaterialForDuration());
        }
    }

    private void UnlockNextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
        {
            PlayerPrefs.SetInt("levelsUnlocked", currentLevel + 1);
        }
    }

    private void LoadNextScene()
    {
        int nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneToLoad);
    }

    private void DisableButtonInPreviousScene()
    {
        PlayerPrefs.SetInt(buttonDisableKey, 1);
    }

    private void EnableTextInPreviousScene()
    {
        PlayerPrefs.SetInt(completedTextKey, 1);
    }

    private void DisableTextInPreviousScene()
    {
        PlayerPrefs.SetInt(pointsTextKey, 1);
    }

    private void AddPoints(int amount)
    {
        int numberOfPoints = PlayerPrefs.GetInt("Points", 0) + amount;
        PlayerPrefs.SetInt("Points", numberOfPoints);
        PlayerPrefs.Save();
    }

    private IEnumerator ChangeWallMaterialForDuration()
    {
        foreach (MeshRenderer quadRenderer in wallMeshRenderers)
        {
            quadRenderer.material = changedMaterial;
        }

        yield return new WaitForSeconds(3f);

        RevertWallMaterial();
    }

    private void RevertWallMaterial()
    {
        foreach (MeshRenderer quadRenderer in wallMeshRenderers)
        {
            quadRenderer.material = originalMaterial;
        }
    }
}
