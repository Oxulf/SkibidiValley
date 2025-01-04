using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileInteraction : MonoBehaviour
{
    public Sprite spriteAvecDéchets;
    public Sprite spritePropre;
    public Sprite spriteSolArrose;
    public Sprite spriteGraine;
    public Sprite spritePlanteMature;

    private SpriteRenderer spriteRenderer;
    private bool estNettoye = false;
    private bool estArrose = false;
    private bool aGraine = false;
    private bool planteMature = false;
    private int joursDepuisPlantation = 0;
    private bool interactionEnCours = false;

    // Gestion de l'inventaire
    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    // Compteur global pour le blé récolté
    public static int totalWheatRecolte = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteAvecDéchets;

        // Initialisation de l'inventaire
        inventory.Add("Wheat", 0);

        // Écoute les changements de jour
        TimeManager.instance.OnNewDay += PasserUnJour;
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
        yield return new WaitForSeconds(5f);
        interactionEnCours = false;
    }

    void GererInteraction()
    {
        if (!estNettoye)
        {
            Nettoyer();
        }
        else if (!estArrose)
        {
            Arroser();
        }
        else if (!aGraine)
        {
            PlanterGraine();
        }
        else if (planteMature)
        {
            Recolter();
        }
        else
        {
            Debug.Log("Aucune interaction possible.");
        }
    }

    void Nettoyer()
    {
        if (!estNettoye)
        {
            estNettoye = true;
            spriteRenderer.sprite = spritePropre;
            Debug.Log("Terrain nettoye !");
        }
    }

    void Arroser()
    {
        if (estNettoye && !estArrose)
        {
            estArrose = true;
            spriteRenderer.sprite = spriteSolArrose;
            Debug.Log("Sol arrose !");
        }
        else if (!estNettoye)
        {
            Debug.Log("Impossible d'arroser : le sol est sale !");
        }
    }

    void PlanterGraine()
    {
        if (estArrose && !aGraine)
        {
            aGraine = true;
            joursDepuisPlantation = 0;
            spriteRenderer.sprite = spriteGraine;
            Debug.Log("Graine plantee !");
        }
        else if (!estArrose)
        {
            Debug.Log("Impossible de planter : le sol n'est pas arrose !");
        }
    }

    void PasserUnJour()
    {
        if (aGraine && !planteMature)
        {
            joursDepuisPlantation++;

            if (joursDepuisPlantation >= 3) // La plante met 3 jours à pousser
            {
                planteMature = true;
                spriteRenderer.sprite = spritePlanteMature;
                Debug.Log("Plante mature !");
            }
        }
    }

    void Recolter()
    {
        if (planteMature)
        {
            planteMature = false;
            aGraine = false;
            estArrose = false;  // Réinitialise l'arrosage
            spriteRenderer.sprite = spritePropre;

            // Ajout à l'inventaire
            AjouterItem("Wheat", 1);

            // Mise à jour du compteur global pour le blé récolté
            totalWheatRecolte += 1;

            Debug.Log("Plante recoltee ! Vous avez collecté 1 Wheat.");
            Debug.Log("Terrain pret a etre arrose.");
            Debug.Log($"Total Wheat récolte: {totalWheatRecolte}"); // Affiche le total global de blé récolté
        }
        else
        {
            Debug.Log("Rien a recolter !");
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

        Debug.Log($"{quantite} {item}(s) ajoute(s) a l'inventaire. Total: {inventory[item]}.");
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
