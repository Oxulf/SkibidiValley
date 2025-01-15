using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ActionWheelManager : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public List<Button> buttons; 
    public List<SeedPrefab> availableSeeds; 
    public GameObject selectedSeedPrefab; 
    void Start()
    {
        if (availableSeeds.Count > 0 && buttons.Count > 0)
        {
            AssignSeedsToButtons();
        }
        else
        {
            Debug.LogWarning("Aucune graine ou bouton disponible.");
        }
    }
    void AssignSeedsToButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (i < availableSeeds.Count)
            {
                SeedPrefab seed = availableSeeds[i];
                Image buttonImage = buttons[i].GetComponent<Image>();
                if (buttonImage != null)
                {
                    buttonImage.sprite = seed.seedSprite;
                }
                Button button = buttons[i];
                button.onClick.RemoveAllListeners(); 
                button.onClick.AddListener(() => SelectSeed(seed));
            }
            else
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
    }
    void SelectSeed(SeedPrefab seed)
    {
        selectedSeedPrefab = seed.seedPrefab;
        if (selectedSeedPrefab != null)
        {
            string seedName = selectedSeedPrefab.GetComponent<SeedData>()?.seedName;
            Debug.Log($"Graine sélectionnée : {seed.seedName}, Prefab associé : {selectedSeedPrefab.name}, Nom dans SeedData : {seedName}");
        }
        else
        {
            Debug.Log("Le prefab associé à cette graine est null !");
        }
    }
}
