using UnityEngine;

public class followObject : MonoBehaviour {
    public Transform objectToFollow;
	void Update () {
        this.transform.position = objectToFollow.position;
	}
}
