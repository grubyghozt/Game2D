using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "newMenuState", menuName = "myAssets/menuState")]
public class menuState : ScriptableObject {
    public List<GameObject> gameObjectsInThisState;
    public menuState backState;
    public void enterState() {
        foreach(GameObject gameObject in gameObjectsInThisState) {
            gameObject.SetActive(true);
        }
    }
    public void exitState() {
        foreach (GameObject gameObject in gameObjectsInThisState) {
            gameObject.SetActive(false);
        }
    }
    public void back() {
        menuController.instatnce.changeState(backState);
    }
}
