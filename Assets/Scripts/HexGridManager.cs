using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexGridManager : MonoBehaviour {
    // The instance of the hexagon that we want to use
    public GameObject hexGridItem;
	
    // Contains all the hexagons on the battlefield
    private GameObject[,] gridList;
    private Hashtable hexHashtable;

    /**
     * Dimensions of the grid
     */ 
    private int gridLength = 8;
    private int gridHeight = 7;

    void Awake () {
        gridList = new GameObject[gridHeight, gridLength];
    }

    // Use this for initialization
	void Start () {
        GameObject gridHolder = new GameObject();
        gridHolder.transform.parent = this.transform;
        gridHolder.name = "GridHolder";
        createGrid(gridHolder);
    }
	
	// Update is called once per frame
	void Update () {
	   if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Casts the ray and get the first game object hit
            if (Physics.Raycast(ray, out hit)) {
                GameObject hex = hit.collider.transform.gameObject;
                hex.GetComponent<HexMesh>().debugPos();
                for (int i=0; i<gridList.GetLength(0); i++) {
                    for (int j=0; j<gridList.GetLength(1); j++) {
                        gridList[i,j].GetComponent<HexMesh>().setClicked(false);
                    }
                }
                hex.GetComponent<HexMesh>().toggleColor();
            }
       }
	}

    /**
     * Creates grid based hex tiles using an axial tiling system
     * Create the grid by creating a series of columns from left to right.
     * Columns are serpentine - e.g. 
     *      x
             x
            x
             x
            x
     */
    private void createGrid(GameObject gridHolder) {
        int xIndex = 0;
        int yIndex = 0;

        List<int> duplicateCheck = new List<int>();

        for (int j=0; j<gridLength; j++) {
            int storedXValue = xIndex;
            for(int i=0; i<gridHeight; i++) {
                // Create the game object and set its position
                GameObject hexagonCreated = (GameObject)Instantiate(hexGridItem);
                hexagonCreated.transform.parent = gridHolder.transform;
                float jPosition = i%2 + j*2;
                hexagonCreated.transform.position = new Vector3(jPosition,0,i*-1.5f);

                // Store the object in the array
                gridList[i,j] = hexagonCreated;

                // Store its axial and cube positions within the object itself
                hexagonCreated.GetComponent<HexMesh>().setXPos(xIndex);
                hexagonCreated.GetComponent<HexMesh>().setYPos(yIndex);
                hexagonCreated.GetComponent<HexMesh>().setCubeCoordinates(xIndex, -xIndex - yIndex, yIndex);

                int positionHash = hexagonCreated.GetComponent<HexMesh>().createPositionHash();
                hexHashtable[positionHash] = hexagonCreated;
                duplicateCheck.Add(positionHash);

                // Y always increases as we descend
                yIndex += 1;

                // Calculate x - as we descend, we alternate between keeping the same, and subtracting 1
                if (i%2 == 1) {
                    xIndex -= 1;
                }
            }
            // Move to next column - reset y and add 1 to whatever x was previously
            yIndex = 0;
            xIndex = storedXValue + 1;
        }
        assignGridNeighbors();
        if (Settings.isErrorCheckerEnabled()) {
            for (int i=0; i<duplicateCheck.Count; i++) {
                for (int j=0; j<duplicateCheck.Count; j++) {
                    if (duplicateCheck[i] == duplicateCheck[j] && i != j) {
                        Debug.Log("'Duplicate found in hashtable with '" + duplicateCheck[i]);
                    }
                }
            }
        }
    }

    /**
     * Assigns the neighbors for the 
     *
     * This will precalculate the positions of the possible neighbors for the particular hex grid item we are on
     * Then it cross references against the available positions of each element stored in the grid. 
     * If they match, that particular gameobject will be added as a list of the neighbors
     *
     * ADD POSITION RELATIVE TO THE NEIGHBOR? (i.e. top left)
     *
     * Not the fastest, but it should only need to be run once
     */
    private void assignGridNeighbors() {
        for(int i=0; i<gridList.GetLength(0); i++) {
            for (int j=0; j<gridList.GetLength(1); j++) {
                int valuesFound = 0;

                GameObject currentHex = gridList[i,j];
                HexMesh currentHex2 = (HexMesh) currentHex.GetComponent<HexMesh>();
                int xPos = currentHex2.getXPos();
                int yPos = currentHex2.getYPos();

                // List all relative positions of possible neighbors
                int[,] neighbors = {
                    { 1, 0},
                    { 1,-1},
                    { 0,-1},
                    {-1, 0},
                    {-1, 1},
                    { 0, 1},
                };

                for (int nIndex=0; nIndex<neighbors.GetLength(0); nIndex++) {
                    int xShift = neighbors[nIndex,0];
                    int yShift = neighbors[nIndex,1];

                    int xNeighbor = xPos + xShift;
                    int yNeighbor = yPos + yShift;

                    // Check if neighbor exists
                    for (int searchX=0; searchX<gridList.GetLength(0); searchX++) {
                        for (int searchY=0; searchY<gridList.GetLength(1); searchY++) {
                            int newX = gridList[searchX, searchY].GetComponent<HexMesh>().getXPos();
                            int newY = gridList[searchX, searchY].GetComponent<HexMesh>().getYPos();
                            if (newX == xNeighbor && newY == yNeighbor) {
                                valuesFound++;
                                currentHex2.addNeighbor(gridList[searchX, searchY]);
                            }
                        }
                    }
                }
            }
        }
    }
}
