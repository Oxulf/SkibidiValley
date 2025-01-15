using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; 
    private Rigidbody2D rb;
    private Vector2 movement; 
    public SpriteRenderer tracteur; 
    public Sprite Up;
    public Sprite Down;
    public Sprite Left;
    public Sprite Right;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; 
        rb.freezeRotation = true; 
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 && vertical != 0)
        {
            vertical = 0; 
        }
        movement = new Vector2(horizontal, vertical);
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
        rb.velocity = movement.normalized * moveSpeed;
        transform.rotation = Quaternion.identity; 
    }
}
