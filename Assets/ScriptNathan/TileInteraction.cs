using UnityEngine;

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

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteAvecD�chets;

        // �coute les changements de jour
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
            Debug.Log("Plante r�colt�e ! Terrain pr�t � �tre arros�.");
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
}
