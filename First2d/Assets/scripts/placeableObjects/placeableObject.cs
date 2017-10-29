using UnityEngine;

public class placeableObject : MonoBehaviour {
    public bool placed;
    public void rotateRight() {
        transform.Rotate(Vector3.forward, 90);
    }
    public void rotateLeft() {
        transform.Rotate(Vector3.forward, -90);
    }
    public void select(Transform parent) {
        transform.parent = parent;
    }
    public void place() {
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>()) {
            sr.sortingOrder = 1;
        }
        placed = true;
        transform.parent = null;
    }
}
