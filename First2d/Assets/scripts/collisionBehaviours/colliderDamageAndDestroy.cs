using UnityEngine;

public class colliderDamageAndDestroy : MonoBehaviour {
    public int damage;
    public int force = 1000;
    public bool shouldBeDestroyed = false;
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<playerController>().takeDamage(damage);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce((-transform.position + collision.transform.position).normalized * force);
        }
        if(shouldBeDestroyed)
        Destroy(this.gameObject);
    }
}
