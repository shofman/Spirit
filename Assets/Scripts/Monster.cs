using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class Monster : MonoBehaviour, IPointerClickHandler {

    // A list of all possible elements
    List<Elements> allElements;

    /** 
     * Manager for handling the monsters
     */
    GameObject monsterManager;

    /**
     * Current Element that this monster has
     */
    Elements currentElement;

    /**
     * Whether we are trying to right click on this particular object
     * @type {Boolean}
     */
    bool isRightClicking = false;

    /**
     * TODO - MOVE MOVEMENT CODE INTO SEPARATE FILE
     */
    /**
     * Whether or not the monster is currently moving
     * @type {Boolean}
     */
    bool isMoving = false;

    /**
     * The location where we want to move
     * @type {GameObject}
     */
    private GameObject targetMovementHexagon;

    /**
     * The hexagon game object the monster is currently on
     */
    private GameObject tilePosition;

    /**
     * How far a monster is allowed to move
     */
    protected int movementAmount;

    /**
     * How far a monster is allowed to attack
     */
    protected int attackRange;

    /**
     * How fast the monster when it does move
     */
    float speed;
    
    /**
     * Called when the script is being loaded 
     */
    void Awake () {
        allElements = new List<Elements> {
            AirElem.getInstance(),
            CaveElem.getInstance(),
            EtherealElem.getInstance(),
            FireElem.getInstance(),
            IceElem.getInstance(),
            JungleElem.getInstance(),
            LightElem.getInstance(),
            ShadowElem.getInstance(),
            WaterElem.getInstance()
        };
        
        movementAmount = 5;
        speed = 10;
        attackRange = 2;
    }

    /**
     * Called when the script is first enabled (will not be run until an object is enabled)
     */
    void Start () {
        monsterManager = GameObject.Find("MonsterManager");
        setRandomElement();
    }

    /**
     * Called once per frame
     */
    void Update () {
        if (Input.GetMouseButtonDown(0)) {
            // GameObject objectClicked = Mouse.instance().getClick();
            // if (objectClicked != null && objectClicked.tag == "Monster") {
            //     if (Mouse.instance().getCurrentState() == Mouse.State.Attack &&
            //         objectClicked != gameObject) {
            //         Debug.Log("'We are attacking'");
            //     }
            //     if (objectClicked == gameObject) {
            //         Mouse.instance().setCurrentState(Mouse.State.Select);
            //     }
            // } 
        } else if (Input.GetMouseButtonDown(1)) {
            isRightClicking = true;
        } else if (Input.GetMouseButtonUp(1)) {
            isRightClicking = false;
            Mouse.instance().setCurrentState(Mouse.State.Select);
        }

        if (isMoving) {
            moveTo(targetMovementHexagon);
            if (transform.position == targetMovementHexagon.transform.position) {
                isMoving = false;
                Debug.Log("REACHED POSITION");
            }
        }
    }

    /**
     * Moves the monster to a location defined by the Vector3
     * @param  {[type]} Vector3 positionToMoveTo - Where we want to move the monster to
     *                                             We only use the x and z coordinates here
     */
    public void moveTo(GameObject hexagon) {
        Vector3 position = hexagon.transform.position;
        if (!isMoving) {
            isMoving = true;
            targetMovementHexagon = hexagon;
        }
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, position, step);
        setTilePosition(hexagon);
    }

    /**
     * Detects mouse clicks, but only if a UI element does not interfere with the click
     * @param eventData - Information about the event that we don't really care currently about
     */
    public void OnPointerClick(PointerEventData eventData) {
        if (!isRightClicking) {
            monsterManager.GetComponent<MonsterManager>().notifyOfClick(gameObject);
        }
    }

    /**
     * Returns the movement amount for this monster
     * @return int - The amount of spaces this monster can move
     */
    public int getMovementAmount() {
        return movementAmount;
    }

    /**
     * Returns the range for which this monster can attack
     * @return {[type]} int - The range of attack
     */
    public int getAttackRange() {
        return attackRange;
    }

    /**
     * Sets the position of the monster
     */
    public void setTilePosition(GameObject hexagon) {
        this.tilePosition = hexagon;
    }

    /**
     * Returns the hexagon object where the monster currently is (or is moving to)
     */
    public GameObject getTilePosition() {
        return this.tilePosition;
    }

    /**
     * Gets a random element for use
     */
    private Elements getRandomElement() {
        int index = RandomGenerator.instance().getRandomNumber(allElements.Count);
        Debug.Log(index);
        return allElements[index];
    }

    /**
     * Sets this monster to be a random element
     */
    private void setRandomElement() {
        Elements element = getRandomElement();
        setMaterialColor(element.getColor());
        currentElement = element;
    }

    /**
     * Sets the material color of the hexagon - this has the effect of changing the appearance of the hexagon 
     * @param {[type]} Color c - The color we want the hexagon to appear as (certain values will not appear due to texture used)
     */
    private void setMaterialColor(Color c) {
        gameObject.renderer.material.color = c;
    }

}