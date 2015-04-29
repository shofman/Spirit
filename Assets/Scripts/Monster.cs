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
            GameObject objectClicked = Mouse.instance().getClick();
            if (objectClicked != null && objectClicked.tag == "Monster") {
                if (Mouse.instance().getCurrentState() == Mouse.State.Attack &&
                    objectClicked != gameObject) {
                    Debug.Log("'We are attacking'");
                }
                if (objectClicked == gameObject) {
                    Mouse.instance().setCurrentState(Mouse.State.Select);
                }
            } 
        } else if (Input.GetMouseButtonDown(1)) {
            isRightClicking = true;
        } else if (Input.GetMouseButtonUp(1)) {
            isRightClicking = false;
            Mouse.instance().setCurrentState(Mouse.State.Select);
        }
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