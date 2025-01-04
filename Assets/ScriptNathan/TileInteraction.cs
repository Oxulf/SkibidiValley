using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileInteraction : MonoBehaviour
{
    public Sprite spriteAvecD�chets;
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

    // Compteur global pour le bl� r�colt�
    public static int totalBl�R�colt� = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteAvecD�chets;

        // Initialisation de l'inventaire
        inventory.Add("Bl�", 0);

        // �coute les changements de jour
        TimeManager.instance.OnNewDay += PasserUnJour;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && JoueurEstProche() && !interactionEnCours)
        {
            StartCoroutine(G�rerInteractionAvecDelai());
        }
    }

    IEnumerator G�rerInteractionAvecDelai()
    {
        interactionEnCours = true;
        G�rerInteraction();
        yield return new WaitForSeconds(5f);
        interactionEnCours = false;
    }

    void G�rerInteraction()
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
            Debug.Log("Terrain nettoy� !");
        }
    }

    void Arroser()
    {
        if (estNettoye && !estArrose)
        {
            estArrose = true;
            spriteRenderer.sprite = spriteSolArrose;
            Debug.Log("Sol arros� !");
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
            Debug.Log("Graine plant�e !");
        }
        else if (!estArrose)
        {
            Debug.Log("Impossible de planter : le sol n'est pas arros� !");
        }
    }

    void PasserUnJour()
    {
        if (aGraine && !planteMature)
        {
            joursDepuisPlantation++;

            if (joursDepuisPlantation >= 3) // La plante met 3 jours � pousser
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
            estArrose = false;  // R�initialise l'arrosage
            spriteRenderer.sprite = spritePropre;

            // Ajout � l'inventaire
            AjouterItem("Bl�", 1);

            // Mise � jour du compteur global pour le bl� r�colt�
            totalBl�R�colt� += 1;

            Debug.Log("Plante r�colt�e ! Vous avez collect� 1 Bl�.");
            Debug.Log("Terrain pr�t � �tre arros�.");
            Debug.Log($"Total Bl� r�colt�: {totalBl�R�colt�}"); // Affiche le total global de bl� r�colt�
        }
        else
        {
            Debug.Log("Rien � r�colter !");
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

        Debug.Log($"{quantite} {item}(s) ajout�(s) � l'inventaire. Total: {inventory[item]}.");
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
