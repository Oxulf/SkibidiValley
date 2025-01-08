using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ActionWheelManager : MonoBehaviour
{
    public List<Button> buttons; // Liste des boutons de la roue, configurés dans l'éditeur
    public List<SeedPrefab> availableSeeds; // Liste des graines disponibles
    public GameObject selectedSeedPrefab; // La graine actuellement sélectionnée

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

                // Configurez l'image du bouton
                Image buttonImage = buttons[i].GetComponent<Image>();
                if (buttonImage != null)
                {
                    buttonImage.sprite = seed.seedSprite;
                }

                // Ajouter l'action de clic
                Button button = buttons[i];
                button.onClick.RemoveAllListeners(); // Supprime les anciens listeners
                button.onClick.AddListener(() => SelectSeed(seed));
            }
            else
            {
                // Désactivez les boutons inutilisés
                buttons[i].gameObject.SetActive(false);
            }
        }
    }

    void SelectSeed(SeedPrefab seed)
    {
        selectedSeedPrefab = seed.seedPrefab;
        Debug.Log($"Graine sélectionnée : {seed.seedName}");
    }
}
