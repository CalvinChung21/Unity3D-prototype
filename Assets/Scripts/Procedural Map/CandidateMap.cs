using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CandidateMap
{
    public MapGrid grid;
    private int numberOfPieces = 0;
    private bool[] obstaclesArray = null;
    private Vector3 startPoint, exitPoint;
    private List<KnightPiece> knightPiecesList;

    public CandidateMap(MapGrid grid)
    {
        this.grid = grid;
    }

    public MapGrid Grid
    {
        get => grid;
    }
    
    public bool[] ObstaclesArray
    {
        get => obstaclesArray;
    }

    public CandidateMap(MapGrid grid, int numberOfPieces)
    {
        this.numberOfPieces = numberOfPieces;
        this.knightPiecesList = new List<KnightPiece>();
        this.grid = grid;
    }

    public void CreateMap(Vector3 startPosition, Vector3 exitPosition, bool autoRepair = false)
    {
        this.startPoint = startPosition;
        this.exitPoint = exitPosition;
        
        RandomlyPlaceKnightPieces(this.numberOfPieces);
    }
    
    private bool CheckIfPositionCanBeObstacles(Vector3 position)
    {
        if (position == startPoint || position == exitPoint)
        {
            return false;
        }

        int index = grid.CalculateIndexFromCoordinates(position.x, position.z);

        return obstaclesArray[index] == false;
    }

    private void RandomlyPlaceKnightPieces(int numberOfPieces)
    {
        var count = numberOfPieces;
        var knightPlacementTryLimit = 100;
        while (count > 0 && knightPlacementTryLimit > 0)
        {
            var randomIndex = Random.Range(0, obstaclesArray.Length);
            if (obstaclesArray[randomIndex] == false)
            {
                var coordinates = grid.CalculateCoordinatesFromIndex(randomIndex);
                if (coordinates == startPoint || coordinates == exitPoint)
                {
                    continue;
                }
                obstaclesArray[randomIndex] = true;
                knightPiecesList.Add(new KnightPiece(coordinates));
                count--;
            }
            knightPlacementTryLimit--;
        }
    }

    public MapData ReturnMapData()
    {
        return new MapData
        {
            obstacleArray = this.obstaclesArray,
            KnightPiecesList = knightPiecesList,
            startPosition = startPoint,
            exitPosition = exitPoint
        };
    }
}
