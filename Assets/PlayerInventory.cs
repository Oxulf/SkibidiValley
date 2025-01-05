using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject inventoryCanvas; // Reference au Canvas de l'inventaire
    public Transform itemsContainer;  // Reference au conteneur d'items
    public GameObject itemPrefab;     // Prefab de l'item a ajouter

    private bool isInventoryOpen = false; // Etat de l'inventaire (ouvert/ferme)

    void Update()
    {
        // Ouvrir ou fermer l'inventaire avec Tab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }

        // Ajouter un item avec F
        if (Input.GetKeyDown(KeyCode.F))
        {
            AddItemToInventory();
        }
    }

    void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryCanvas.SetActive(isInventoryOpen);
    }

    void AddItemToInventory()
    {
        // Créer un nouvel item a partir du prefab
        GameObject newItem = Instantiate(itemPrefab, itemsContainer);

        Debug.Log("Item ajoute a l'inventaire !");
    }
}
