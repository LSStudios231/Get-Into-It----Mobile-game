using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinManager : MonoBehaviour
{
    public GameObject[] skinImages;
    public int selectedSkinIndex;

    public Button confirmButton;
    public TextMeshProUGUI pointsText;
    public Button buyButton;
    public TextMeshProUGUI priceText; 

    public int[] skinPrices;
    private int points;

    private void Awake()
    {
        selectedSkinIndex = PlayerPrefs.GetInt("SelectedSkin", 0);
        UpdateSkinDisplay();
        UpdatePointsDisplay();
        UpdateBuyButton();
        UpdateConfirmButton();
    }

    public void SwitchSkinLeft()
    {
        DisableAllSkins();
        selectedSkinIndex = (selectedSkinIndex - 1 + skinImages.Length) % skinImages.Length;
        UpdateSkinDisplay();
        UpdateBuyButton();
        UpdateConfirmButton();  // Ensure the confirm button state is updated when switching skins
    }

    public void SwitchSkinRight()
    {
        DisableAllSkins();
        selectedSkinIndex = (selectedSkinIndex + 1) % skinImages.Length;
        UpdateSkinDisplay();
        UpdateBuyButton();
        UpdateConfirmButton();  // Ensure the confirm button state is updated when switching skins
    }

    public void BuySkin()
    {
        int skinPrice = skinPrices[selectedSkinIndex];
        if (points >= skinPrice)
        {
            DeductPoints(skinPrice);
            PlayerPrefs.SetInt("UnlockStatus_" + selectedSkinIndex, 1);
            PlayerPrefs.SetInt("SkinBought_" + selectedSkinIndex, 1);
            UpdateBuyButton();
            UpdateConfirmButton(); // Update confirm button state after buying skin
        }

        UpdatePointsDisplay();
    }

    public void ConfirmSkinSelection()
    {
        PlayerPrefs.SetInt("SelectedSkin", selectedSkinIndex);
        ApplySelectedSkinMaterial();
    }

    private void UpdateSkinDisplay()
    {
        if (selectedSkinIndex >= 0 && selectedSkinIndex < skinImages.Length)
        {
            DisableAllSkins();
            skinImages[selectedSkinIndex].SetActive(true);
        }
    }

    private void DisableAllSkins()
    {
        foreach (GameObject skinImage in skinImages)
        {
            skinImage.SetActive(false);
        }
    }

    private void ApplySelectedSkinMaterial()
    {
        Renderer playerRenderer = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Renderer>();
        if (playerRenderer != null)
        {
            // Apply selected skin material here
        }
    }

    private void UpdateBuyButton()
    {
        bool isSkinUnlocked = IsSkinUnlocked(selectedSkinIndex);
        bool isSkinBought = IsSkinBought(selectedSkinIndex);

        // Enable or disable the buy button based on whether the skin is unlocked and not bought
        buyButton.interactable = !isSkinUnlocked && !isSkinBought;

        // Update the buy button text with the current skin price
        priceText.text = "Buy: " + skinPrices[selectedSkinIndex] + " points";
    }

    private void UpdateConfirmButton()
    {
        // Check if the selected skin is bought
        bool isSkinBought = IsSkinBought(selectedSkinIndex);

        // Enable confirm button only if the selected skin is bought
        confirmButton.interactable = isSkinBought;
    }

    private bool IsSkinUnlocked(int skinIndex)
    {
        return PlayerPrefs.GetInt("UnlockStatus_" + skinIndex, 0) == 1;
    }

    private bool IsSkinBought(int skinIndex)
    {
        return PlayerPrefs.GetInt("SkinBought_" + skinIndex, 0) == 1;
    }

    public void UpdatePointsDisplay()
    {
        points = PlayerPrefs.GetInt("Points", 0);
        pointsText.text = "Points: " + points;
    }

    private void DeductPoints(int amount)
    {
        points -= amount;
        PlayerPrefs.SetInt("Points", points);
    }
}
