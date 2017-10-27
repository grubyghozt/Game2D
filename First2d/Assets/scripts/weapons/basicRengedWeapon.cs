using UnityEngine;

public class basicRengedWeapon : MonoBehaviour, Weapon, flippable {
    public GameObject bullet;
    public float bulletSpeed = 25;
    private bool facingLeft;
    public void flip(bool flipLeft) {
        facingLeft = flipLeft;
        GetComponent<SpriteRenderer>().flipX = flipLeft;
    }

    public void Use() {
        if (facingLeft) {
            Instantiate(bullet, transform.Find("leftBulletSpawn").position, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(transform.Find("leftBulletSpawn").position.x - transform.position.x) * bulletSpeed, 0);
        }
        else {
            Instantiate(bullet, transform.Find("rightBulletSpawn").position, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(transform.Find("rightBulletSpawn").position.x - transform.position.x) * bulletSpeed, 0);
        }
    }
}
