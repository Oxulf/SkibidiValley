using UnityEngine;
using UnityEngine.UI;

public class MerchantUIController : MonoBehaviour
{
    public GameObject merchantUI; // Canvas pour l'interface du marchand
    private bool isPlayerNear = false;
    public PlayerInventory playerInventory; // Référence à l'inventaire du joueur
    public MerchantInventory merchantInventory; // Référence à l'inventaire du marchand

    public Transform itemsContainer; // Conteneur des items à afficher dans l'UI du marchand
    public GameObject itemButtonPrefab; // Préfabriqué pour le bouton des objets à vendre

    void Start()
    {
        if (merchantUI != null)
        {
            merchantUI.SetActive(false); // Désactiver l'UI au démarrage
        }
        DisplayMerchantItems();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Key R pressed.");
        }

        if (isPlayerNear && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Toggling merchant UI...");
            ToggleMerchantUI();
        }
    }

    void ToggleMerchantUI()
    {
        if (merchantUI != null)
        {
            bool isActive = merchantUI.activeSelf;
            merchantUI.SetActive(!isActive);

            if (!isActive)
            {
                DisplayMerchantItems(); // Réaffiche les items à vendre lorsque l'UI est ouverte
            }

            Debug.Log("Merchant UI is now: " + (isActive ? "Disabled" : "Enabled"));
        }
        else
        {
            Debug.LogError("Merchant UI is not assigned in the inspector!");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("CollisionStay detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("NPC"))
        {
            isPlayerNear = true;
            Debug.Log("Player is near the merchant.");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("CollisionExit detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("NPC"))
        {
            isPlayerNear = false;
            Debug.Log("Player left the merchant.");
            if (merchantUI != null)
            {
                merchantUI.SetActive(false);
            }
        }
    }

    // Affiche les objets à vendre du marchand
    void DisplayMerchantItems()
    {
        if (itemsContainer == null || itemButtonPrefab == null || merchantInventory == null)
        {
            Debug.LogError("UI Elements or Merchant inventory are not set correctly.");
            return;
        }

        // Efface les anciens boutons d'items
        foreach (Transform child in itemsContainer)
        {
            Destroy(child.gameObject);
        }

        // Affiche les items à vendre
        foreach (var item in merchantInventory.itemsForSale)
        {
            GameObject button = Instantiate(itemButtonPrefab, itemsContainer); // Crée un bouton pour chaque item à vendre
            Text buttonText = button.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = $"{item.prefab.name} - {item.price} Coins"; // Met à jour le texte du bouton
            }

            Button buttonComponent = button.GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() => BuyItemFromMerchant(item.prefab.name, 1));
            }
        }
    }

    // Acheter un item au marchand
    void BuyItemFromMerchant(string itemName, int quantity)
    {
        if (merchantInventory != null && playerInventory != null)
        {
            bool success = merchantInventory.SellItem(itemName, quantity, playerInventory);
            if (success)
            {
                Debug.Log($"Achat de {quantity} {itemName}(s) réussi.");
                DisplayMerchantItems(); // Rafraîchit les items après l'achat
            }
            else
            {
                Debug.LogWarning($"L'achat de {itemName} a échoué.");
            }
        }
        else
        {
            Debug.LogError("MerchantInventory or PlayerInventory is null.");
        }
    }
}