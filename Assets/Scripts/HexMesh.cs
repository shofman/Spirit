using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class HexMesh : MonoBehaviour {
    Vector3[] newVertices;
    Vector2[] newUV;
    int[] newTriangles;

    public int xPos = 0;
    public int yPos = 0;

    int cubeXPos = 0;
    int cubeYPos = 0;
    int cubeZPos = 0;

    private List<GameObject> neighbors;

    private bool clicked = false;

    void Awake() {
        neighbors = new List<GameObject>();
        setupVertices();
        setupUV();
        setupTriangles();
        setupNormals();
    }

    void Start() {
        Mesh mesh = new Mesh();
        mesh.name = "Hexagon";
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = newVertices;
        mesh.uv = newUV;
        mesh.triangles = newTriangles;
        mesh.RecalculateNormals();

        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    void Update() {

    }

    /**
     * Debugs info about the hex mesh object clicked (position)
     * @param displayPos - whether we want to display the position of the clicked objects
     */
    public void debugPos(bool displayPos, bool clearScreen) {
        if (clearScreen) {
            Utility.clearLog();
        }
        Debug.Log("CALLED HERE with " + xPos + " " + yPos);
        if (displayPos) {
            Debug.Log("CUBE XPOS IS: " + cubeXPos);
            Debug.Log("CUBE YPOS IS: " + cubeYPos);
            Debug.Log("CUBE ZPOS IS: " + cubeZPos);
        }
    }

    /**
     * Toggles the color of the current hexagon
     */
    public void toggleColor() {
        if (clicked) {
            setMaterialColor(Color.red);
        } else {
            setMaterialColor(Color.white);
        }
        clicked = !clicked;
    }

    /**
     * Sets whether this particular hexagon has been clicked
     * @param {[type]} bool isClicked Whether we want to indicate this hexagon has been selected or deselected
     */
    public void setClicked(bool isClicked) {
        clicked = isClicked;
        toggleColor();
    }

    /**
     * Sets the material color of the hexagon - this has the effect of changing the appearance of the hexagon 
     * @param {[type]} Color c - The color we want the hexagon to appear as (certain values will not appear due to texture used)
     */
    private void setMaterialColor(Color c) {
        gameObject.renderer.material.color = c;
    }

    /**
     * @overloads debugPos(bool displayPos, bool clearScreen)
     */
    public void debugPos() {
        debugPos(false, true);
    }

    /**
     * Adds a neighbor to this hexagon
     * @param {[type]} GameObject neighbor Another hexagon that we consider this to be its neighbor
     */
    public void addNeighbor(GameObject neighbor) {
        neighbors.Add(neighbor);
    }

    /**
     * Display all neighbors axial positions to the unity console 
     */
    public void debugNeighbors() {
        foreach (GameObject neighbor in neighbors) {
            neighbor.GetComponent<HexMesh>().debugPos(false, false);
        }
    }

    /**
     * Displays number of neighbors this hex has to the unity screen
     * @param  {[type]} bool clearScreen   Whether we should clear the screen before showing the value
     */
    public void debugNeighborCount(bool clearScreen) {
        if (clearScreen) {
            Utility.clearLog();
        }
        Debug.Log(neighbors.Count);
    }

    /**
     * Displays the amount of neighbors this hex has to the unity screen
     */
    public void debugNeighborCount() {
        debugNeighborCount(true);
    }

    /**
     * Creates a unique hash based on the axial positions within the hex grid 
     * @return {[type]} int - a unique hashcode based off the positions
     */
    public int createPositionHash() {
        int hashCode = 23;
        hashCode = (hashCode * 37) + xPos;
        hashCode = (hashCode * 37) + yPos;
        return hashCode;
    }

    /**
     * @return {[type]} int - Returns the x axial coordinate within the hex grid system 
     */
    public int getXPos() {
        return this.xPos;
    }

    /**
     * @return {[type]} int - Returns the y axial coordinate within the hex grid system 
     */
    public int getYPos() {
        return this.yPos;
    }

    /**
     * Sets the position of axial value of X for this hexagon - represents motion in the horizontal direction across the grid
     * @param {[type]} int yPos - Value we want to set
     */
    public void setXPos(int xPos) {
        this.xPos = xPos;
    }

    /**
     * Sets the position of axial Y for this hexagon - represents motion in the vertical direction
     * @param {[type]} int yPos - Value we want to set
     */
    public void setYPos(int yPos) {
        this.yPos = yPos;
    }

    /**
     * Sets the cubic parameters of the hex - each axis represents a line across the hex grid
     * @param {[int]} xPos - The x position that we want the cubic value to equal
     * @param {[int]} yPos - The y position that we want the cubic value to equal 
     * @param {[int]} zPos - The z position that we want the cubic value to equal
     */
    public void setCubeCoordinates(int xPos, int yPos, int zPos) {
        this.cubeXPos = xPos;
        this.cubeYPos = yPos;
        this.cubeZPos = zPos;
    }

    /**
     * Gets the x value for the cubic position 
     * @return {[int]}
     */
    public int getCubeXPos() {
        return this.cubeXPos;
    }

    /**
     * Gets the y value for the cubic position 
     * @return {[int]}
     */
    public int getCubeYPos() {
        return this.cubeYPos;   
    }

    /**
     * Gets the z value for the cubic position 
     * @return {[int]}
     */
    public int getCubeZPos() {
        return this.cubeZPos;
    }

    /**
     * Creates the vertices on the mesh object
     */
    private void setupVertices() {
        newVertices = new Vector3[] {
            new Vector3(0,0,1),
            new Vector3(1,0,.5f),
            new Vector3(1,0,-.5f),
            new Vector3(0,0,-1),
            new Vector3(-1,0,-.5f),
            new Vector3(-1,0,.5f)
        };
    }

    /**
     * Sets up the triangles on the mesh object 
     */
    private void setupTriangles() {
        newTriangles = new int[] {
            1,5,0,
            1,4,5,
            1,2,4,
            2,3,4
        };
    }

    /**
     * Creates UV mappings for Hexagon
     * Each vector is a corner of the hexagon 
     */
    private void setupUV() {
        newUV = new Vector2[] {
            // Generic Values
            /*new Vector2(0,.25f),
            new Vector2(0,.75f),
            new Vector2(.5f,1),
            new Vector2(1,.75f),
            new Vector2(1,.25f),
            new Vector2(.5f,0)*/

            // Precise values
            new Vector2(.17f,.25f),
            new Vector2(.165f,.74f),
            new Vector2(.5f,.985f),
            new Vector2(.8f,.75f),
            new Vector2(.79f,.24f),
            new Vector2(.475f,0.01f)
        };
    }

    private void setupNormals() {
        // Does nothing for now
    }

}