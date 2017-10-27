using UnityEngine;

public class cursorController : MonoBehaviour {
    public string horizontalAxis = "Horizontal_P";
    public string verticalAxis = "Vertical_P";
    public string selectName = "Fire1_P";
    public string rotateLeftName = "RotateLeft_P";
    public string rotateRightName = "RotateRight_P";

    private placeableObject selectedObject;
    private GameController gameController;

	void Start () {
        gameController = GameController.instance;
	}
	void Update () {
        float horizontal = Input.GetAxisRaw(horizontalAxis);
        float vertical = Input.GetAxisRaw(verticalAxis);
        if (horizontal > 0) {
            transform.position = new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z);
        }
        else if (horizontal < 0) {
            transform.position = new Vector3(transform.position.x - 0.2f, transform.position.y, transform.position.z);
        }
        if (vertical > 0) {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        }
        else if (vertical < 0) {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
        }
        if (Input.GetButtonDown(selectName)) {
            if (selectedObject == null) {
                RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position, 1 << LayerMask.NameToLayer("Placeable"));
                if (hit) {
                    selectedObject = hit.transform.gameObject.GetComponent<placeableObject>();
                    if (!selectedObject.placed) {
                        selectedObject.select(transform);
                        this.gameObject.SetActive(false);
                        gameController.itemSelected();
                    }
                    else {
                        selectedObject = null;
                    }
                }
            }
            else {
                selectedObject.place();
                selectedObject = null;
                this.gameObject.SetActive(false);
                gameController.itemPlaced();
            }
        }
        if (Input.GetButtonDown(rotateLeftName)) {
            selectedObject.rotateLeft();
        }
        else if (Input.GetButtonDown(rotateRightName)) {
            selectedObject.rotateRight();
        }
    }
    public void setBindings(int numOfPlayer) {
        horizontalAxis += numOfPlayer;
        verticalAxis += numOfPlayer;
        selectName += numOfPlayer;
        rotateLeftName += numOfPlayer;
        rotateRightName += numOfPlayer;
    }
   
}
