using System.Collections.Generic;
using UnityEngine;

public class MerchantInventory : MonoBehaviour
{
    [System.Serializable]
    public class MerchantItem
    {
        public GameObject prefab; // Prefab de l'item (l'objet que le marchand vend)
        public int quantity;      // Quantité d'items que le marchand a
        public int price;         // Prix de l'item
    }

    public List<MerchantItem> itemsForSale = new List<MerchantItem>(); // Liste des items à vendre

    void Start()
    {
        // Initialiser les items du marchand
        InitializeItems();
    }

    // Initialisation des items du marchand
    void InitializeItems()
    {
        // Exemple d'items prédéfinis
        itemsForSale.Add(new MerchantItem
        {
            prefab = Resources.Load<GameObject>("Items/Apple"), // Pré-chargement d'un objet "Apple"
            quantity = 10,
            price = 5
        });

        itemsForSale.Add(new MerchantItem
        {
            prefab = Resources.Load<GameObject>("Items/Sword"),  // Pré-chargement d'un objet "Sword"
            quantity = 5,
            price = 15
        });

        itemsForSale.Add(new MerchantItem
        {
            prefab = Resources.Load<GameObject>("Items/Shield"), // Pré-chargement d'un objet "Shield"
            quantity = 8,
            price = 10
        });
    }

    // Acheter un item du marchand
    public bool SellItem(string itemName, int quantity, PlayerInventory playerInventory)
    {
        foreach (var item in itemsForSale)
        {
            if (item.prefab.name == itemName && item.quantity >= quantity)
            {
                int totalCost = item.price * quantity;

                // Vérifier si le joueur a assez de pièces pour acheter
                if (playerInventory.HasItem("Coins") && playerInventory.GetItemQuantity("Coins") >= totalCost)
                {
                    // Retirer les pièces du joueur
                    playerInventory.RemoveItem("Coins", totalCost);

                    // Ajouter l'item au joueur
                    for (int i = 0; i < quantity; i++)
                    {
                        playerInventory.AddItemToInventory(item.prefab);
                    }

                    // Réduire la quantité d'item chez le marchand
                    item.quantity -= quantity;

                    return true;
                }
                else
                {
                    Debug.LogWarning("Le joueur n'a pas assez de pièces !");
                    return false;
                }
            }
        }

        Debug.LogWarning($"L'item {itemName} n'est pas disponible ou en quantité insuffisante chez le marchand.");
        return false;
    }
}