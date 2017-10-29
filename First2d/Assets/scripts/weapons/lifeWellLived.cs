using System.Collections;
using UnityEngine;

public class lifeWellLived : MonoBehaviour {
    public float lifeTime;
	void Start () {
        StartCoroutine("destroyAfterTime");
	}
    IEnumerator destroyAfterTime() {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }
}
