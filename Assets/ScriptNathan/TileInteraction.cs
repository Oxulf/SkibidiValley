using UnityEngine;

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

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteAvecDéchets;

        // Écoute les changements de jour
        TimeManager.instance.OnNewDay += PasserUnJour;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && JoueurEstProche()) Nettoyer();
        if (Input.GetKeyDown(KeyCode.A) && JoueurEstProche()) Arroser();
        if (Input.GetKeyDown(KeyCode.P) && JoueurEstProche()) PlanterGraine();
        if (Input.GetKeyDown(KeyCode.R) && JoueurEstProche()) Recolter();
    }

    void Nettoyer()
    {
        if (!estNettoye)
        {
            estNettoye = true;
            spriteRenderer.sprite = spritePropre;
            Debug.Log("Terrain nettoyé !");
        }
    }

    void Arroser()
    {
        if (estNettoye && !estArrose)
        {
            estArrose = true;
            spriteRenderer.sprite = spriteSolArrose;
            Debug.Log("Sol arrosé !");
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
            Debug.Log("Graine plantée !");
        }
        else if (!estArrose)
        {
            Debug.Log("Impossible de planter : le sol n'est pas arrosé !");
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
            Debug.Log("Plante récoltée ! Terrain prêt à être arrosé.");
        }
        else
        {
            Debug.Log("Rien à récolter !");
        }
    }

    bool JoueurEstProche()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Player"));
        return player != null;
    }
}
