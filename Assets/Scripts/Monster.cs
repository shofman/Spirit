using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class Monster : MonoBehaviour, IPointerClickHandler {
    // Random number generation (DO NOT INSTANTIATE MULTIPLE TIMES FOR BETTER RANDOMNESS)
    System.Random random;

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
     * @type {Vector3}
     */
    private Vector3 targetMovementPos;

    /**
     * How far a monster is allowed to move
     */
    protected int movementAmount;

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
        random = new System.Random();
        movementAmount = 100;
        speed = 10;
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
            moveTo(targetMovementPos);
            if (transform.position == targetMovementPos) {
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
    public void moveTo(Vector3 position) {
        if (!isMoving) {
            isMoving = true;
            targetMovementPos = position;
        }
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, position, step);
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
     * Gets a random element for use
     */
    private Elements getRandomElement() {
        int index = random.Next(allElements.Count);
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