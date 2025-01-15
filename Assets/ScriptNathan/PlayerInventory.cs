using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [System.Serializable]
    public class StartingItem
    {
        public GameObject prefab;
        public int quantity;  
    }

    public List<StartingItem> startingItems;

    public GameObject backgroundCanvas;
    public GameObject itemsCanvas;
    public Transform itemsContainer;
    public List<GameObject> collectiblePrefabs;
    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    private bool isInventoryOpen = false;

    void Start()
    {
        backgroundCanvas.SetActive(false);
        itemsCanvas.SetActive(false);

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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;

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

                UpdateUI(itemName, inventory[itemName]);
            }
            else
            {
                inventory[itemName] = quantity;

                GameObject newItem = Instantiate(prefab, itemsContainer);

                RectTransform rt = newItem.GetComponent<RectTransform>();
                if (rt == null)
                {
                    Debug.LogError("Le prefab d'item doit avoir un RectTransform !");
                    return;
                }

                rt.anchoredPosition = Vector2.zero;
                rt.sizeDelta = new Vector2(100, 100);

                Text quantityText = newItem.GetComponentInChildren<Text>();
                if (quantityText != null)
                {
                    quantityText.text = inventory[itemName].ToString();
                }

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

                Transform itemUI = itemsContainer.Find(itemName);
                if (itemUI != null)
                {
                    Destroy(itemUI.gameObject);
                }

                Debug.Log($"L'item {itemName} a été épuisé et retiré de l'inventaire.");
            }
            else
            {
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
    public int GetItemQuantity(string itemName)
    {
        if (inventory.ContainsKey(itemName))
        {
            return inventory[itemName];
        }

        return 0;
    }
}
