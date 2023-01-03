using UnityEngine;

public class PlayerMovment : MonoBehaviour {
    
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anima;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldwon;
    

    private void Awake() {
        // Animasyon Ve Karakter Nesneleri
        body = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    

    private void Jump() {
        body.velocity = new Vector2(body.velocity.x, speed);
        anima.SetTrigger("jump");
    }

    private void OnCollisionEnter2D (Collision2D collision) {
       
    }

    private bool isGrounded() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}
