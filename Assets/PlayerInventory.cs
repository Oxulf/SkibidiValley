using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public GameObject backgroundCanvas; // Référence au Canvas du background
    public GameObject itemsCanvas;      // Référence au Canvas des items
    public Transform itemsContainer;    // Référence au conteneur d'items dans le itemsCanvas
    public GameObject itemPrefab;       // Prefab de l'item à ajouter

    private bool isInventoryOpen = false; // État de l'inventaire (ouvert/fermé)

    void Start()
    {
        // Assurez-vous que l'inventaire est fermé au début
        backgroundCanvas.SetActive(false);
        itemsCanvas.SetActive(false);
    }

    void Update()
    {
        // Ouvrir ou fermer l'inventaire avec Tab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;

        // Activer/Désactiver les deux Canvas
        backgroundCanvas.SetActive(isInventoryOpen);
        itemsCanvas.SetActive(isInventoryOpen);
    }

    public void AddItemToInventory(string itemName)
    {
        GameObject newItem = Instantiate(itemPrefab, itemsContainer);

        // Ajoutez le nom de l'item dans le visuel
        Text itemText = newItem.GetComponentInChildren<Text>();
        if (itemText != null)
        {
            itemText.text = itemName; // Affiche le nom de l'item
        }

        RectTransform rt = newItem.GetComponent<RectTransform>();
        if (rt == null)
        {
            Debug.LogError("Le prefab d'item doit avoir un RectTransform !");
            return;
        }

        rt.anchoredPosition = Vector2.zero; // Centré dans le parent
        rt.sizeDelta = new Vector2(100, 100); // Ajuster la taille si nécessaire

        // Forcez la mise à jour du layout
        LayoutRebuilder.ForceRebuildLayoutImmediate(itemsContainer.GetComponent<RectTransform>());

        Debug.Log($"Item ajouté : {itemName}");
    }
}
