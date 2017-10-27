using UnityEngine;

public class yetiController : MonoBehaviour, flippable {
    public float normalSpeed = 15;
    public float rageSpeed = 30;
    private float speed;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool facingLeft;
    private GameController gameController;
	void Awake () {
        gameController = GameController.instance;
        gameController.roundStarted += startYeti;
        gameController.roundEnded += stopYeti;
        gameController.gamestarted += stopYeti;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        facingLeft=!sr.flipX;
	}

    private void Update() {
        RaycastHit2D hit;
        if (facingLeft) {
            hit = Physics2D.Raycast(transform.Find("leftCheck").position, Vector2.left);
        }
        else {
            hit = Physics2D.Raycast(transform.Find("rightCheck").position, Vector2.right);
        }
        if(hit && hit.transform.gameObject.tag == "Player") {
            speed = rageSpeed;
        }
        else {
            speed = normalSpeed;
        }
    }
    void FixedUpdate () {
        if (facingLeft) {
            rb.velocity = new Vector2(-speed,0);
        }
        else {
            rb.velocity = new Vector2(speed, 0);
        }
	}
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<playerController>().takeDamage(100);
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000));
        }
        else {
            flip(!facingLeft);
        }
    }

    public void flip(bool flipLeft) {
        facingLeft = flipLeft;
        GetComponent<SpriteRenderer>().flipX = !facingLeft;
    }
    public void startYeti() {
        this.gameObject.SetActive(true);
    }
    public void stopYeti() {
        this.gameObject.SetActive(false);
    }
}
