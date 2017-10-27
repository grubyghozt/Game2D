using System.Collections;
using UnityEngine;

public class basicMelleWeapon : MonoBehaviour, Weapon, flippable {
    private bool facingLeft;
    public void flip(bool flipLeft) {
        facingLeft = flipLeft;
    }
	public void Use() {
        if (facingLeft) {
            StartCoroutine("SwingLeftRight");          
        }
        else {
            StartCoroutine("SwingRightLeft");
        }

    }
    IEnumerator SwingRightLeft() {
        for(int i = 0; i <9; i ++) {
            transform.Rotate(new Vector3(0, 0, -10));
            yield return null;
        }
        for (int i = 0; i < 9; i++) {
            transform.Rotate(new Vector3(0, 0, 10));
            yield return null;
        }
    }
    IEnumerator SwingLeftRight() {
        for (int i = 0; i < 9; i ++) {
            transform.Rotate(new Vector3(0, 0, 10));
            yield return null;
        }
        for (int i = 0; i < 9; i++) {
            transform.Rotate(new Vector3(0, 0, -10));
            yield return null;
        }
    }
}
