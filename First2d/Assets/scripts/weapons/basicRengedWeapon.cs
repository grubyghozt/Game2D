using UnityEngine;
using System.Collections;

public class basicRengedWeapon : MonoBehaviour, Weapon, flippable {
    public GameObject bullet;
    public float bulletSpeedHorizontal = 25;
    public float bulletSpeedVertical;
    public float lift;
    public float fireRate;
    private bool facingLeft;
    private bool canFire = true;

    public void flip(bool flipLeft) {
        facingLeft = flipLeft;
        GetComponent<SpriteRenderer>().flipX = flipLeft;
        transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, facingLeft ? -lift : lift);
    }

    public void Use() {
        if (canFire) {
            if (facingLeft) {
                Instantiate(bullet, transform.Find("leftBulletSpawn").position, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(transform.Find("leftBulletSpawn").position.x - transform.position.x) * bulletSpeedHorizontal, bulletSpeedVertical);
            }
            else {
                Instantiate(bullet, transform.Find("rightBulletSpawn").position, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(transform.Find("rightBulletSpawn").position.x - transform.position.x) * bulletSpeedHorizontal, bulletSpeedVertical);
            }
            canFire = false;
            StartCoroutine("shotDelay");
        }
    }
    IEnumerator shotDelay() {
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }
}
