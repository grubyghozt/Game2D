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
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>()) {
            sr.sortingOrder = 1;
        }
        transform.parent = parent;
        placed = true;
    }
    public void place() {
        transform.parent = null;
    }
}
