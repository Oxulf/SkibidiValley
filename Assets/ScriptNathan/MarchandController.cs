using UnityEngine;
using UnityEngine.UI;

public class MerchantUIController : MonoBehaviour
{
    public GameObject merchantUI;
    private bool isPlayerNear = false;
    public PlayerInventory playerInventory;
    public MerchantInventory merchantInventory;

    public Transform itemsContainer;
    public GameObject itemButtonPrefab;

    void Start()
    {
        if (merchantUI != null)
        {
            merchantUI.SetActive(false);
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
                DisplayMerchantItems();
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

    void DisplayMerchantItems()
    {
        if (itemsContainer == null || itemButtonPrefab == null || merchantInventory == null)
        {
            Debug.LogError("UI Elements or Merchant inventory are not set correctly.");
            return;
        }

        foreach (Transform child in itemsContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in merchantInventory.itemsForSale)
        {
            GameObject button = Instantiate(itemButtonPrefab, itemsContainer);
            Text buttonText = button.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = $"{item.prefab.name} - {item.price} Coins";
            }

            Button buttonComponent = button.GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() => BuyItemFromMerchant(item.prefab.name, 1));
            }
        }
    }

    void BuyItemFromMerchant(string itemName, int quantity)
    {
        if (merchantInventory != null && playerInventory != null)
        {
            bool success = merchantInventory.SellItem(itemName, quantity, playerInventory);
            if (success)
            {
                Debug.Log($"Achat de {quantity} {itemName}(s) réussi.");
                DisplayMerchantItems();
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