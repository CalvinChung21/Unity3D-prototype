using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapVisualizer : MonoBehaviour
{
    private Transform parent;
    public Color startColor, exitColor;

    private void Awake()
    {
        parent = this.transform;
    }

    public void VisualizeMap(MapGrid grid, MapData data, bool visualizeUsingPrefabs)
    {
        if (visualizeUsingPrefabs)
        {
            
        }
        else
        {
           
        }
    }
    
    private void VisualizeUsingPrimities(MapGrid grid, MapData data)
    {
        PlaceStartAndExitPoints(data);
        for (int i = 0; i < data.obstacleArray.Length; i++)
        {
            if (data.obstacleArray[i])
            {
                var positionOnGrid = grid.CalculateCoordinatesFromIndex(i);
                if (positionOnGrid == data.startPosition || positionOnGrid == data.exitPosition)
                {
                    continue;
                }
                grid.SetCell(positionOnGrid.x, positionOnGrid.z, CellObjectType.Obstacle);

            }
        }
    }

    private void PlaceStartAndExitPoints(MapData data)
    {
        CreateIndicator(data.startPosition, startColor, PrimitiveType.Sphere);
        CreateIndicator(data.exitPosition, exitColor, PrimitiveType.Sphere);
    }

    private void CreateIndicator(Vector3 position, Color color, PrimitiveType sphere)
    {
        var element = GameObject.CreatePrimitive(sphere);
        element.transform.position = position;
        element.transform.parent = parent;
        var renderer = element.GetComponent<Renderer>();
        renderer.material.SetColor("_Color", color);
    }
}



public struct MapData
{
    public bool[] obstacleArray;
    public List<KnightPiece> KnightPiecesList;
    public Vector3 startPosition;
    public Vector3 exitPosition;
}
