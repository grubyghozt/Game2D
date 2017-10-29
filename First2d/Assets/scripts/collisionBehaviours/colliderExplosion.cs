using UnityEngine;

public class colliderExplosion : MonoBehaviour {
    public GameObject explosion;
    private void OnCollisionEnter2D(Collision2D collision) {
        Instantiate(explosion, transform.position,explosion.transform.rotation);
        Destroy(this.gameObject);
    }
}
