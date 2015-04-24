using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Monster : MonoBehaviour {
    // Random number generation (DO NOT INSTANTIATE MULTIPLE TIMES FOR BETTER RANDOMNESS)
    System.Random random;

    // A list of all possible elements
    List<Elements> allElements;
    
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

    private Elements getRandomElement() {
        int index = random.Next(allElements.Count);
        return allElements[index];
    }

    /**
     * Called when the script is first enabled (will not be run until an object is enabled)
     */
    void Start () {

    }

    /**
     * Called once per frame
     */
    void Update () {
        if (Input.GetMouseButtonDown(0)) {
            Elements element = getRandomElement();
            setMaterialColor(element.getColor());
            Debug.Log(element);
        }
    }

    /**
     * Sets the material color of the hexagon - this has the effect of changing the appearance of the hexagon 
     * @param {[type]} Color c - The color we want the hexagon to appear as (certain values will not appear due to texture used)
     */
    private void setMaterialColor(Color c) {
        gameObject.renderer.material.color = c;
    }

}