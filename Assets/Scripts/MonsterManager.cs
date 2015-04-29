using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class MonsterManager : MonoBehaviour {
    /**
     * Monster game object to use
     */
    public GameObject monster;

    /**
     * The hex grid manager within the scene
     */
    public GameObject hexGridManager;

    /**
     * The Monster display 
     */
    public GameObject monsterDisplay;

    /**
     * Called when the script is being loaded 
     */
    void Awake () {
        
    }

    /**
     * Called when the script is first enabled (will not be run until an object is enabled)
     */
    void Start () {
        GameObject monsterCreated = (GameObject)Instantiate(monster);

        // Get a random starting position
        GameObject randomTile = hexGridManager.GetComponent<HexGridManager>().getRandomTile();

        // monsterCreated.transform.position = 
    }

    /**
     * Called once per frame
     */
    void Update () {

    }

    /**
     * TODO - MAKE INTO AN INTERFACE FOR A MANAGER CLASS
     * Receives a notification of a click event that happened from one of the monsters
     */
    public void notifyOfClick(GameObject monster) {
        monsterDisplay.GetComponent<MonsterActionDisplay>().moveToGameObject(monster);
    }
}