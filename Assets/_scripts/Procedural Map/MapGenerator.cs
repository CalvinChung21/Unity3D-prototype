using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GridVisualizer gridVisualizer;
    public Direction startEdge, exitEdge;
    public bool randomPlacement;
    private Vector3 startPosition, exitPosition;
    
    [Range(3, 20)] 
    public int width = 11, length = 11;
    private MapGrid grid;
    // Start is called before the first frame update
    private void Start()
    {
        grid = new MapGrid(width, length);
        gridVisualizer.VisualizerGrid(width, length);
        MapHelper.RandomlyChooseAndSetStartAndExit(grid, ref startPosition, ref exitPosition, randomPlacement, startEdge, exitEdge);
        Debug.Log(startPosition);
        Debug.Log(exitPosition);
    }
    
}
