using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MouseDisplay : MonoBehaviour {
    Text displayText;
    /**
     * Called when the script is being loaded 
     */
    void Awake () {
        Mouse.instance().setDisplay(gameObject);
        displayText = GetComponent<Text>();
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
        if (Input.GetKeyDown("1")) {
            Mouse.instance().setCurrentState(Mouse.State.Select);
        } else if (Input.GetKeyDown("2")) {
            Mouse.instance().setCurrentState(Mouse.State.Attack);
        } else if (Input.GetKeyDown("3")) {
            Mouse.instance().setCurrentState(Mouse.State.Move);
        }
    }

    public void updateDisplay(Mouse.State s) {
        Debug.Log(s.ToString());
        displayText.text = "Mouse State: " + s.ToString();
    }
}