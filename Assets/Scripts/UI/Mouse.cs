using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/**
 * Class that handles the state that a mouse can be in
 */
public class Mouse {
    /**
     * List of states that a mouse can be in
     */
    public enum State {
        Select,
        Attack,
        Move
    };

    /**
     * The display object on the scene
     */
    private GameObject display;

    /**
     * The current state of the mouse
     */
    private State currentState;

    /**
     * Singleton pattern - we want only one instance of a mouse state per game
     */
    private static Mouse _instance;

    /**
     * Constructor - set to private to prevent accidental use
     */
    private Mouse() {
        currentState = State.Select;
    }

    /**
     * Statically access the current mouse state
     * @return Mouse - the current object (if initially null, we create a new Mouse)
     */
    public static Mouse instance() {
        if (_instance == null) {
            _instance = new Mouse();
        }
        return _instance;
    }

    /**
     * Gets the curremt state of the mouse
     * @return State - What State the mouse is in
     */
    public State getCurrentState() {
        return currentState;
    }

    /**
     * Sets the current mouse state
     * @param {[type]} State s - The state that we wish to set the mouse to
     */
    public void setCurrentState(State s) {
        currentState = s;
        updateDisplay(currentState);
    }

    /**
     * Sets the display
     * @param {[type]} GameObject display The display 
     */
    public void setDisplay(GameObject display) {
        this.display = display;
    }

    /**
     * Gets the users mouse click from the position of the camera
     * @return {[type]} GameObject - Returns the gameObject that  was clicked
     *                            or null if nothing was hit
     */
    public GameObject getClick() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Casts the ray and get the first game object hit
        if (Physics.Raycast(ray, out hit)) {
            return hit.collider.transform.gameObject;
        } else {
            return null;
        }
    }

    /**
     * Updates the display for the current state
     */
    private void updateDisplay(State state) {
        display.GetComponent<MouseDisplay>().updateDisplay(state);
    }
}