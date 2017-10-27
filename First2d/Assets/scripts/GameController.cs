using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController instance;
    public GameObject playerPrefab;
    public GameObject cursorPrefab;
    public GameObject chestPrefab;
    public GameObject placeableObjectsHolderPrefab;
    public List<GameObject> placeableObjects;
    public int numberOfPlayers=2;
    public List<Color> colors;
    public delegate void eventAction();
    public event eventAction gamestarted;
    public event eventAction roundStarted;
    public event eventAction roundEnded;
    public event eventAction itemsSelected;

    private int selectedItems = 0;
    private int placedItems = 0;
    private int deadPlayers = 0;
    private List<GameObject> players;
    private List<GameObject> cursors;
    private GameObject placeableObjectsHolder;
	
    private void initializeEvents() {
        gamestarted += instantiateCursors;
        gamestarted += instantiatePlayers;
        gamestarted += instantiatePlaceableObjectsHolder;
        gamestarted += disablePlayers;
        gamestarted += initializePlaceableobjectsHolder;
        gamestarted += randomizeCursorsPosition;

        roundStarted += spawnChests;
        roundStarted += randomizePlayersPosition;
        roundStarted += delayedEnablePlayers;

        itemsSelected += disablePlaceableObjectsHolder;
        itemsSelected += enableCursors;

        roundEnded += disablePlayers;
        roundEnded += resetRoundStats;
        roundEnded += enableCursors;
        roundEnded += enablePlaceableObjectsHolder;
        roundEnded += initializePlaceableobjectsHolder;
        roundEnded += randomizeCursorsPosition;
    }
	void Awake () {
        if (instance == null) {
            instance = this;
        }
        colors = new List<Color>();
        colors.Add(Color.green);
        colors.Add(Color.red);
        colors.Add(Color.blue);
        colors.Add(Color.white);

        initializeEvents();

	}
    private void Start() {
        gamestarted();
    }
    private void instantiatePlayers() {
        players = new List<GameObject>();
        for(int i = 1; i<= numberOfPlayers; i++) {
            GameObject player = Instantiate(playerPrefab, transform.position,Quaternion.identity);
            player.GetComponent<playerController>().setBindings(i);
            //player.GetComponent<playerController>().enabled=true;
            player.GetComponent<SpriteRenderer>().color = colors[i-1];
            players.Add(player);
        }
    }
    private void instantiateCursors() {
        cursors = new List<GameObject>();
        for (int i = 1; i <= numberOfPlayers; i++) {
            GameObject cursor = Instantiate(cursorPrefab, transform.position, Quaternion.identity);
            cursor.GetComponent<cursorController>().setBindings(i);
            //cursor.GetComponent<cursorController>().enabled=true;
            cursor.GetComponent<SpriteRenderer>().color = colors[i-1];
            cursors.Add(cursor);
        }
    }
    private void instantiatePlaceableObjectsHolder() {
        placeableObjectsHolder = Instantiate(placeableObjectsHolderPrefab, transform.position, Quaternion.identity);
    }
    private void delayedEnablePlayers() {
        Invoke("enablePlayers", 0.5f);
    }
    private void enablePlayers() {
        foreach(GameObject player in players) {
            player.SetActive(true);
        }
    }
    private void disablePlayers() {
        foreach (GameObject player in players) {
            player.SetActive(false);
        }
    }
    private void enableCursors() {
        foreach (GameObject cursor in cursors) {
            cursor.SetActive(true);
        }
    }
    private void disableCursors() {
        foreach (GameObject cursor in cursors) {
            cursor.SetActive(false);
        }
    }
    private void enablePlaceableObjectsHolder() {
        placeableObjectsHolder.SetActive(true);
    }
    private void disablePlaceableObjectsHolder() {
        placeableObjectsHolder.SetActive(false);
    }
    private void initializePlaceableobjectsHolder() {
        Bounds bounds = placeableObjectsHolder.GetComponent<SpriteRenderer>().bounds;
        for(int i = 0;  i < numberOfPlayers; i++) {
            Instantiate(placeableObjects[Random.Range(0, placeableObjects.Count)], bounds.center + new Vector3(Random.Range(-bounds.extents.x+2, bounds.extents.x-2), Random.Range(-bounds.extents.y+2, bounds.extents.y-2), 0), Quaternion.identity);
        }
    }
    private void randomizeCursorsPosition() {
        Bounds bounds = placeableObjectsHolder.GetComponent<SpriteRenderer>().bounds;
        for (int i = 0; i < numberOfPlayers; i++) {
            cursors[i].transform.position = bounds.center + new Vector3(Random.Range(-bounds.extents.x, bounds.extents.x), Random.Range(-bounds.extents.y, bounds.extents.y), 0);
        }
    }
    private void randomizePlayersPosition() {
        Bounds bounds = GetComponent<EdgeCollider2D>().bounds;
        for (int i = 0; i < numberOfPlayers; i++) {
            players[i].transform.position = bounds.center + new Vector3(Random.Range(-bounds.extents.x+2, bounds.extents.x-2), Random.Range(-bounds.extents.y+2, bounds.extents.y-2), 0);
            //if not above jumpable or placeable object OR intersects with something then retry
            if (!Physics2D.Linecast(players[i].transform.position, new Vector3(players[i].transform.position.x,-20, players[i].transform.position.z), 1 << LayerMask.NameToLayer("Jumpable") | 1 << LayerMask.NameToLayer("Placeable")) || Physics2D.OverlapBox(players[i].GetComponent<BoxCollider2D>().bounds.center, players[i].GetComponent<BoxCollider2D>().bounds.size,0)){
                i--;
            }
        }
    }
    private void spawnChests() {
        Bounds bounds = GetComponent<EdgeCollider2D>().bounds;
        for (int i = 0; i < numberOfPlayers; i++) {
            GameObject chest = Instantiate(chestPrefab, bounds.center + new Vector3(Random.Range(-bounds.extents.x + 2, bounds.extents.x - 2), Random.Range(-bounds.extents.y + 2, bounds.extents.y - 2), 0), Quaternion.identity);
            chest.SetActive(false);
            RaycastHit2D hit = Physics2D.Linecast(chest.transform.position, new Vector3(chest.transform.position.x, -20, chest.transform.position.z), 1 << LayerMask.NameToLayer("Jumpable") | 1 << LayerMask.NameToLayer("Placeable"));
            //if not above jumpable or placeable object OR intersects with something then retry
            if (!hit || Physics2D.OverlapBox(chest.GetComponent<BoxCollider2D>().bounds.center, chest.GetComponent<BoxCollider2D>().bounds.size, 0)) {
                i--;
                Destroy(chest);
            }
            else {
                //place on ground
                chest.transform.position = new Vector3(chest.transform.position.x, hit.point.y+chest.GetComponent<SpriteRenderer>().bounds.extents.y, 0);
                chest.GetComponent<placeableObject>().place();
                chest.SetActive(true);
            }
        }
    }
    public void itemSelected() {
        selectedItems++;
        if (selectedItems == numberOfPlayers) {
            itemsSelected();
        }
    }
    public void itemPlaced() {
        placedItems++;
        if (placedItems == numberOfPlayers) {
            roundStarted();
        }
    }
    public void playerDied() {
        deadPlayers++;
        if(deadPlayers == numberOfPlayers - 1) {
            roundEnded();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<playerController>().takeDamage(1000);
        }
        else {
            Destroy(collision.gameObject);              
        }
    }
    private void resetRoundStats() {
        selectedItems = 0;
        placedItems = 0;
        deadPlayers = 0;
    }
}
