using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [System.Serializable]
    public class StartingItem
    {
        public GameObject prefab; // Prefab de l'item initial
        public int quantity;      // Quantité initiale
    }

    public List<StartingItem> startingItems; // Liste des items initiaux

    public GameObject backgroundCanvas; // Référence au Canvas du background
    public GameObject itemsCanvas;      // Référence au Canvas des items
    public Transform itemsContainer;    // Référence au conteneur d'items dans le itemsCanvas
    public List<GameObject> collectiblePrefabs; // Liste des prefabs récoltables
    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    private bool isInventoryOpen = false; // État de l'inventaire (ouvert/fermé)

    void Start()
    {
        // Fermez l'inventaire au départ
        backgroundCanvas.SetActive(false);
        itemsCanvas.SetActive(false);

        // Ajoutez les items initiaux à l'inventaire
        foreach (var item in startingItems)
        {
            for (int i = 0; i < item.quantity; i++)
            {
                AddItemToInventory(item.prefab);
            }
        }
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

    public void AddItemToInventory(GameObject prefab, int quantity = 1)
    {
        if (collectiblePrefabs.Contains(prefab))
        {
            string itemName = prefab.name;

            if (inventory.ContainsKey(itemName))
            {
                inventory[itemName] += quantity;

                // Met à jour l'UI de l'objet existant
                UpdateUI(itemName, inventory[itemName]);
            }
            else
            {
                inventory[itemName] = quantity;

                // Crée une instance de l’item dans l’UI uniquement si c'est un nouvel objet
                GameObject newItem = Instantiate(prefab, itemsContainer);
                newItem.name = itemName; // Utilise le nom pour le retrouver facilement dans l'UI

                RectTransform rt = newItem.GetComponent<RectTransform>();
                if (rt == null)
                {
                    Debug.LogError("Le prefab d'item doit avoir un RectTransform !");
                    return;
                }

                rt.anchoredPosition = Vector2.zero; // Centré dans le parent
                rt.sizeDelta = new Vector2(100, 100); // Ajuster la taille si nécessaire

                // Ajoutez un texte pour afficher la quantité
                Text quantityText = newItem.GetComponentInChildren<Text>();
                if (quantityText != null)
                {
                    quantityText.text = inventory[itemName].ToString();
                }

                // Forcez la mise à jour du layout
                LayoutRebuilder.ForceRebuildLayoutImmediate(itemsContainer.GetComponent<RectTransform>());
            }

            Debug.Log($"Ajouté : {quantity} {itemName}(s). Total : {inventory[itemName]}");
        }
        else
        {
            Debug.LogWarning($"Le prefab {prefab.name} n'est pas dans la liste des items récoltables !");
        }
    }

    public bool HasItem(string itemName)
    {
        Debug.Log($"Vérification de l'item : {itemName}. Inventaire actuel :");
        foreach (var kvp in inventory)
        {
            Debug.Log($"{kvp.Key}: {kvp.Value}");
        }
        return inventory.ContainsKey(itemName) && inventory[itemName] > 0;
    }

    public void RemoveItem(string itemName, int quantity = 1)
    {
        if (HasItem(itemName))
        {
            inventory[itemName] -= quantity;

            if (inventory[itemName] <= 0)
            {
                inventory[itemName] = 0;

                // Supprime l'objet de l'UI
                Transform itemUI = itemsContainer.Find(itemName);
                if (itemUI != null)
                {
                    Destroy(itemUI.gameObject);
                }

                Debug.Log($"L'item {itemName} a été épuisé et retiré de l'inventaire.");
            }
            else
            {
                // Met à jour l'UI de l'objet existant
                UpdateUI(itemName, inventory[itemName]);
            }

            Debug.Log($"Retiré : {quantity} {itemName}(s). Restant : {inventory[itemName]}");
        }
        else
        {
            Debug.LogWarning($"Impossible de retirer {itemName}. Vous n'en possédez pas assez !");
        }
    }
    private void UpdateUI(string itemName, int quantity)
    {
        Transform itemUI = itemsContainer.Find(itemName);
        if (itemUI != null)
        {
            Text quantityText = itemUI.GetComponentInChildren<Text>();
            if (quantityText != null)
            {
                quantityText.text = quantity.ToString();
            }
        }
    }
}
