using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de d�placement
    private Rigidbody2D rb;
    private Vector2 movement; // Direction de mouvement
    public GameObject directionIndicator; // Indicateur de direction

    void Start()
    {
        // Initialisation du Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // D�sactiver la gravit�

        // Cr�ation de l'indicateur de direction si non attribu�
        if (directionIndicator == null)
        {
            directionIndicator = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            directionIndicator.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            directionIndicator.GetComponent<Renderer>().material.color = Color.yellow;
        }

        directionIndicator.transform.parent = transform; // Fixer l'indicateur au joueur
        directionIndicator.transform.localPosition = Vector3.zero;
    }

    void Update()
    {
        // R�cup�rer les entr�es utilisateur
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Limiter les mouvements � un seul axe (priorit� horizontale ici)
        if (horizontal != 0 && vertical != 0)
        {
            vertical = 0; // Priorise les mouvements horizontaux
        }

        // Mettre � jour le vecteur de mouvement
        movement = new Vector2(horizontal, vertical);

        // Mettre � jour l'indicateur de direction
        UpdateDirectionIndicator();
    }

    void FixedUpdate()
    {
        // Appliquer le mouvement au Rigidbody
        rb.velocity = movement.normalized * moveSpeed;
    }

    void UpdateDirectionIndicator()
    {
        if (movement != Vector2.zero)
        {
            directionIndicator.SetActive(true); // Afficher l'indicateur de direction
            Vector3 directionPosition = new Vector3(movement.x, movement.y, 0f).normalized;
            directionIndicator.transform.localPosition = directionPosition;
        }
        else
        {
            directionIndicator.SetActive(false); // Masquer l'indicateur si pas de mouvement
        }
    }
}
