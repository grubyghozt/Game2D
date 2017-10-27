using UnityEngine;

public class playerController : MonoBehaviour, flippable {
    public string movementName = "Horizontal_P";
    public string jumpName = "Fire1_P";
    public string useName = "Fire2_P";
    public float speed = 10f;
    public float jumpSpeed = 20f;
    public int health=100;

    private GameObject weapon;
    private bool isJumping;
    private GameController gameController;
    private bool facingLeft;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Transform jumpDetection;
	
	void Awake () {
        rb = GetComponent<Rigidbody2D>();
        jumpDetection = transform.Find("JumpDetection");
        facingLeft = GetComponent<SpriteRenderer>().flipX;
        gameController = GameController.instance;
        sr = GetComponent<SpriteRenderer>();
        gameController.roundStarted += reset;
	}
    void Update() {
        //if player isn't standing on jumpable or placeable then isJumping is true so he can't jump again
        isJumping = !Physics2D.BoxCast(transform.position, GetComponent<BoxCollider2D>().bounds.size, 0,Vector2.down, transform.position.y-jumpDetection.position.y, 1 << LayerMask.NameToLayer("Jumpable") | 1 << LayerMask.NameToLayer("Placeable"));

        if (rb.velocity.x > 0 && facingLeft) {
            flip(false);
        }
        else if (rb.velocity.x < 0 && !facingLeft) {
            flip(true);
        }
        if (Input.GetButtonDown(useName) && weapon!=null){
            useWeapon();
        }
    }
    void FixedUpdate() {
        float movement = Input.GetAxisRaw(movementName);
        int direction=0;
        if (movement > 0){
            direction = 1;
        }
        else if(movement<0) {
            direction = -1;
        }
        //&& Mathf.Approximately(rb.velocity.y,0)
        if (Input.GetButton(jumpName) && !isJumping) {
            isJumping = true;
            rb.velocity = new Vector2(direction * speed, jumpSpeed);
        }
        else {
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        }
    }
    public void setBindings(int numOfPlayer) {
        movementName += numOfPlayer;
        jumpName += numOfPlayer;
        useName += numOfPlayer;
    }
    public void takeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            gameController.playerDied();
            this.gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
    public void equipWeapon(GameObject wep) {
        if (weapon != null) {
            Destroy(weapon.gameObject);
        }
        weapon = wep;
        weapon.transform.parent = transform;
        flip(facingLeft);
    }
    private void useWeapon() {
        weapon.GetComponent<Weapon>().Use();
    }
    public void flip(bool flipLeft) {
        facingLeft = flipLeft;
        sr.flipX = facingLeft;
        foreach(flippable flippable in GetComponentsInChildren<flippable>()) {
            if(flippable!=this)
            flippable.flip(flipLeft);
        }
    }
    public void reset() {
        health = 100;
        isJumping = true;
        if (weapon != null) {
            Destroy(weapon);
            weapon = null;
        }
    }
}
