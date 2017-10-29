using UnityEngine;

public class chestController : MonoBehaviour {
    public weaponsList weaponsList;
	private GameObject weaponInChest;
	void Start () {
        weaponInChest = weaponsList.weapons[Random.Range(0, weaponsList.weapons.Count)];
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            GameObject weapon = Instantiate(weaponInChest, collision.gameObject.transform.position,weaponInChest.transform.rotation);
            collision.gameObject.GetComponent<playerController>().equipWeapon(weapon);
            Destroy(this.gameObject);
        }
    }
}
