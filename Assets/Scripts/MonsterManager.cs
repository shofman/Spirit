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
        for (int i=0; i<3; i++) {
            spawnMonster("RANDOM"+i);
        }
        // spawnMonster("Tim");
        // spawnMonster("George");
        // spawnMonster("BOB");

        // monsterCreated.transform.position = 
    }

    /**
     * Called once per frame
     */
    void Update () {
        if (Input.GetKeyDown("4")) {
            // randomTile.GetComponent<HexMesh>().setClicked(true);
        }
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

    /**
     * Creates a monster on the field at random (as long as there isn't another monster already on the field)
     * @param  {[type]} string name The name of the monster
     */
    private void spawnMonster(string name) {
        GameObject monsterCreated = (GameObject)Instantiate(monsterTemplate);
        GameObject randomTile = null;
        while (randomTile == null) {
            randomTile = hexGridManager.GetComponent<HexGridManager>().getRandomTile();
            if (randomTile.GetComponent<HexMesh>().hasMonster()) {
                randomTile = null;
            } else {
                randomTile.GetComponent<HexMesh>().setAssociatedMonster(monsterCreated);
                monsterCreated.GetComponent<Monster>().setTilePosition(randomTile);
                monsterCreated.transform.position = randomTile.transform.position;
            }
        }
        monsterCreated.name = name;
    }
}