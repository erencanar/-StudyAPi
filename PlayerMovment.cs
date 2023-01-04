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
    
    private void Update() {
        float horizontalInput = Input.GetAxis("Horizontal");

        //Sag-Sol Hareketi
        if (horizontalInput > 0.01f)
            transform.localScale =new Vector3(3,3,4);

        else if (horizontalInput < -0.01f)
            transform.localScale =new Vector3(-3,3,4);


        anima.SetBool("run", horizontalInput != 0);
        anima.SetBool("grounded", isGrounded());

        if (wallJumpCooldwon < 0.2f) {
            if(Input.GetKey(KeyCode.Space) && isGrounded())
            Jump();
            body.velocity = new Vector2(Input.GetAxis("Horizontal")*speed, body.velocity.y);

            if (onWall() && !isGrounded()) {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 1;
        }
        else
            wallJumpCooldwon += Time.deltaTime;
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
