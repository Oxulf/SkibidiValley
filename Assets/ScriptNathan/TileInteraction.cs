using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileInteraction : MonoBehaviour
{
    public Sprite[] cropStages; // Array des sprites pour chaque étape
    public string cropName = ""; // Nom de la culture
    public int[] daysPerStage; // Nombre de jours nécessaires par étape
    private int currentStage = 0; // Étape actuelle

    private SpriteRenderer spriteRenderer;
    private bool interactionEnCours = false;

    // Gestion de l'inventaire
    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    // État actuel de la parcelle
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
        spriteRenderer.sprite = cropStages[0]; // Sprite initial (sale)

        // Initialisation de l'inventaire
        if (!inventory.ContainsKey(cropName))
        {
            inventory.Add(cropName, 0);
        }
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
        yield return new WaitForSeconds(0.5f); // Délai pour éviter une double interaction
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
        cropState = CropState.Seeded;
        currentStage = 0;
        spriteRenderer.sprite = cropStages[3];
        Debug.Log("Graine plantée !");
    }

    void ArroserPourPousser()
    {
        if (cropState == CropState.Seeded || cropState == CropState.Growing || cropState == CropState.Flowering)
        {
            Debug.Log($"Arrosage effectué pour l'état : {cropState}. La plante va progresser...");

            // Lancer la coroutine pour gérer le délai après l’arrosage
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

        // Cooldown pour simuler le temps de croissance
        yield return new WaitForSeconds(3f);

        // Passer à l’étape suivante après le délai
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
            spriteRenderer.sprite = cropStages[cropStages.Length - 1];
            Debug.Log("La plante est prête à être récoltée !");
        }
    }

    void Recolter()
    {
        if (cropState == CropState.Harvestable)
        {
            cropState = CropState.Clean;
            spriteRenderer.sprite = cropStages[1]; // Retour à l'état sec

            // Ajoute l’item dans l’inventaire
            AjouterItem(cropName, 1);
            Debug.Log($"{cropName} récolté !");
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
