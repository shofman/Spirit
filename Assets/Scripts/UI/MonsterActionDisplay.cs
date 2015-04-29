using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MonsterActionDisplay : MonoBehaviour {
    Button move;
    Button attack;
    /**
     * Called when the script is being loaded 
     */
    void Awake () {

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
        if (Input.GetMouseButtonDown(1)) {
            disableDisplay();
        }
    }

    public void moveToGameObject(GameObject monster) {
        if (!gameObject.activeSelf) {
            enableDisplay();
        }
        Vector3 monsterPos = monster.transform.position;
        gameObject.transform.position = new Vector3(monsterPos[0] + 14, 5, monsterPos[2] + 5);
        // Debug.Log(position);
    }

    /**
     * Enables the current display
     */
    public void enableDisplay() {
        gameObject.SetActive(true);
    }

    /**
     * Disables the current display
     */
    public void disableDisplay() {
        gameObject.SetActive(false);
    }

    /**
     * Sets the mouse state to attack
     * Used as part of the UI button
     */
    public void setStateAsAttack() {
        Mouse.instance().setCurrentState(Mouse.State.Attack);
    }

    /**
     * Sets the mouse state to move
     * Used as part of the UI button
     */
    public void setStateAsMove() {
        Mouse.instance().setCurrentState(Mouse.State.Move);
    }
}