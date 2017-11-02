using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour {
    public static menuController instatnce;
    public menuState homeState;
    public menuState selectLevelState;
    public menuState selectNumberOfPlayersState;
    public List<GameObject> gameObjectsInHomeState;
    public List<GameObject> gameObjectsSelectLevelState;
    public List<GameObject> gameObjectsSelectNumberOfPlayersState;

    private menuState currentState;

    void Start () {
       if (instatnce == null) {
            instatnce = this;
        }
        homeState.gameObjectsInThisState = gameObjectsInHomeState;
        selectLevelState.gameObjectsInThisState = gameObjectsSelectLevelState;
        selectNumberOfPlayersState.gameObjectsInThisState = gameObjectsSelectNumberOfPlayersState;
        currentState = homeState;
        currentState.enterState();
	}
	
    public void changeState(menuState newState) {
        currentState.exitState();
        newState.enterState();
        currentState = newState;
    }
    public void back() {
        currentState.back();
    }
    public void hitPlay() {
        changeState(selectNumberOfPlayersState);
    }
    public void exit() {
        Application.Quit();
    }
    public void selectNumberOfPlayers(int num) {
        globalOptions.numberOfPlayers = num;
        changeState(selectLevelState);
    }
    public void selectLevel(int index) {
        SceneManager.LoadScene(index);
    }
}
