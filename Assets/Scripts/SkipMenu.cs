using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject continueButton;
    public Transform spawnPoint; 

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivatePauseMenu();
        }
    }

    public void ActivatePauseMenu()
    {
        Time.timeScale = 0f;

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }

        if (continueButton != null)
        {
            continueButton.SetActive(true);
        }

        

        MovePlayerToNewPosition();
        DeactivateSkipMenu();
    }

    public void DeactivatePauseMenu()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        continueButton.SetActive(false);
    }

    private void MovePlayerToNewPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && spawnPoint != null)
        {
            player.transform.position = spawnPoint.position;
        }
    }

    private void DeactivateSkipMenu()
    {
        gameObject.SetActive(false); // Deactivate the SkipMenu GameObject
    }
}

