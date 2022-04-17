using UnityEngine;
using System.Collections;

public class Node : IHeapItem<Node> {
    
	// position and whether it is walkable 
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;
    
    // for calculating path finding
    public int gCost; // the cost of the path from the start node to n
    public int hCost; // estimates the cost to reach goal from node n
    public Node parent; // 
    int heapIndex; // for the use of prioritising their order in a queue based on their f cost
	
    // constructor for the node
    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY) {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }
    
    // getter for f cost
    public int fCost { get => gCost + hCost; }
    
    // getter and setter for the heap index
    public int HeapIndex
    {
        get => heapIndex;
        set => heapIndex = value;
    }
    
    // the function used to compare the f cost of each node
    // if f cost are the same, then compare their h cost <-- estimate cost to the goal
    // return -1 if the current node has lower priority <-- higher cost
    // return 0 if both node has the same cost
    // return 1 if the current node has higher priority <-- lower cost
    public int CompareTo(Node nodeToCompare) {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0) {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}