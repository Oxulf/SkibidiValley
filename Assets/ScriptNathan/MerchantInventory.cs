using System.Collections.Generic;
using UnityEngine;

public class MerchantInventory : MonoBehaviour
{
    [System.Serializable]
    public class MerchantItem
    {
        public GameObject prefab;
        public int quantity;
        public int price;
    }

    public List<MerchantItem> itemsForSale = new List<MerchantItem>();

    void Start()
    {
        InitializeItems();
    }

    void InitializeItems()
    {
        itemsForSale.Add(new MerchantItem
        {
            prefab = Resources.Load<GameObject>("Items/Graines_Pensées_Jaune"),
            quantity = 10,
            price = 1
        });

        itemsForSale.Add(new MerchantItem
        {
            prefab = Resources.Load<GameObject>("Items/Graines_Pensées_Bleue"),
            quantity = 10,
            price = 1
        });

        itemsForSale.Add(new MerchantItem
        {
            prefab = Resources.Load<GameObject>("Items/Graines_Calibrachoa_Jaune"),
            quantity = 10,
            price = 1
        });
        itemsForSale.Add(new MerchantItem
        {
            prefab = Resources.Load<GameObject>("Items/Graines_Calibrachoa_Rouge"),
            quantity = 10,
            price = 1
        });
        itemsForSale.Add(new MerchantItem
        {
            prefab = Resources.Load<GameObject>("Items/Graines_Hibiscus_Rose"),
            quantity = 10,
            price = 1
        });
        itemsForSale.Add(new MerchantItem
        {
            prefab = Resources.Load<GameObject>("Items/Graines_Hibiscus_Bleu"),
            quantity = 10,
            price = 1
        });
        itemsForSale.Add(new MerchantItem
        {
            prefab = Resources.Load<GameObject>("Items/Graines_Lys_Blanche"),
            quantity = 10,
            price = 1
        });
        itemsForSale.Add(new MerchantItem
        {
            prefab = Resources.Load<GameObject>("Items/Graines_Roses_Blanche"),
            quantity = 10,
            price = 1
        });
        itemsForSale.Add(new MerchantItem
        {
            prefab = Resources.Load<GameObject>("Items/Graines_Roses_Rouge"),
            quantity = 10,
            price = 1
        });
    }

    public bool SellItem(string itemName, int quantity, PlayerInventory playerInventory)
    {
        foreach (var item in itemsForSale)
        {
            if (item.prefab.name == itemName && item.quantity >= quantity)
            {
                int totalCost = item.price * quantity;

                if (playerInventory.HasItem("Coins") && playerInventory.GetItemQuantity("Coins") >= totalCost)
                {
                    playerInventory.RemoveItem("Coins", totalCost);

                    for (int i = 0; i < quantity; i++)
                    {
                        playerInventory.AddItemToInventory(item.prefab);
                    }

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