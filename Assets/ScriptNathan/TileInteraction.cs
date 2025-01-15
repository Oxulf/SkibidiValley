using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileInteraction : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public ActionWheelManager actionWheel;
    public Sprite[] cropStages;
    public int[] daysPerStage;
    private int currentStage = 0;
    private SeedData plantedSeedData;
    private Sprite harvestableSprite;
    private GameObject harvestablePrefab;
    private SpriteRenderer spriteRenderer;
    private bool interactionEnCours = false;
    private Dictionary<string, int> inventory = new Dictionary<string, int>();
    private enum CropState
    {
        Dirty,
        Clean,
        Watered,
        Seeded,
        Growing,
        Flowering,
        Harvestable
    }
    private CropState cropState = CropState.Dirty;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = cropStages[0]; 
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && JoueurEstProche() && !interactionEnCours)
        {
            StartCoroutine(GererInteractionAvecDelai());
        }
    }
    IEnumerator GererInteractionAvecDelai()
    {
        interactionEnCours = true;
        GererInteraction();
        yield return new WaitForSeconds(0.5f); 
        interactionEnCours = false;
    }
    void GererInteraction()
    {
        switch (cropState)
        {
            case CropState.Dirty:
                Nettoyer();
                break;
            case CropState.Clean:
                Arroser();
                break;
            case CropState.Watered:
                PlanterGraine();
                break;
            case CropState.Seeded:
            case CropState.Growing:
            case CropState.Flowering:
                ArroserPourPousser();
                break;
            case CropState.Harvestable:
                Recolter();
                break;
            default:
                Debug.Log("Aucune interaction possible.");
                break;
        }
    }
    void Nettoyer()
    {
        cropState = CropState.Clean;
        spriteRenderer.sprite = cropStages[1];
        Debug.Log("Terrain nettoyé !");
    }
    void Arroser()
    {
        cropState = CropState.Watered;
        spriteRenderer.sprite = cropStages[2];
        Debug.Log("Sol arrosé !");
    }
    void PlanterGraine()
    {
        if (actionWheel.selectedSeedPrefab != null)
        {
            SeedData seedData = actionWheel.selectedSeedPrefab.GetComponent<SeedData>();
            if (seedData != null)
            {
                string seedName = seedData.seedName;
                Debug.Log($"Tentative de plantation pour : {seedName}");
                if (playerInventory.HasItem(seedName))
                {
                    playerInventory.RemoveItem(seedName);
                    harvestableSprite = seedData.harvestableSprite;
                    harvestablePrefab = seedData.harvestablePrefab;
                    cropState = CropState.Seeded;
                    currentStage = 0;
                    spriteRenderer.sprite = cropStages[3];
                    Debug.Log($"Graine {seedName} plantée !");
                }
                else
                {
                    Debug.Log($"Vous n'avez pas la graine {seedName} dans votre inventaire !");
                }
            }
            else
            {
                Debug.LogError("Le prefab sélectionné n'a pas de script SeedData !");
            }
        }
        else
        {
            Debug.LogError("Aucune graine valide sélectionnée !");
        }
    }
    void ArroserPourPousser()
    {
        if (cropState == CropState.Seeded || cropState == CropState.Growing || cropState == CropState.Flowering)
        {
            Debug.Log($"Arrosage effectué pour l'état : {cropState}. La plante va progresser...");
            StartCoroutine(ProgressionApresArrosage());
        }
        else
        {
            Debug.Log("L'état actuel ne permet pas d'arroser davantage.");
        }
    }
    IEnumerator ProgressionApresArrosage()
    {
        Debug.Log("La plante est en train de pousser...");
        yield return new WaitForSeconds(3f);
        currentStage++;
        if (currentStage == 1)
        {
            cropState = CropState.Growing;
            spriteRenderer.sprite = cropStages[currentStage + 3];
            Debug.Log("La graine a germé !");
        }
        else if (currentStage == 2)
        {
            cropState = CropState.Flowering;
            spriteRenderer.sprite = cropStages[currentStage + 3];
            Debug.Log("La plante commence à fleurir !");
        }
        else if (currentStage == 3)
        {
            cropState = CropState.Harvestable;
            spriteRenderer.sprite = harvestableSprite;
            Debug.Log("La plante est prête à être récoltée !");
        }
    }
    void Recolter()
    {
        if (cropState == CropState.Harvestable)
        {
            cropState = CropState.Clean;
            spriteRenderer.sprite = cropStages[1]; 
            if (harvestablePrefab != null)
            {
                playerInventory.AddItemToInventory(harvestablePrefab);
                Debug.Log($"Récolte effectuée : {harvestablePrefab.name} ajouté à l'inventaire !");
            }
            else
            {
                Debug.LogWarning("Aucun prefab récoltable défini pour cette graine !");
            }
            Debug.Log("Parcelle nettoyée et prête à être réutilisée !");
        }
    }
    bool JoueurEstProche()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Player"));
        return player != null;
    }
    public void AjouterItem(string item, int quantite)
    {
        if (inventory.ContainsKey(item))
        {
            inventory[item] += quantite;
        }
        else
        {
            inventory[item] = quantite;
        }
        Debug.Log($"{quantite} {item}(s) ajouté(s) à l’inventaire. Total: {inventory[item]}.");
    }
    public void AfficherInventaire()
    {
        Debug.Log("Inventaire :");
        foreach (var kvp in inventory)
        {
            Debug.Log($"{kvp.Key}: {kvp.Value}");
        }
    }
}