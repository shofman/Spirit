using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class MonsterManager : MonoBehaviour {
    /**
     * Monster game object to use
     */
    public GameObject monsterTemplate;

    /**
     * The hex grid manager within the scene
     */
    public GameObject hexGridManager;

    /**
     * The Monster display 
     */
    public GameObject monsterDisplay;

    /**
     * The monster that has been currently selected (one at a time)
     */
    private GameObject selectedMonster;

    /**
     * Called when the script is being loaded 
     */
    void Awake () {
        
    }

    /**
     * Called when the script is first enabled (will not be run until an object is enabled)
     */
    void Start () {
        GameObject monsterCreated = (GameObject)Instantiate(monsterTemplate);
        monsterCreated.name = "TIM";
        // Get a random starting position
        GameObject randomTile = hexGridManager.GetComponent<HexGridManager>().getRandomTile();

        // monsterCreated.transform.position = 
    }

    /**
     * Called once per frame
     */
    void Update () {

    }

    public GameObject getSelectedMonster() {
        return selectedMonster;
    }

    public void debugCurrentSelectedMonster() {
        if (selectedMonster != null) {
            Debug.Log(selectedMonster.name);
        }
    }

    /**
     * TODO - MAKE INTO AN INTERFACE FOR A MANAGER CLASS
     * Receives a notification of a click event that happened from one of the monsters
     */
    public void notifyOfClick(GameObject monster) {
        Mouse.State currentState = Mouse.instance().getCurrentState();
        switch (currentState) {
            case Mouse.State.Select:
                monsterDisplay.GetComponent<MonsterActionDisplay>().moveToGameObject(monster);
                selectedMonster = monster;
                break;
            case Mouse.State.Attack:
                Debug.Log("WE ARE TRYING TO ATTACK " + monster.name);
                break;
            case Mouse.State.Move:
                Debug.Log("CANNOT MOVE TO THIS POSITION");
                break;
        }
    }
}