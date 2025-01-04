using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de déplacement
    private Rigidbody2D rb;
    private Vector2 movement; // Direction de mouvement

    public SpriteRenderer tracteur; // Composant SpriteRenderer
    public Sprite Up;
    public Sprite Down;
    public Sprite Left;
    public Sprite Right;

    void Start()
    {
        // Initialisation du Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // Désactiver la gravité
        rb.freezeRotation = true; // Empêcher la rotation du Rigidbody2D
    }

    void Update()
    {
        // Récupérer les entrées utilisateur
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Bloquer le mouvement diagonal
        if (horizontal != 0 && vertical != 0)
        {
            vertical = 0; // On privilégie le déplacement horizontal
        }

        // Mettre à jour le vecteur de mouvement
        movement = new Vector2(horizontal, vertical);

        // Changer le sprite en fonction de la direction
        if (movement.x > 0)
        {
            tracteur.sprite = Right;
        }
        else if (movement.x < 0)
        {
            tracteur.sprite = Left;
        }
        else if (movement.y > 0)
        {
            tracteur.sprite = Up;
        }
        else if (movement.y < 0)
        {
            tracteur.sprite = Down;
        }
    }

    void FixedUpdate()
    {
        // Appliquer le mouvement au Rigidbody
        rb.velocity = movement.normalized * moveSpeed;

        // Empêcher toute modification accidentelle de la rotation
        transform.rotation = Quaternion.identity; // Garde la rotation fixe
    }
}
