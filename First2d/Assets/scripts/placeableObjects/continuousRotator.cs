using UnityEngine;

//Basic continuous rotation
public class continuousRotator : MonoBehaviour {
    public float speed = 100;
    public bool rotateRight;
	void Update () {
        transform.Rotate((rotateRight?1:-1)* -Vector3.forward*speed* Time.deltaTime);
	}
}
